// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StorageContainerFactory.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Responsible for creating IStorageContainer implementation instances.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.Data
{
    /// <summary>
    ///   Responsible for creating IStorageContainer implementation instances.
    /// </summary>
    public static class StorageContainerFactory
    {
        /// <summary>
        ///   Creates an instance of the storage container specified in the specified configuration section.
        /// </summary>
        /// <param name = "storageContainerConfigurationSection">The storage container configuration section.</param>
        /// <returns>An <see cref = "IStorageContainer" /> implementation instance.</returns>
        /// <exception cref = "InvalidContainerTypeKeyException"> thrown when the ContainerTypeKey property of the specified <see cref = "StorageContainerConfigurationSection" />
        /// is invalid.</exception>
        public static IStorageContainer Create(StorageContainerConfigurationSection storageContainerConfigurationSection)
        {
            switch (storageContainerConfigurationSection.ContainerTypeKey)
            {
                case "CacheSwapping":
                    {
                        return new CacheSwappingStorageContainer(storageContainerConfigurationSection.StorageLocation);
                    }

                case "InMemory":
                    {
                        return new InMemoryStorageContainer();
                    }

                case "SqlServer":
                    {
                        return new SqlServerStorageContainer(storageContainerConfigurationSection.StorageLocation);
                    }
            }

            throw new InvalidContainerTypeKeyException(storageContainerConfigurationSection.ContainerTypeKey);
        }
    }
}