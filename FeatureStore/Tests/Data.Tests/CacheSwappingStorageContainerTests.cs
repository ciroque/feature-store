namespace Ciroque.Foundations.FeatureStore.Data.Tests
{
    using System;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mutual;

    [TestClass]
    public class CacheSwappingStorageContainerTests : StorageContainerTests
    {
        private const string StorageFilename = @".\StorageFile.dat";

        public CacheSwappingStorageContainerTests()
        {
            m_StorageContainer = new CacheSwappingStorageContainer(StorageFilename);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            if (File.Exists(StorageFilename))
            {
                File.Delete(StorageFilename);
            }
        }

        [TestMethod]
        [DeploymentItem(@"features.dat")]
        public void ExistingFeatureStorageFileIsLoaded()
        {
            CacheSwappingStorageContainer container = new CacheSwappingStorageContainer(@"features.dat");
            Feature feature =
                container.Retrieve(
                    FeatureKey.Create(
                        5,
                        new Guid("a7fd39ea-95a1-48d3-9c5e-0aee8045f3f7"),
                        new Guid("275f5020-b4ec-4f6c-90d7-798999c8d932")));

            Assert.IsNotNull(feature);
            Assert.AreEqual("Feature 5", feature.Name);
            Assert.IsFalse(feature.Enabled);
        }
    }
}