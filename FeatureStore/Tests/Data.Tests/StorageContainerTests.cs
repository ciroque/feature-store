namespace Ciroque.Foundations.FeatureStore.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mutual;

    [TestClass]
    public abstract class StorageContainerTests
    {
        protected IStorageContainer m_StorageContainer;

        [TestMethod]
        public void MultipleFeaturesCanBeAddedToStorageAndQueriedIndividually()
        {
            const string featureNameBase = "Feature Name ";
            const int count = 5;
            Guid[] spaces = new Guid[count];
            Guid[] owners = new Guid[count];

            for (int index = 0; index < count; index++)
            {
                spaces[index] = Guid.NewGuid();
                owners[index] = Guid.NewGuid();

                Feature feature = Feature.Create(index, owners[index], spaces[index], featureNameBase + index);
                Feature stored = m_StorageContainer.Store(feature);

                Assert.AreEqual(feature.Id, stored.Id);
                Assert.AreEqual(feature.Space, stored.Space);
                Assert.AreEqual(feature.OwnerId, stored.OwnerId);
                Assert.AreEqual(feature.Name, stored.Name);
                Assert.IsFalse(stored.Enabled);
            }

            for (int index = 0; index < 5; index++)
            {
                Feature retrieved = m_StorageContainer.Retrieve(FeatureKey.Create(index, owners[index], spaces[index]));

                Assert.IsNotNull(retrieved);

                Assert.AreEqual(index, retrieved.Id);
                Assert.AreEqual(spaces[index], retrieved.Space);
                Assert.AreEqual(owners[index], retrieved.OwnerId);
                Assert.AreEqual(featureNameBase + index, retrieved.Name);
                Assert.IsFalse(retrieved.Enabled);
            }

            Assert.IsNull(m_StorageContainer.Retrieve(FeatureKey.Create(2112, Guid.Empty, Guid.Empty)));
        }

        [TestMethod]
        public void StorageIsQueryableBySpaceAndOwnerId()
        {
            Guid ownerId1 = Guid.NewGuid();
            Guid ownerId2 = Guid.NewGuid();
            Guid ownerId3 = Guid.NewGuid();

            Guid space1 = Guid.NewGuid();
            Guid space2 = Guid.NewGuid();
            Guid space3 = Guid.NewGuid();

            List<Feature> defined = new List<Feature>
                                        {
                                            Feature.Create(1, ownerId1, space1, "space1 ownerId1"),
                                            Feature.Create(1, ownerId1, space2, "space2 ownerId1"),
                                            Feature.Create(1, ownerId1, space3, "space3 ownerId1"),
                                            Feature.Create(1, ownerId2, space1, "space1 ownerId2"),
                                            Feature.Create(2, ownerId2, space1, "space1 ownerId2"),
                                            Feature.Create(3, ownerId2, space1, "space1 ownerId2"),
                                            Feature.Create(1, ownerId2, space2, "space2 ownerId2"),
                                            Feature.Create(1, ownerId2, space3, "space3 ownerId2"),
                                            Feature.Create(1, ownerId3, space1, "space1 ownerId3"),
                                            Feature.Create(1, ownerId3, space2, "space2 ownerId3"),
                                            Feature.Create(1, ownerId3, space3, "space3 ownerId3")
                                        };

            // CacheSwappingStorageContainer container = new CacheSwappingStorageContainer(@"MultipleQuery.dat");

            foreach (Feature feature in defined)
            {
                Feature stored = m_StorageContainer.Store(feature);

                Assert.AreEqual(feature.Id, stored.Id);
                Assert.AreEqual(feature.Space, stored.Space);
                Assert.AreEqual(feature.OwnerId, stored.OwnerId);
                Assert.AreEqual(feature.Name, stored.Name);
                Assert.AreEqual(feature.Enabled, stored.Enabled);
            }

            /* -- Assert Owner1 All Features -- */
            List<Feature> owner1Features =
                new List<Feature>(m_StorageContainer.Retrieve(FeatureScope.Create(ownerId1, Guid.Empty)));
            Assert.AreEqual(3, owner1Features.Count);
            CollectionAssert.Contains(owner1Features, defined[0]);
            CollectionAssert.Contains(owner1Features, defined[1]);
            CollectionAssert.Contains(owner1Features, defined[2]);

            /* -- Assert Owner2 All Features -- */
            List<Feature> owner2Features =
                new List<Feature>(m_StorageContainer.Retrieve(FeatureScope.Create(ownerId2, Guid.Empty)));
            Assert.AreEqual(5, owner2Features.Count);
            CollectionAssert.Contains(owner2Features, defined[3]);
            CollectionAssert.Contains(owner2Features, defined[4]);
            CollectionAssert.Contains(owner2Features, defined[5]);
            CollectionAssert.Contains(owner2Features, defined[6]);
            CollectionAssert.Contains(owner2Features, defined[7]);

            /* -- Assert Owner2 Space1 Features -- */
            List<Feature> owner2Space1Features =
                new List<Feature>(m_StorageContainer.Retrieve(FeatureScope.Create(ownerId2, space1)));
            Assert.AreEqual(3, owner2Space1Features.Count);
            CollectionAssert.Contains(owner2Space1Features, defined[3]);
            CollectionAssert.Contains(owner2Space1Features, defined[4]);
            CollectionAssert.Contains(owner2Space1Features, defined[5]);

            /* -- Assert Owner2 Space2 Features -- */
            List<Feature> owner2Space2Features =
                new List<Feature>(m_StorageContainer.Retrieve(FeatureScope.Create(ownerId2, space2)));
            Assert.AreEqual(1, owner2Space2Features.Count);
            CollectionAssert.Contains(owner2Space2Features, defined[6]);

            /* -- Assert Owner2 Space3 Features -- */
            List<Feature> owner2Space3Features =
                new List<Feature>(m_StorageContainer.Retrieve(FeatureScope.Create(ownerId2, space3)));
            Assert.AreEqual(1, owner2Space3Features.Count);
            CollectionAssert.Contains(owner2Space3Features, defined[7]);

            /* -- Assert Owner3 All Features -- */
            List<Feature> owner3Features =
                new List<Feature>(m_StorageContainer.Retrieve(FeatureScope.Create(ownerId3, Guid.Empty)));
            Assert.AreEqual(3, owner3Features.Count);
            CollectionAssert.Contains(owner3Features, defined[8]);
            CollectionAssert.Contains(owner3Features, defined[9]);
            CollectionAssert.Contains(owner3Features, defined[10]);

            /* -- Assert Owner3 Space1 Features -- */
            List<Feature> owner3Space1Features =
                new List<Feature>(m_StorageContainer.Retrieve(FeatureScope.Create(ownerId3, space1)));
            Assert.AreEqual(1, owner3Space1Features.Count);
            CollectionAssert.Contains(owner3Space1Features, defined[8]);

            /* -- Assert Owner2 Space2 Features -- */
            List<Feature> owner3Space2Features =
                new List<Feature>(m_StorageContainer.Retrieve(FeatureScope.Create(ownerId3, space2)));
            Assert.AreEqual(1, owner3Space2Features.Count);
            CollectionAssert.Contains(owner3Space2Features, defined[9]);

            /* -- Assert Owner2 Space3 Features -- */
            List<Feature> owner3Space3Features =
                new List<Feature>(m_StorageContainer.Retrieve(FeatureScope.Create(ownerId3, space3)));
            Assert.AreEqual(1, owner3Space3Features.Count);
            CollectionAssert.Contains(owner3Space3Features, defined[10]);
        }

        [TestMethod]
        public void UpdateFeature()
        {
            Feature feature = Feature.Create(2112, Guid.NewGuid(), Guid.NewGuid(), "To Be Updated");
            Feature stored = m_StorageContainer.Store(feature);

            Assert.IsFalse(feature.Enabled);
            Assert.IsFalse(stored.Enabled);

            stored.Enabled = true;

            Feature newStored = m_StorageContainer.Store(stored);

            Assert.IsNotNull(newStored);
            Assert.IsTrue(newStored.Enabled);

            Feature retrieved = m_StorageContainer.Retrieve(FeatureKey.Create(stored.Id, stored.OwnerId, stored.Space));

            Assert.IsNotNull(retrieved);
            Assert.IsTrue(retrieved.Enabled);
        }
    }
}