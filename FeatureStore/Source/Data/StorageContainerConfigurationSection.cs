// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StorageContainerConfigurationSection.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Represents the featureStore.StorageContainer section within a configuration file.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.Data
{
    using System.Configuration;

    /// <summary>
    ///   Represents the featureStore.StorageContainer section within a configuration file.
    /// </summary>
    public class StorageContainerConfigurationSection : ConfigurationSection
    {
        /// <summary>
        ///   The key used as an index into the configuration section to extract the Container Type Key.
        /// </summary>
        private const string ContainerTypeConfigurationKey = "containerType";

        /// <summary>
        ///   The key used as an index into the configuration section to extract the Storage Location.
        /// </summary>
        private const string StorageLocationConfigurationKey = "storageLocation";

        /// <summary>
        ///   Gets the container key.
        /// </summary>
        /// <value>The container key.</value>
        [ConfigurationProperty(ContainerTypeConfigurationKey, DefaultValue = "InMemory", IsRequired = true)]
        public string ContainerTypeKey
        {
            get
            {
                return (string)this[ContainerTypeConfigurationKey];
            }
        }

        /// <summary>
        ///   Gets the storage location.
        /// </summary>
        /// <value>The storage location.</value>
        [ConfigurationProperty(StorageLocationConfigurationKey, DefaultValue = "", IsRequired = true)]
        public string StorageLocation
        {
            get
            {
                return (string)this[StorageLocationConfigurationKey];
            }
        }
    }
}