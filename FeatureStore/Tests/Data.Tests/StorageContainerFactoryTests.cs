namespace Ciroque.Foundations.FeatureStore.Data.Tests
{
    using System;
    using System.Configuration;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class StorageContainerFactoryTests
    {
        [TestMethod]
        public void CacheSwappingStorageContainerCreatedBasedOnConfigurationSection()
        {
            CreateAndAssert("featureStore.StorageContainer-CacheSwapping", "CacheSwapping",
                            @".\CacheSwappingStorageFile.dat", typeof (CacheSwappingStorageContainer));
        }

        [TestMethod]
        public void InMemoryStorageContainerCreatedBasedOnConfigurationSection()
        {
            CreateAndAssert("featureStore.StorageContainer-InMemory", "InMemory", string.Empty,
                            typeof (InMemoryStorageContainer));
        }

        [TestMethod]
        public void SqlServerStorageContainerCreatedBasedOnConfigurationSection()
        {
            CreateAndAssert("featureStore.StorageContainer-SqlServer", "SqlServer", string.Empty,
                            typeof (SqlServerStorageContainer));
        }

        [TestMethod]
        public void InvalidContainerTypeKeyExceptionThrownOnUnkonwnConfigurationValue()
        {
            try
            {
                CreateAndAssert("featureStore.StorageContainer-Invalid", "Invalid", string.Empty,
                                typeof (SqlServerStorageContainer));
                Assert.Fail("Expected a InvalidContainerTypeKeyException to be thrown.");
            }
            catch (InvalidContainerTypeKeyException)
            {
            }
        }

        private static void CreateAndAssert(string configSectionName, string expectedContainerTypeKey,
                                            string expectedStorageLocation, Type expectedImplementationType)
        {
            StorageContainerConfigurationSection storageContainerConfigurationSection =
                (StorageContainerConfigurationSection) ConfigurationManager.GetSection(configSectionName);

            Assert.AreEqual(expectedContainerTypeKey, storageContainerConfigurationSection.ContainerTypeKey);
            Assert.AreEqual(expectedStorageLocation, storageContainerConfigurationSection.StorageLocation);

            IStorageContainer storageContainer = StorageContainerFactory.Create(storageContainerConfigurationSection);

            Assert.IsInstanceOfType(storageContainer, expectedImplementationType);
        }
    }
}