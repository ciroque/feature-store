// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StandardFeatureStore.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   The primary implementation of the IFeatureStore interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.Core
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading;

    using Ciroque.Foundations.FeatureStore.Data;
    using Ciroque.Foundations.FeatureStore.Instrumentation;
    using Ciroque.Foundations.FeatureStore.Mutual;

    using log4net;
    using log4net.Config;

    /// <summary>
    ///   The primary implementation of the IFeatureStore interface.
    /// </summary>
    public class StandardFeatureStore : IFeatureStore
    {
        /// <summary>
        ///   Instance of the log4net ILog interface for logging.
        /// </summary>
        private readonly ILog m_Logger;

        /// <summary>
        ///   The <see cref = "IStorageContainer" /> to be used for data storage / retrieval operations.
        /// </summary>
        private readonly IStorageContainer m_StorageContainer;

        /// <summary>
        ///   Initializes a new instance of the <see cref = "StandardFeatureStore" /> class.
        /// </summary>
        /// <param name = "storageContainer">The container to / from which the Features will be stored / loaded.</param>
        public StandardFeatureStore(IStorageContainer storageContainer)
        {
            XmlConfigurator.Configure();
            m_Logger = LogManager.GetLogger(typeof(StandardFeatureStore));

            m_StorageContainer = storageContainer;
        }

        #region IFeatureStore Members

        /// <summary>
        ///   Stores a feature that can be queried and updated.
        /// </summary>
        /// <param name = "request"><see cref = "CreateFeatureRequest" /> instance that defines the state required to create a new Feature.</param>
        /// <returns>
        ///   <see cref = "CreateFeatureResponse" /> containing the results of the request to create a new Feature.
        /// </returns>
        public CreateFeatureResponse CreateFeature(CreateFeatureRequest request)
        {
            CreateFeatureResponse response;
            using (PerformanceCounterReporterFactory.CreateReporter(PerformanceCounterReporterType.CreateFeature))
            {
                Feature feature;

                EnsureOwnerId(request.Feature);
                CheckDuplicateKey(request.Feature);

                try
                {
                    feature = m_StorageContainer.Store(request.Feature);
                }
                catch (Exception e)
                {
                    CreateFeatureException createFeatureException =
                        new CreateFeatureException(ExceptionMessageResources.FEATURE_CREATION_EXCEPTION, e);
                    m_Logger.Error(createFeatureException);

                    throw createFeatureException;
                }

                response = CreateFeatureResponse.Create(request.Header.MessageId, feature);
                LogInteraction(request, response);
            }

            return response;
        }

        /// <summary>
        ///   Checks the state of an existing Feature.
        /// </summary>
        /// <param name = "request"><see cref = "CheckFeatureStateRequest" /> instance that defines the criteria by which the Feature will be queried.</param>
        /// <returns>
        ///   <see cref = "CheckFeatureStateResponse" /> containing the results of the request for the state of a Feature.
        /// </returns>
        public CheckFeatureStateResponse CheckFeatureState(CheckFeatureStateRequest request)
        {
            CheckFeatureStateResponse response;
            using (PerformanceCounterReporterFactory.CreateReporter(PerformanceCounterReporterType.CheckFeatureState))
            {
                Feature feature;

                try
                {
                    feature = m_StorageContainer.Retrieve(request.Key);
                }
                catch (Exception e)
                {
                    CheckFeatureStateException checkFeatureStateException =
                        new CheckFeatureStateException(
                            string.Format(
                                CultureInfo.CurrentUICulture,
                                ExceptionMessageResources.CHECK_FEATURE_STATE_EXCEPTION,
                                request.Key.Id,
                                request.Key.Space),
                        e);
                    m_Logger.Error(checkFeatureStateException);

                    throw checkFeatureStateException;
                }

                response = CheckFeatureStateResponse.Create(request.Header.MessageId, feature);
                LogInteraction(request, response);
            }

            return response;
        }

        /// <summary>
        ///   Updates the state of a specified feature.
        /// </summary>
        /// <param name = "request"><see cref = "UpdateFeatureStateRequest" /> instance that defines the state necessary to update a specified Feature.</param>
        /// <returns>
        ///   <see cref = "UpdateFeatureStateResponse" /> containing the results of the request to update the state of a specified Feature.
        /// </returns>
        public UpdateFeatureStateResponse UpdateFeatureState(UpdateFeatureStateRequest request)
        {
            UpdateFeatureStateResponse response;
            using (PerformanceCounterReporterFactory.CreateReporter(PerformanceCounterReporterType.UpdateFeatureState))
            {
                Feature updatedFeature;

                try
                {
                    Feature feature = m_StorageContainer.Retrieve(request.Key);

                    if (feature == null)
                    {
                        throw new UpdateFeatureStateException(
                            string.Format(
                                CultureInfo.CurrentUICulture,
                                ExceptionMessageResources.FEATURE_NOT_FOUND,
                                request.Key.Id,
                                request.Key.Space));
                    }

                    feature.Enabled = request.NewState;

                    updatedFeature = m_StorageContainer.Store(feature);
                }
                catch (Exception e)
                {
                    UpdateFeatureStateException updateFeatureStateException =
                        new UpdateFeatureStateException(
                            string.Format(
                                CultureInfo.CurrentUICulture,
                                ExceptionMessageResources.UPDATE_FEATURE_STATE_EXCEPTION,
                                request.Key.Id,
                                request.Key.Space),
                        e);
                    m_Logger.Error(updateFeatureStateException);

                    throw updateFeatureStateException;
                }

                response = UpdateFeatureStateResponse.Create(request.Header.MessageId, updatedFeature);
                LogInteraction(request, response);
            }

            return response;
        }

        /// <summary>
        ///   Retrieves the defined features.
        /// </summary>
        /// <param name = "request"><see cref = "RetrieveDefinedFeaturesRequest" /> instance containing the criteria by which the <see
        ///    cref = "Feature" />s are selected.</param>
        /// <returns>
        ///   <see cref = "RetrieveDefinedFeaturesResponse" /> instance containing the results based on the given criteria.
        /// </returns>
        public RetrieveDefinedFeaturesResponse RetrieveDefinedFeatures(RetrieveDefinedFeaturesRequest request)
        {
            RetrieveDefinedFeaturesResponse response;
            using (PerformanceCounterReporterFactory.CreateReporter(PerformanceCounterReporterType.RetrieveDefinedFeatures))
            {
                IList<Feature> features;
                try
                {
                    features = m_StorageContainer.Retrieve(request.FeatureScope);
                }
                catch (Exception)
                {
                    RetrieveDefinedFeaturesException retrieveDefinedFeaturesException =
                        new RetrieveDefinedFeaturesException(
                            string.Format(
                                CultureInfo.CurrentUICulture,
                                ExceptionMessageResources.RETRIEVE_DEFINED_FEATURES_EXCEPTION,
                                request.FeatureScope.OwnerId,
                                request.FeatureScope.Space));
                    m_Logger.Error(retrieveDefinedFeaturesException);

                    throw retrieveDefinedFeaturesException;
                }

                response = RetrieveDefinedFeaturesResponse.Create(request.Header.MessageId, features);
                LogInteraction(request, response);
            }

            return response;
        }

        #endregion

        /// <summary>
        ///   Ensures the owner id.
        /// </summary>
        /// <param name = "feature">The feature.</param>
        private static void EnsureOwnerId(Feature feature)
        {
            if (feature == null)
            {
                throw new ArgumentNullException("feature");
            }

            if (feature.OwnerId.Equals(Guid.Empty))
            {
                throw new CreateFeatureException(ExceptionMessageResources.EMPTY_OWNER_GUID_NOT_ALLOWED);
            }
        }

        /// <summary>
        ///   Checks the duplicate key.
        /// </summary>
        /// <param name = "feature">The feature.</param>
        private void CheckDuplicateKey(Feature feature)
        {
            if (m_StorageContainer.Retrieve(FeatureKey.Create(feature.Id, feature.OwnerId, feature.Space)) != null)
            {
                throw new CreateFeatureException(
                    string.Format(
                        CultureInfo.CurrentUICulture,
                        ExceptionMessageResources.DUPLICATE_KEY_VIOLATION,
                        feature.Id,
                        feature.OwnerId,
                        feature.Space));
            }
        }

        /// <summary>
        ///   Logs the interaction.
        /// </summary>
        /// <param name = "request">The request.</param>
        /// <param name = "response">The response.</param>
        private void LogInteraction(object request, object response)
        {
            ThreadPool.QueueUserWorkItem(state =>
                                             {
                                                 string requestJson = JsonToStringHelper.ObjectToJson(request);
                                                 string responseJson = JsonToStringHelper.ObjectToJson(response);
                                                 m_Logger.DebugFormat(
                                                     CultureInfo.InvariantCulture,
                                                     LoggingResources.LOG_REQUEST_RESPONSE,
                                                     requestJson,
                                                     responseJson);
                                             });
        }
    }
}