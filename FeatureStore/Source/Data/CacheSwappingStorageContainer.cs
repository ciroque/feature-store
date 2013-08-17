// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CacheSwappingStorageContainer.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   An implementation of the IStorageContainer interface that uses a file-backed cache utilizing two synchronized caches.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.Data
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Threading;
    using Mutual;

    /// <summary>
    ///   An implementation of the IStorageContainer interface that uses a file-backed cache utilizing two synchronized caches.
    /// </summary>
    public class CacheSwappingStorageContainer : IStorageContainer
    {
        /// <summary>
        ///   Used to control access to shared resources during an update operation.
        /// </summary>
        private readonly object m_SyncRoot = new object();

        /// <summary>
        ///   The *Active* Feature list. That is, the list that is used by querying methods.
        /// </summary>
        private ConcurrentDictionary<FeatureKey, Feature> m_ActiveFeatureList;

        /// <summary>
        ///   The *Backup* Feature list. That is, the list that is used to offload updates and additions to allow online query operations to continute while the cache is updated.
        /// </summary>
        private ConcurrentDictionary<FeatureKey, Feature> m_BackupFeatureList;

        /// <summary>
        ///   Initializes a new instance of the <see cref = "CacheSwappingStorageContainer" /> class.
        /// </summary>
        /// <param name = "storageLocation">The physical storage location for the cache.</param>
        public CacheSwappingStorageContainer(string storageLocation)
        {
            StorageLocation = storageLocation;
            if (File.Exists(StorageLocation))
            {
                DeserializeCache();
            }
            else
            {
                m_ActiveFeatureList = new ConcurrentDictionary<FeatureKey, Feature>();
                m_BackupFeatureList = new ConcurrentDictionary<FeatureKey, Feature>();
            }
        }

        /// <summary>
        ///   Gets or sets the physica storage location.
        /// </summary>
        /// <value>The storage location.</value>
        private string StorageLocation { get; set; }

        #region IStorageContainer Members

        /// <summary>
        ///   Stores the specified entity.
        /// </summary>
        /// <param name = "feature">The entity to be stored.</param>
        /// <returns>The entity that was saved.</returns>
        public Feature Store(Feature feature)
        {
            lock (m_SyncRoot)
            {
                FeatureKey featureKey = FeatureKey.Create(feature.Id, feature.OwnerId, feature.Space);

                // update the current backup copy
                m_BackupFeatureList.AddOrUpdate(featureKey, key => feature, (key, original) => feature);

                // persist the new feature set to disk
                SerializeCache(m_BackupFeatureList);

                // swap the backup into the active
                m_BackupFeatureList = Interlocked.Exchange(ref m_ActiveFeatureList, m_BackupFeatureList);

                // update the previously active copy
                m_BackupFeatureList.AddOrUpdate(featureKey, key => feature, (key, original) => feature);
            }

            return feature;
        }

        /// <summary>
        ///   Searches for a <see cref = "Feature" /> matching the criteria specified in the query.
        /// </summary>
        /// <param name = "key">The query.</param>
        /// <returns>
        ///   A <see cref = "Feature" /> instance matching the parameters specified by the <see cref = "FeatureKey" /> or null.
        /// </returns>
        public Feature Retrieve(FeatureKey key)
        {
            if (m_ActiveFeatureList.ContainsKey(key))
            {
                return m_ActiveFeatureList[key];
            }

            return null;
        }

        /// <summary>
        ///   Retrieves the specified feature space.
        /// </summary>
        /// <param name = "featureScope">The feature space.</param>
        /// <returns>An IEnumerable instance containing the results of the query.</returns>
        public IList<Feature> Retrieve(FeatureScope featureScope)
        {
            return m_ActiveFeatureList.Values
                .Where(entry =>
                       (featureScope.OwnerId == Guid.Empty || featureScope.OwnerId == entry.OwnerId)
                       && (featureScope.Space == Guid.Empty || featureScope.Space == entry.Space)).ToList();
        }

        #endregion

        /// <summary>
        ///   Serializes the cache.
        /// </summary>
        /// <param name = "features">The features to be serialized.</param>
        private void SerializeCache(ConcurrentDictionary<FeatureKey, Feature> features)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (Stream streamWriter = File.OpenWrite(StorageLocation))
            {
                formatter.Serialize(streamWriter, features);
                streamWriter.Flush();
            }
        }

        /// <summary>
        ///   Deserializes the cache.
        /// </summary>
        private void DeserializeCache()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (Stream stream = File.OpenRead(StorageLocation))
            {
                m_BackupFeatureList = (ConcurrentDictionary<FeatureKey, Feature>)formatter.Deserialize(stream);
            }

            m_ActiveFeatureList = new ConcurrentDictionary<FeatureKey, Feature>(m_BackupFeatureList);
        }
    }
}