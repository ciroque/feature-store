// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FeatureStoreTests.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Defines the FeatureStoreTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    using Ciroque.Foundations.FeatureStore.Data;
    using Ciroque.Foundations.FeatureStore.Instrumentation;
    using Ciroque.Foundations.FeatureStore.Mutual;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Rhino.Mocks;

    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    [TestClass]
    public class FeatureStoreTests
    {
        /// <summary>
        /// 
        /// </summary>
        protected const string FeatureName = "This is the feature name";
        
        /// <summary>
        /// 
        /// </summary>
        protected MockRepository m_MockRepository = new MockRepository();

        /// <summary>
        /// 
        /// </summary>
        protected IStorageContainer m_StorageContainer;

        #region Initialize / CleanUp

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        /// <remarks></remarks>
        public FeatureStoreTests()
        {
            PerformanceCounterRegistrar.EnsureExist();
            Debug.WriteLine("PerformanceCounterRegistrar.EnsureExist");
            m_StorageContainer = m_MockRepository.StrictMock<IStorageContainer>();
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the <see cref="FeatureStoreTests"/> is reclaimed by garbage collection.
        /// </summary>
        /// <remarks></remarks>
        ~FeatureStoreTests()
        {
            PerformanceCounterRegistrar.Remove();
            Debug.WriteLine("PerformanceCounterRegistrar.Remove");
        }

        #endregion

        #region CheckFeatureState tests

        /// <summary>
        /// Queries the exist ding feature.
        /// </summary>
        /// <remarks></remarks>
        [TestMethod]
        public void QueryExistingFeature()
        {
            string messageId = Guid.NewGuid().ToString();
            Guid space = Guid.NewGuid();
            Guid owner = Guid.NewGuid();
            FeatureKey query = FeatureKey.Create(1, owner, space);
            CheckFeatureStateRequest request = CheckFeatureStateRequest.Create(messageId, query);
            Feature foundFeature = Feature.Create(1, owner, space, FeatureName);
            StandardFeatureStore standardFeatureStore = new StandardFeatureStore(m_StorageContainer);

            using (m_MockRepository.Record())
            {
                Expect.Call(m_StorageContainer.Retrieve(query)).Return(foundFeature);

                m_MockRepository.ReplayAll();

                CheckFeatureStateResponse response = standardFeatureStore.CheckFeatureState(request);

                Assert.AreEqual(messageId, response.Header.MessageId);
                Assert.IsNotNull(response);
                Assert.IsFalse(response.Result.Enabled);
                m_MockRepository.VerifyAll();
            }
        }

        /// <summary>
        /// Nons the existent feature results in null result.
        /// </summary>
        /// <remarks></remarks>
        [TestMethod]
        public void NonExistentFeatureResultsInNullResult()
        {
            string messageId = Guid.NewGuid().ToString();
            FeatureKey query = FeatureKey.Create(-1, Guid.Empty, Guid.Empty);
            CheckFeatureStateRequest request = CheckFeatureStateRequest.Create(messageId, query);
            StandardFeatureStore standardFeatureStore = new StandardFeatureStore(m_StorageContainer);

            using (m_MockRepository.Record())
            {
                Expect.Call(m_StorageContainer.Retrieve(query)).Return(null);

                m_MockRepository.ReplayAll();

                CheckFeatureStateResponse response = standardFeatureStore.CheckFeatureState(request);

                Assert.AreEqual(messageId, response.Header.MessageId);
                Assert.IsNull(response.Result);
                m_MockRepository.VerifyAll();
            }
        }

        /// <summary>
        /// Exceptions the is raised as check feature state exception.
        /// </summary>
        /// <remarks></remarks>
        [TestMethod]
        public void ExceptionIsRaisedAsCheckFeatureStateException()
        {
            string messageId = Guid.NewGuid().ToString();
            Exception exception = m_MockRepository.StrictMock<Exception>();
            FeatureKey query = FeatureKey.Create(-1, Guid.Empty, Guid.Empty);
            CheckFeatureStateRequest request = CheckFeatureStateRequest.Create(messageId, query);
            StandardFeatureStore standardFeatureStore = new StandardFeatureStore(m_StorageContainer);

            using (m_MockRepository.Record())
            {
                Expect.Call(exception.Message).Return("Bad Mojo");
                Expect.Call(m_StorageContainer.Retrieve(query)).Throw(exception);
                m_MockRepository.ReplayAll();

                try
                {
                    standardFeatureStore.CheckFeatureState(request);
                    Assert.Fail("Expected CheckFeatureStateException");
                }
                catch (CheckFeatureStateException e)
                {
                    StringAssert.Contains(e.Message, "Id " + query.Id);
                    StringAssert.Contains(e.Message, "Space " + query.Space);
                }

                m_MockRepository.VerifyAll();
            }
        }

        #endregion

        #region CreateFeature tests

        /// <summary>
        /// Creates the feature.
        /// </summary>
        /// <remarks></remarks>
        [TestMethod]
        public void CreateFeature()
        {
            Feature toCreate = Feature.Create(1, Guid.NewGuid(), Guid.NewGuid(), FeatureName);

            IStorageContainer container = m_MockRepository.StrictMock<IStorageContainer>();
            string messageId = Guid.NewGuid().ToString();

            using (m_MockRepository.Record())
            {
                Expect.Call(container.Retrieve(FeatureKey.Create(toCreate.Id, toCreate.OwnerId, toCreate.Space))).Return
                    (null);
                Expect.Call(container.Store(toCreate)).Return(toCreate);
                m_MockRepository.ReplayAll();

                StandardFeatureStore service = new StandardFeatureStore(container);

                CreateFeatureRequest request = CreateFeatureRequest.Create(messageId, toCreate);

                CreateFeatureResponse response = service.CreateFeature(request);

                Assert.AreEqual(messageId, response.Header.MessageId);
                Assert.AreEqual(toCreate.Id, response.Result.Id);
                Assert.AreEqual(toCreate.Name, response.Result.Name);
                Assert.AreEqual(toCreate.Space, response.Result.Space);
                Assert.AreEqual(toCreate.OwnerId, response.Result.OwnerId);

                m_MockRepository.VerifyAll();
            }
        }

        /// <summary>
        /// Creates the feature exception thrown when creation fails.
        /// </summary>
        /// <remarks></remarks>
        [TestMethod]
        public void CreateFeatureExceptionThrownWhenCreationFails()
        {
            string messageId = Guid.NewGuid().ToString();
            Exception exception = m_MockRepository.StrictMock<Exception>();

            Feature toCreate = Feature.Create(1, Guid.NewGuid(), Guid.NewGuid(), FeatureName);
            CreateFeatureRequest request = CreateFeatureRequest.Create(messageId, toCreate);

            IFeatureStore featureStore = new StandardFeatureStore(m_StorageContainer);

            using (m_MockRepository.Record())
            {
                Expect.Call(exception.Message).Return("Bad Mojo");
                Expect.Call(m_StorageContainer.Retrieve(FeatureKey.Create(toCreate.Id, toCreate.OwnerId, toCreate.Space)))
                    .Return(null);
                Expect.Call(m_StorageContainer.Store(toCreate)).Throw(exception);
                m_MockRepository.ReplayAll();

                try
                {
                    featureStore.CreateFeature(request);
                    Assert.Fail("Expected FeatureCreationException");
                }
                catch (CreateFeatureException)
                {
                }

                m_MockRepository.VerifyAll();
            }
        }

        /// <summary>
        /// Creates the feature fails when duplicate key provided.
        /// </summary>
        /// <remarks></remarks>
        [TestMethod]
        public void CreateFeatureFailsWhenDuplicateKeyProvided()
        {
            string messageId = Guid.NewGuid().ToString();
            Feature toCreate = Feature.Create(1, Guid.NewGuid(), Guid.NewGuid(), FeatureName);

            IFeatureStore featureStore = new StandardFeatureStore(m_StorageContainer);

            using (m_MockRepository.Record())
            {
                Expect.Call(m_StorageContainer.Retrieve(FeatureKey.Create(toCreate.Id, toCreate.OwnerId, toCreate.Space)))
                    .Return(toCreate);
                m_MockRepository.ReplayAll();

                try
                {
                    featureStore.CreateFeature(CreateFeatureRequest.Create(messageId, toCreate));
                    Assert.Fail("Expected FeatureCreationException due to duplicate key violation.");
                }
                catch (CreateFeatureException e)
                {
                    StringAssert.Contains(e.Message, "duplicate key violation");
                    StringAssert.Contains(e.Message, "Id");
                    StringAssert.Contains(e.Message, "OwnerId");
                    StringAssert.Contains(e.Message, "Space");
                }
            }
        }

        /// <summary>
        /// Attemptings to add feature with empty owner id throws create feature exception.
        /// </summary>
        /// <remarks></remarks>
        [TestMethod]
        public void AttemptingToAddFeatureWithEmptyOwnerIdThrowsCreateFeatureException()
        {
            string messageId = Guid.NewGuid().ToString();
            Feature toCreate = Feature.Create(1, Guid.Empty, Guid.NewGuid(), FeatureName);

            IFeatureStore featureStore = new StandardFeatureStore(m_StorageContainer);

            try
            {
                featureStore.CreateFeature(CreateFeatureRequest.Create(messageId, toCreate));
                Assert.Fail("Expected FeatureCreationException due to duplicate key violation.");
            }
            catch (CreateFeatureException e)
            {
                StringAssert.Contains(e.Message, "A non-empty OwnerId must be provided");
            }
        }

        #endregion

        #region RetrieveDefinedFeatures tests

        /// <summary>
        /// Retrieves the features for owner id.
        /// </summary>
        /// <remarks></remarks>
        [TestMethod]
        public void RetrieveFeaturesForOwnerId()
        {
            string messageId = Guid.NewGuid().ToString();
            Guid ownerId = Guid.NewGuid();
            IList<Feature> features = new List<Feature>
                                          {
                                              Feature.Create(1, ownerId, Guid.Empty, "Feature One"),
                                              Feature.Create(1, ownerId, Guid.NewGuid(), "Feature Two"),
                                              Feature.Create(1, ownerId, Guid.NewGuid(), "Feature Three")
                                          };

            IFeatureStore featureStore = new StandardFeatureStore(m_StorageContainer);
            FeatureScope featureScope = FeatureScope.Create(ownerId, Guid.Empty);
            RetrieveDefinedFeaturesRequest request = RetrieveDefinedFeaturesRequest.Create(messageId, featureScope);

            using (m_MockRepository.Record())
            {
                Expect.Call(m_StorageContainer.Retrieve(featureScope)).Return(features);
                m_MockRepository.ReplayAll();

                RetrieveDefinedFeaturesResponse response = featureStore.RetrieveDefinedFeatures(request);

                Assert.AreEqual(request.Header.MessageId, response.Header.MessageId);
                foreach (Feature feature in response.Result)
                {
                    Assert.AreEqual(ownerId, feature.OwnerId);
                }

                m_MockRepository.VerifyAll();
            }
        }

        /// <summary>
        /// Retrieves the defined features exception thrown.
        /// </summary>
        /// <remarks></remarks>
        [TestMethod]
        public void RetrieveDefinedFeaturesExceptionThrown()
        {
            string messageId = Guid.NewGuid().ToString();
            Exception exception = m_MockRepository.StrictMock<Exception>();

            RetrieveDefinedFeaturesRequest request = RetrieveDefinedFeaturesRequest.Create(messageId,
                                                                                           FeatureScope.Create(
                                                                                               Guid.NewGuid(),
                                                                                               Guid.NewGuid()));

            IFeatureStore featureStore = new StandardFeatureStore(m_StorageContainer);

            using (m_MockRepository.Record())
            {
                Expect.Call(m_StorageContainer.Retrieve(request.FeatureScope)).Throw(exception);

                m_MockRepository.ReplayAll();

                try
                {
                    featureStore.RetrieveDefinedFeatures(request);
                    Assert.Fail("Expected FeatureCreationException");
                }
                catch (RetrieveDefinedFeaturesException)
                {
                }

                m_MockRepository.VerifyAll();
            }
        }

        #endregion

        #region UpdateFeatureState tests

        /// <summary>
        /// Existings the feature is updated correctly.
        /// </summary>
        /// <remarks></remarks>
        [TestMethod]
        public void ExistingFeatureIsUpdatedCorrectly()
        {
            string messageId = Guid.NewGuid().ToString();
            Guid space = Guid.NewGuid();
            Guid owner = Guid.NewGuid();
            FeatureKey featureQuery = FeatureKey.Create(1, owner, space);
            Feature storedFeature = Feature.Create(1, owner, space, FeatureName);
            Feature updatedFeature = Feature.Create(1, owner, space, FeatureName);
            updatedFeature.Enabled = true;
            StandardFeatureStore standardFeatureStore = new StandardFeatureStore(m_StorageContainer);
            UpdateFeatureStateRequest request = UpdateFeatureStateRequest.Create(messageId, featureQuery, true);

            using (m_MockRepository.Record())
            {
                Expect.Call(m_StorageContainer.Retrieve(featureQuery)).Return(storedFeature);
                Expect.Call(m_StorageContainer.Store(storedFeature)).Return(updatedFeature);
                m_MockRepository.ReplayAll();

                UpdateFeatureStateResponse response = standardFeatureStore.UpdateFeatureState(request);

                Assert.AreEqual(messageId, response.Header.MessageId);
                Assert.AreEqual(1, response.Result.Id);
                Assert.AreEqual(space, response.Result.Space);
                Assert.IsTrue(response.Result.Enabled);

                m_MockRepository.VerifyAll();
            }
        }

        /// <summary>
        /// Updates the feature state exception thrown when updating non existent feature.
        /// </summary>
        /// <remarks></remarks>
        [TestMethod]
        public void UpdateFeatureStateExceptionThrownWhenUpdatingNonExistentFeature()
        {
            string messageId = Guid.NewGuid().ToString();
            Guid space = Guid.NewGuid();
            Guid owner = Guid.NewGuid();
            FeatureKey featureQuery = FeatureKey.Create(1, owner, space);
            StandardFeatureStore standardFeatureStore = new StandardFeatureStore(m_StorageContainer);
            UpdateFeatureStateRequest request = UpdateFeatureStateRequest.Create(messageId, featureQuery, true);

            using (m_MockRepository.Record())
            {
                Expect.Call(m_StorageContainer.Retrieve(featureQuery)).Return(null);
                m_MockRepository.ReplayAll();

                try
                {
                    standardFeatureStore.UpdateFeatureState(request);
                    Assert.Fail("Expected a UpdateFeatureStateException to be thrown");
                }
                catch (UpdateFeatureStateException ex)
                {
                    StringAssert.Contains(ex.Message, "Id 1");
                    StringAssert.Contains(ex.Message, "Space " + space);
                    Assert.IsNotNull(ex.InnerException);
                }
            }
        }

        #endregion

        #region StandardFeatureStore full interface tests

        /// <summary>
        /// Exercises the full interface.
        /// </summary>
        /// <remarks></remarks>
        [TestMethod, Ignore]
        [DeploymentItem(@".\log4net.config")]
        public void ExerciseFullInterface()
        {
            Debug.WriteLine("BEGIN: ExerciseFullInterface");

            CacheSwappingStorageContainer cacheSwappingStorageContainer =
                new CacheSwappingStorageContainer(@".\ExerciseFullInterface_Storage.dat");
            StandardFeatureStore standardFeatureStore = new StandardFeatureStore(cacheSwappingStorageContainer);

            /* -- CreateFeature -- */

            CreateFeatureRequest createFeatureRequest1 = CreateFeatureRequest.Create(
                Guid.NewGuid().ToString(),
                Feature.Create(
                    1,
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    "Feature One"));
            
            CreateFeatureRequest createFeatureRequest2 = CreateFeatureRequest.Create(
                Guid.NewGuid().ToString(),
                Feature.Create(
                    2,
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    "Feature Two"));
            
            CreateFeatureRequest createFeatureRequest3 = CreateFeatureRequest.Create(
                Guid.NewGuid().ToString(),
                Feature.Create(
                    3,
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                                                                                                    "Feature Three"));

            CreateFeatureResponse createFeatureResponse = standardFeatureStore.CreateFeature(createFeatureRequest1);
            AssertCreateFeatureResponse(createFeatureRequest1, createFeatureResponse);

            createFeatureResponse = standardFeatureStore.CreateFeature(createFeatureRequest2);
            AssertCreateFeatureResponse(createFeatureRequest2, createFeatureResponse);

            createFeatureResponse = standardFeatureStore.CreateFeature(createFeatureRequest3);
            AssertCreateFeatureResponse(createFeatureRequest3, createFeatureResponse);

            AssertPerformanceCountersRecorded(PerformanceCounterReporterType.CreateFeature, false, 3);

            /* -- CheckFeatureState -- */

            CheckFeatureStateRequest checkFeatureStateRequest1 =
                CheckFeatureStateRequest.Create(
                    Guid.NewGuid().ToString(),
                    FeatureKey.Create(
                        createFeatureRequest1.Feature.Id,
                        createFeatureRequest1.Feature.OwnerId,
                                                                  createFeatureRequest1.Feature.Space));
            CheckFeatureStateResponse checkFeatureStateResponse =
                standardFeatureStore.CheckFeatureState(checkFeatureStateRequest1);
            AssertCheckFeatureStateResponse(checkFeatureStateRequest1, checkFeatureStateResponse);

            CheckFeatureStateRequest checkFeatureStateRequest2 =
                CheckFeatureStateRequest.Create(
                    Guid.NewGuid().ToString(),
                    FeatureKey.Create(
                        createFeatureRequest2.Feature.Id,
                        createFeatureRequest2.Feature.OwnerId,
                                                                  createFeatureRequest2.Feature.Space));
            checkFeatureStateResponse = standardFeatureStore.CheckFeatureState(checkFeatureStateRequest2);
            AssertCheckFeatureStateResponse(checkFeatureStateRequest2, checkFeatureStateResponse);

            CheckFeatureStateRequest checkFeatureStateRequest3 =
                CheckFeatureStateRequest.Create(
                    Guid.NewGuid().ToString(),
                    FeatureKey.Create(
                        createFeatureRequest3.Feature.Id,
                        createFeatureRequest3.Feature.OwnerId,
                                                                  createFeatureRequest3.Feature.Space));
            checkFeatureStateResponse = standardFeatureStore.CheckFeatureState(checkFeatureStateRequest3);
            AssertCheckFeatureStateResponse(checkFeatureStateRequest3, checkFeatureStateResponse);

            AssertPerformanceCountersRecorded(PerformanceCounterReporterType.CheckFeatureState, false, 3);

            /* -- UpdateFeatureState -- */

            UpdateFeatureStateRequest updateFeatureStateRequest1 =
                UpdateFeatureStateRequest.Create(
                    Guid.NewGuid().ToString(),
                    FeatureKey.Create(
                        createFeatureRequest1.Feature.Id,
                        createFeatureRequest1.Feature.OwnerId,
                        createFeatureRequest1.Feature.Space),
                    true);
            UpdateFeatureStateResponse updateFeatureStateResponse =
                standardFeatureStore.UpdateFeatureState(updateFeatureStateRequest1);
            Assert.IsTrue(updateFeatureStateResponse.Result.Enabled);

            UpdateFeatureStateRequest updateFeatureStateRequest2 =
                UpdateFeatureStateRequest.Create(
                    Guid.NewGuid().ToString(),
                    FeatureKey.Create(
                        createFeatureRequest2.Feature.Id,
                        createFeatureRequest2.Feature.OwnerId,
                        createFeatureRequest2.Feature.Space),
                    true);
            updateFeatureStateResponse = standardFeatureStore.UpdateFeatureState(updateFeatureStateRequest2);
            Assert.IsTrue(updateFeatureStateResponse.Result.Enabled);

            UpdateFeatureStateRequest updateFeatureStateRequest3 =
                UpdateFeatureStateRequest.Create(
                    Guid.NewGuid().ToString(),
                    FeatureKey.Create(
                        createFeatureRequest3.Feature.Id,
                        createFeatureRequest3.Feature.OwnerId,
                        createFeatureRequest3.Feature.Space),
                    true);
            
            updateFeatureStateResponse = standardFeatureStore.UpdateFeatureState(updateFeatureStateRequest3);
            Assert.IsTrue(updateFeatureStateResponse.Result.Enabled);

            AssertPerformanceCountersRecorded(PerformanceCounterReporterType.UpdateFeatureState, false, 3);

            /* -- RetrieveDefinedFeatures -- */

            RetrieveDefinedFeaturesRequest retrieveDefinedFeaturesRequest = RetrieveDefinedFeaturesRequest.Create(
                Guid.NewGuid().ToString(),
                FeatureScope.Create(createFeatureRequest1.Feature.OwnerId, createFeatureRequest1.Feature.Space));

            RetrieveDefinedFeaturesResponse retrieveDefinedFeaturesResponse =
                standardFeatureStore.RetrieveDefinedFeatures(retrieveDefinedFeaturesRequest);
            Assert.IsNotNull(retrieveDefinedFeaturesResponse.Result);
            Assert.IsTrue(retrieveDefinedFeaturesResponse.Result.GetEnumerator().MoveNext());

            AssertPerformanceCountersRecorded(PerformanceCounterReporterType.RetrieveDefinedFeatures, true, 1);

            Debug.WriteLine("END: ExerciseFullInterface");
        }

        /// <summary>
        /// Asserts the check feature state response.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="response">The response.</param>
        /// <remarks></remarks>
        private static void AssertCheckFeatureStateResponse(CheckFeatureStateRequest request,
                                                            CheckFeatureStateResponse response)
        {
            Assert.AreEqual(request.Header.MessageId, response.Header.MessageId);
            Assert.AreEqual(request.Key.Id, response.Result.Id);
            Assert.AreEqual(request.Key.Space, response.Result.Space);
        }

        /// <summary>
        /// Asserts the performance counters recorded.
        /// </summary>
        /// <param name="reporterType">Type of the reporter.</param>
        /// <param name="checkExecutionTime">if set to <c>true</c> [check execution time].</param>
        /// <param name="expectedCount">The expected count.</param>
        /// <remarks></remarks>
        private static void AssertPerformanceCountersRecorded(PerformanceCounterReporterType reporterType,
                                                              bool checkExecutionTime, int expectedCount)
        {
            PerformanceCounterReporter counterRecorder = PerformanceCounterReporterFactory.CreateReporter(reporterType);

            Assert.AreEqual(expectedCount, counterRecorder.OperationCountValue);

            Assert.IsTrue(counterRecorder.OperationCountValue > 0);
            float executionTime = counterRecorder.ExecutionTimeValue;
            Console.WriteLine(@"The execution time was reported as " + executionTime);

            // The CheckFeatureState method smokes the millisecond barrier at these cache sizes,
            // therefore this check is disabled for that method.
            if (checkExecutionTime)
            {
                Assert.IsTrue(executionTime > 0);
            }
        }

        /// <summary>
        /// Asserts the create feature response.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="response">The response.</param>
        /// <remarks></remarks>
        private static void AssertCreateFeatureResponse(CreateFeatureRequest request, CreateFeatureResponse response)
        {
            Assert.AreEqual(request.Header.MessageId, response.Header.MessageId);
            Assert.AreEqual(request.Feature.Id, response.Result.Id);
            Assert.AreEqual(request.Feature.Space, response.Result.Space);
            Assert.AreEqual(request.Feature.Name, response.Result.Name);
        }

        #endregion
    }
}