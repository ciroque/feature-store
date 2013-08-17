namespace Ciroque.Foundations.FeatureStore.Service.Tests
{
    using System;
    using System.ServiceModel;
    using Core;
    using Data;
    using Instrumentation.Tests;
    using Mutual;
    using Rhino.Mocks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class WcfServiceImplementationTests
    {
        private readonly MockRepository m_MockRepository = new MockRepository();

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            PerformanceCounterCategoryTestManager.Initialize();
        }

        [ClassCleanup]
        public static void ClassCleanUp()
        {
            PerformanceCounterCategoryTestManager.CleanUp();
        }

        [TestMethod]
        public void ServiceCanBeStartedAndStopped()
        {
            using (WcfService<FeatureStoreService> service = new WcfService<FeatureStoreService>())
            {
                service.Start();
            }
        }

        [TestMethod]
        public void CreateFeatureThrowsFeatureStoreFaultExceptionOnException()
        {
            IStorageContainer storageContainer = m_MockRepository.StrictMock<IStorageContainer>();
            Feature feature = Feature.Create(1, Guid.NewGuid(), Guid.NewGuid(),
                                             "CreateFeatureThrowsCreateFeatureFaultOnException");

            using (m_MockRepository.Record())
            {
                Expect.Call(storageContainer.Retrieve(FeatureKey.Create(feature.Id, feature.OwnerId, feature.Space))).
                    Throw(new CreateFeatureException("Bad Mojo Exception"));
                m_MockRepository.ReplayAll();

                FeatureStoreService featureStoreService = new FeatureStoreService(storageContainer);

                try
                {
                    featureStoreService.CreateFeature(
                        CreateFeatureRequest.Create(
                            "CreateFeatureThrowsCreateFeatureFaultOnException",
                            feature));

                    Assert.Fail("Expecting FaultException<FeatureStoreFault>");
                }
                catch (FaultException<FeatureStoreFault> e)
                {
                    Console.WriteLine(e.Detail.Message);
                    Console.WriteLine(e.Message);
                    StringAssert.Contains(e.Detail.Message, "Bad Mojo Exception");
                }

                m_MockRepository.VerifyAll();
            }
        }

        [TestMethod]
        public void CheckFeatureStateThrowsFeatureStoreFaultExceptionOnException()
        {
            IStorageContainer storageContainer = m_MockRepository.StrictMock<IStorageContainer>();
            FeatureKey key = FeatureKey.Create(1, Guid.NewGuid(), Guid.NewGuid());

            using (m_MockRepository.Record())
            {
                Expect.Call(storageContainer.Retrieve(FeatureKey.Create(key.Id, key.OwnerId, key.Space))).Throw(
                    new CheckFeatureStateException("Bad Mojo Exception"));
                m_MockRepository.ReplayAll();

                FeatureStoreService featureStoreService = new FeatureStoreService(storageContainer);

                try
                {
                    featureStoreService.CheckFeatureState(
                        CheckFeatureStateRequest.Create(
                            "CheckFeatureStateThrowsFeatureStoreFaultExceptionOnException",
                            key));

                    Assert.Fail("Expecting FaultException<FeatureStoreFault>");
                }
                catch (FaultException<FeatureStoreFault> e)
                {
                    Console.WriteLine(e.Detail.Message);
                    Console.WriteLine(e.Message);
                    StringAssert.Contains(e.Detail.Message,
                                          "An exception occurred querying the data store for the Feature.");
                }

                m_MockRepository.VerifyAll();
            }
        }

        [TestMethod]
        public void UpdateFeatureStateThrowsFeatureStoreFaultExceptionOnException()
        {
            IStorageContainer storageContainer = m_MockRepository.StrictMock<IStorageContainer>();
            FeatureKey key = FeatureKey.Create(1, Guid.NewGuid(), Guid.NewGuid());

            using (m_MockRepository.Record())
            {
                Expect.Call(storageContainer.Retrieve(FeatureKey.Create(key.Id, key.OwnerId, key.Space))).Throw(
                    new CheckFeatureStateException("Bad Mojo Exception"));
                m_MockRepository.ReplayAll();

                FeatureStoreService featureStoreService = new FeatureStoreService(storageContainer);

                try
                {
                    featureStoreService.UpdateFeatureState(
                        UpdateFeatureStateRequest.Create(
                            "UpdateFeatureStateThrowsFeatureStoreFaultExceptionOnException",
                            key,
                            true));

                    Assert.Fail("Expecting FaultException<FeatureStoreFault>");
                }
                catch (FaultException<FeatureStoreFault> e)
                {
                    Console.WriteLine(e.Detail.Message);
                    Console.WriteLine(e.Message);
                    StringAssert.Contains(e.Detail.Message, "An exception occurred updating the Feature.");
                }

                m_MockRepository.VerifyAll();
            }
        }

        [TestMethod]
        public void RetrieveDefinedFeaturesThrowsFeatureStoreFaultExceptionOnException()
        {
            IStorageContainer storageContainer = m_MockRepository.StrictMock<IStorageContainer>();
            FeatureScope scope = FeatureScope.Create(Guid.NewGuid(), Guid.NewGuid());

            using (m_MockRepository.Record())
            {
                Expect.Call(storageContainer.Retrieve(scope)).Throw(new CheckFeatureStateException("Bad Mojo Exception"));
                m_MockRepository.ReplayAll();

                FeatureStoreService featureStoreService = new FeatureStoreService(storageContainer);

                try
                {
                    featureStoreService.RetrieveDefinedFeatures(
                        RetrieveDefinedFeaturesRequest.Create(
                            "UpdateFeatureStateThrowsFeatureStoreFaultExceptionOnException",
                            scope));

                    Assert.Fail("Expecting FaultException<FeatureStoreFault>");
                }
                catch (FaultException<FeatureStoreFault> e)
                {
                    Console.WriteLine(e.Detail.Message);
                    Console.WriteLine(e.Message);
                    StringAssert.Contains(e.Detail.Message, "An exception occurred retrieving defined Features.");
                }

                m_MockRepository.VerifyAll();
            }
        }
    }
}