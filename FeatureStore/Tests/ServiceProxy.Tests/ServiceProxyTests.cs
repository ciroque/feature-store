namespace Ciroque.Foundations.ServiceProxy.Tests
{
    using System;
    using System.Threading;
    using FeatureStore.Mutual;
    using FeatureStore.Service;
    using FeatureStore.ServiceProxy;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    [DeploymentItem(@"log4net.config")]
    public class ServiceProxyTests
    {
        private static WcfService<FeatureStoreService> m_FeatureStoreService;
        private static readonly IFeatureStoreServiceProxy m_FeatureStoreServiceProxy = new FeatureStoreServiceProxy();

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            m_FeatureStoreService = new WcfService<FeatureStoreService>();
            m_FeatureStoreService.Start();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            m_FeatureStoreService.Dispose();
        }

        #region CheckFeatureState methods

        [TestMethod]
        public void ProxyCanCallCheckFeatureStateSynchronously()
        {
            CheckFeatureStateRequest request =
                BuildCheckFeatureStateRequestWithSavedFeature("ProxyCanCallCheckFeatureStateSynchronously");
            CheckFeatureStateResponse response = m_FeatureStoreServiceProxy.CheckFeatureState(request);

            Assert.IsNotNull(response.Result);
            Assert.AreEqual(request.Key.Id, response.Result.Id);
            Assert.AreEqual(request.Key.OwnerId, response.Result.OwnerId);
            Assert.AreEqual(request.Key.Space, response.Result.Space);
        }

        [TestMethod]
        public void ProxyCanCallCheckFeatureStateAsynchronouslyWithPolling()
        {
            const string messageId = "ProxyCanCallCheckFeatureStateAsynchronouslyWithPolling";
            CheckFeatureStateRequest request = BuildCheckFeatureStateRequestWithSavedFeature(messageId);
            IAsyncResult result = m_FeatureStoreServiceProxy.BeginCheckFeatureState(request, null, null);
            while (!result.IsCompleted)
            {
            }

            CheckFeatureStateResponse response = m_FeatureStoreServiceProxy.EndCheckFeatureState(result);

            AssertCheckFeatureStateValues(request, response);
        }

        [TestMethod]
        public void ProxyCanCallCheckFeatureStateAsynchronouslyWithBlocking()
        {
            const string messageId = "ProxyCanCallCheckFeatureStateAsynchronouslyWithPolling";
            CheckFeatureStateRequest request = BuildCheckFeatureStateRequestWithSavedFeature(messageId);
            IAsyncResult result = m_FeatureStoreServiceProxy.BeginCheckFeatureState(request, null, null);

            result.AsyncWaitHandle.WaitOne();

            CheckFeatureStateResponse response = m_FeatureStoreServiceProxy.EndCheckFeatureState(result);

            AssertCheckFeatureStateValues(request, response);
        }

        [TestMethod]
        public void ProxyCanCallCheckFeatureStateAsynchronouslyWithCallback()
        {
            const string messageId = "ProxyCanCallCheckFeatureStateAsynchronouslyWithPolling";
            ManualResetEvent resetEvent = new ManualResetEvent(false);
            CheckFeatureStateRequest request = BuildCheckFeatureStateRequestWithSavedFeature(messageId);
            CheckFeatureStateResponse response = null;

            m_FeatureStoreServiceProxy.BeginCheckFeatureState(
                request,
                r =>
                    {
                        response = m_FeatureStoreServiceProxy.EndCheckFeatureState(r);
                        resetEvent.Set();
                    },
                null);

            resetEvent.WaitOne();

            AssertCheckFeatureStateValues(request, response);
        }

        private static void AssertCheckFeatureStateValues(CheckFeatureStateRequest request,
                                                          CheckFeatureStateResponse response)
        {
            Assert.IsNotNull(response.Result);
            Assert.AreEqual(request.Key.Id, response.Result.Id);
            Assert.AreEqual(request.Key.OwnerId, response.Result.OwnerId);
            Assert.AreEqual(request.Key.Space, response.Result.Space);
        }

        private static CheckFeatureStateRequest BuildCheckFeatureStateRequestWithSavedFeature(string messageId)
        {
            Feature feature = Feature.Create(1, Guid.NewGuid(), Guid.NewGuid(), "CheckFeatureStateFeature");
            AddFeatureToStorage(messageId, feature);
            return CheckFeatureStateRequest.Create(messageId,
                                                   FeatureKey.Create(feature.Id, feature.OwnerId, feature.Space));
        }

        #endregion

        #region CreateFeature methods

        [TestMethod]
        public void ProxyCanCallCreateFeatureSynchronously()
        {
            const string messageId = "ProxyCanCallCreateFeatureSynchronously";
            Feature feature = Feature.Create(1, Guid.NewGuid(), Guid.NewGuid(), "Testing 1-2-3");

            CreateFeatureResponse response =
                m_FeatureStoreServiceProxy.CreateFeature(
                    CreateFeatureRequest.Create(messageId, feature));

            Assert.IsNotNull(response.Result);
            Assert.AreEqual(messageId, response.Header.MessageId);
            Assert.AreEqual(feature.Enabled, response.Result.Enabled);
            Assert.AreEqual(feature.Id, response.Result.Id);
            Assert.AreEqual(feature.Name, response.Result.Name);
            Assert.AreEqual(feature.OwnerId, response.Result.OwnerId);
            Assert.AreEqual(feature.Space, response.Result.Space);
        }

        [TestMethod]
        public void ProxyCanCallCreateFeatureAsynchronouslyWithPolling()
        {
            const string messageId = "ProxyCanCallCreateFeatureAsynchronouslyWithPolling";
            CreateFeatureRequest request = BuildCreateFeatureRequest(messageId);
            IAsyncResult result = m_FeatureStoreServiceProxy.BeginCreateFeature(request, null, null);
            while (!result.IsCompleted)
            {
            }

            CreateFeatureResponse response = m_FeatureStoreServiceProxy.EndCreateFeature(result);

            AssertCreateFeatureValues(request, response);
        }

        [TestMethod]
        public void ProxyCanCallCreateFeatureAsynchronouslyWithBlocking()
        {
            const string messageId = "ProxyCanCallCreateFeatureAsynchronouslyWithBlocking";
            CreateFeatureRequest request = BuildCreateFeatureRequest(messageId);
            IAsyncResult result = m_FeatureStoreServiceProxy.BeginCreateFeature(request, null, null);

            result.AsyncWaitHandle.WaitOne();

            CreateFeatureResponse response = m_FeatureStoreServiceProxy.EndCreateFeature(result);

            AssertCreateFeatureValues(request, response);
        }

        [TestMethod]
        public void ProxyCanCallCreateFeatureAsynchronouslyWithCallback()
        {
            const string messageId = "ProxyCanCallCreateFeatureAsynchronouslyWithCallback";
            ManualResetEvent resetEvent = new ManualResetEvent(false);
            CreateFeatureRequest request = BuildCreateFeatureRequest(messageId);
            CreateFeatureResponse response = null;

            m_FeatureStoreServiceProxy.BeginCreateFeature(
                request,
                r =>
                    {
                        try
                        {
                            response = m_FeatureStoreServiceProxy.EndCreateFeature(r);
                            resetEvent.Set();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    },
                null);

            resetEvent.WaitOne();

            AssertCreateFeatureValues(request, response);
        }

        private static void AssertCreateFeatureValues(CreateFeatureRequest request, CreateFeatureResponse response)
        {
            Assert.AreEqual(request.Header.MessageId, response.Header.MessageId);
            Assert.AreEqual(request.Feature.Enabled, response.Result.Enabled);
            Assert.AreEqual(request.Feature.Id, response.Result.Id);
            Assert.AreEqual(request.Feature.Name, response.Result.Name);
            Assert.AreEqual(request.Feature.OwnerId, response.Result.OwnerId);
            Assert.AreEqual(request.Feature.Space, response.Result.Space);
        }

        private static CreateFeatureRequest BuildCreateFeatureRequest(string messageId)
        {
            return CreateFeatureRequest.Create(
                messageId,
                Feature.Create(
                    1,
                    Guid.NewGuid(),
                    Guid.NewGuid(), Guid.NewGuid().ToString()));
        }

        #endregion

        #region UpdateFeatureState methods

        [TestMethod]
        public void ProxyCanCallUpdateFeatureStateSynchronously()
        {
            const string messageId = "ProxyCanCallUpdateFeatureStateSynchronously";
            UpdateFeatureStateRequest request = BuildUpdateFeatureStateRequestWithSavedFeature(messageId);
            UpdateFeatureStateResponse response = m_FeatureStoreServiceProxy.UpdateFeatureState(request);
            AssertUpdateFeatureStateValues(request, response);
        }

        [TestMethod]
        public void ProxyCanCallUpdateFeatureStateAsynchronouslyWithPolling()
        {
            const string messageId = "ProxyCanCallUpdateFeatureStateAsynchronouslyWithPolling";
            UpdateFeatureStateRequest request = BuildUpdateFeatureStateRequestWithSavedFeature(messageId);
            IAsyncResult result = m_FeatureStoreServiceProxy.BeginUpdateFeatureState(request, null, null);
            while (!result.IsCompleted)
            {
            }

            UpdateFeatureStateResponse response = m_FeatureStoreServiceProxy.EndUpdateFeatureState(result);

            AssertUpdateFeatureStateValues(request, response);
        }

        [TestMethod]
        public void ProxyCanCallUpdateFeatureStateAsynchronouslyWithBlocking()
        {
            const string messageId = "ProxyCanCallUpdateFeatureStateAsynchronouslyWithBlocking";
            UpdateFeatureStateRequest request = BuildUpdateFeatureStateRequestWithSavedFeature(messageId);
            IAsyncResult result = m_FeatureStoreServiceProxy.BeginUpdateFeatureState(request, null, null);

            result.AsyncWaitHandle.WaitOne();

            UpdateFeatureStateResponse response = m_FeatureStoreServiceProxy.EndUpdateFeatureState(result);

            AssertUpdateFeatureStateValues(request, response);
        }

        [TestMethod]
        public void ProxyCanCallUpdateFeatureStateAsynchronouslyWithCallback()
        {
            const string messageId = "ProxyCanCallUpdateFeatureStateAsynchronouslyWithCallback";
            ManualResetEvent resetEvent = new ManualResetEvent(false);
            UpdateFeatureStateResponse response = null;
            UpdateFeatureStateRequest request = BuildUpdateFeatureStateRequestWithSavedFeature(messageId);

            m_FeatureStoreServiceProxy.BeginUpdateFeatureState(
                request,
                r =>
                    {
                        response = m_FeatureStoreServiceProxy.EndUpdateFeatureState(r);
                        resetEvent.Set();
                    },
                null);

            resetEvent.WaitOne();

            AssertUpdateFeatureStateValues(request, response);
        }

        private static UpdateFeatureStateRequest BuildUpdateFeatureStateRequestWithSavedFeature(string messageId)
        {
            Feature feature = Feature.Create(2, Guid.NewGuid(), Guid.NewGuid(), "UpdateFeatureStateFeature");
            AddFeatureToStorage(messageId, feature);
            return UpdateFeatureStateRequest.Create(
                messageId,
                FeatureKey.Create(feature.Id, feature.OwnerId, feature.Space),
                !feature.Enabled);
        }

        private static void AssertUpdateFeatureStateValues(UpdateFeatureStateRequest request,
                                                           UpdateFeatureStateResponse response)
        {
            Assert.AreEqual(request.Header.MessageId, response.Header.MessageId);
            Assert.AreEqual(request.Key.Id, response.Result.Id);
            Assert.AreEqual(request.Key.OwnerId, response.Result.OwnerId);
            Assert.AreEqual(request.Key.Space, response.Result.Space);
            Assert.AreEqual(request.NewState, response.Result.Enabled);
        }

        private static void AddFeatureToStorage(string messageId, Feature feature)
        {
            m_FeatureStoreServiceProxy.CreateFeature(CreateFeatureRequest.Create(messageId, feature));
        }

        #endregion

        #region RetrieveDefinedFeatures methods

        [TestMethod]
        public void ProxyCanCallRetrieveDefinedFeaturesSynchronously()
        {
            const string messageId = "ProxyCanCallRetrieveDefinedFeaturesSynchronously";
            RetrieveDefinedFeaturesRequest request = BuildRetrieveDefinedFeaturesRequestWithSavedFeature(messageId);
            RetrieveDefinedFeaturesResponse response = m_FeatureStoreServiceProxy.RetrieveDefinedFeatures(request);
            AssertRetrieveDefinedFeaturesValues(request, response);
        }

        [TestMethod]
        public void ProxyCanCallRetrieveDefinedFeaturesAsynchronouslyWithPolling()
        {
            const string messageId = "ProxyCanCallRetrieveDefinedFeaturesAsynchronouslyWithPolling";
            RetrieveDefinedFeaturesRequest request = BuildRetrieveDefinedFeaturesRequestWithSavedFeature(messageId);
            IAsyncResult result = m_FeatureStoreServiceProxy.BeginRetrieveDefinedFeatures(request, null, null);
            while (!result.IsCompleted)
            {
            }

            RetrieveDefinedFeaturesResponse response = m_FeatureStoreServiceProxy.EndRetrieveDefinedFeatures(result);

            AssertRetrieveDefinedFeaturesValues(request, response);
        }

        [TestMethod]
        public void ProxyCanCallRetrieveDefinedFeaturesAsynchronouslyWithBlocking()
        {
            const string messageId = "ProxyCanCallRetrieveDefinedFeaturesAsynchronouslyWithBlocking";
            RetrieveDefinedFeaturesRequest request = BuildRetrieveDefinedFeaturesRequestWithSavedFeature(messageId);
            IAsyncResult result = m_FeatureStoreServiceProxy.BeginRetrieveDefinedFeatures(request, null, null);

            result.AsyncWaitHandle.WaitOne();

            RetrieveDefinedFeaturesResponse response = m_FeatureStoreServiceProxy.EndRetrieveDefinedFeatures(result);

            AssertRetrieveDefinedFeaturesValues(request, response);
        }

        [TestMethod]
        public void ProxyCanCallRetrieveDefinedFeaturesAsynchronouslyWithCallback()
        {
            const string messageId = "ProxyCanCallRetrieveDefinedFeaturesAsynchronouslyWithCallback";
            RetrieveDefinedFeaturesRequest request = BuildRetrieveDefinedFeaturesRequestWithSavedFeature(messageId);
            RetrieveDefinedFeaturesResponse response = null;
            ManualResetEvent resetEvent = new ManualResetEvent(false);
            m_FeatureStoreServiceProxy.BeginRetrieveDefinedFeatures(
                request,
                r =>
                    {
                        response = m_FeatureStoreServiceProxy.EndRetrieveDefinedFeatures(r);
                        resetEvent.Set();
                    },
                null);

            resetEvent.WaitOne();

            AssertRetrieveDefinedFeaturesValues(request, response);
        }

        private static void AssertRetrieveDefinedFeaturesValues(RetrieveDefinedFeaturesRequest request,
                                                                RetrieveDefinedFeaturesResponse response)
        {
            Assert.AreEqual(request.Header.MessageId, response.Header.MessageId);
            foreach (Feature feature in response.Result)
            {
                Assert.AreEqual(request.FeatureScope.OwnerId, feature.OwnerId);
                Assert.AreEqual(request.FeatureScope.Space, feature.Space);
            }
        }

        private static RetrieveDefinedFeaturesRequest BuildRetrieveDefinedFeaturesRequestWithSavedFeature(
            string messageId)
        {
            Guid ownerGuid = Guid.NewGuid();
            Guid space = Guid.NewGuid();

            Feature[] features = new[]
                                     {
                                         Feature.Create(3, ownerGuid, space, "RetrieveDefinedFeaturesFeature3"),
                                         Feature.Create(4, ownerGuid, space, "RetrieveDefinedFeaturesFeature4"),
                                         Feature.Create(5, ownerGuid, space, "RetrieveDefinedFeaturesFeature5")
                                     };
            foreach (Feature feature in features)
            {
                AddFeatureToStorage(feature.Name, feature);
            }

            return RetrieveDefinedFeaturesRequest.Create(messageId, FeatureScope.Create(ownerGuid, space));
        }

        #endregion
    }
}