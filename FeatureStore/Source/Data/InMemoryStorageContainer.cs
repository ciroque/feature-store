// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InMemoryStorageContainer.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   A simple in-memory implementation of the <see cref="IStorageContainer" /> interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.Data
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using Mutual;

    /// <summary>
    ///   A simple in-memory implementation of the <see cref = "IStorageContainer" /> interface.
    /// </summary>
    public class InMemoryStorageContainer : IStorageContainer
    {
        /// <summary>
        ///   The Feature list.
        /// </summary>
        private readonly ConcurrentDictionary<FeatureKey, Feature> m_FeatureList;

        /// <summary>
        ///   Initializes a new instance of the <see cref = "InMemoryStorageContainer" /> class.
        /// </summary>
        public InMemoryStorageContainer()
        {
            m_FeatureList = new ConcurrentDictionary<FeatureKey, Feature>();
        }

        #region IStorageContainer Members

        /// <summary>
        ///   Stores the specified entity.
        /// </summary>
        /// <param name = "feature">The entity to be stored.</param>
        /// <returns>The entity that was saved.</returns>
        public Feature Store(Feature feature)
        {
            FeatureKey featureKey = FeatureKey.Create(feature.Id, feature.OwnerId, feature.Space);
            m_FeatureList.AddOrUpdate(featureKey, key => feature, (key, original) => feature);
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
            if (m_FeatureList.ContainsKey(key))
            {
                return m_FeatureList[key];
            }

            return null;
        }

        /// <summary>
        ///   Retrieves the specified feature space.
        /// </summary>
        /// <param name = "featureScope">The feature space.</param>
        /// <returns>
        ///   An IEnumerable containing the results of the query.
        /// </returns>
        public IList<Feature> Retrieve(FeatureScope featureScope)
        {
            return m_FeatureList.Values
                .Where(entry =>
                       (featureScope.OwnerId == Guid.Empty || featureScope.OwnerId == entry.OwnerId)
                       && (featureScope.Space == Guid.Empty || featureScope.Space == entry.Space)).ToList();
        }

        #endregion
    }
}