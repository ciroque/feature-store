// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FeatureStoreService.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   WCF implementation of the <see cref="IFeatureStore" /> interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.Service
{
    using System.Configuration;
    using System.Globalization;
    using System.ServiceModel;
    using Core;
    using Data;
    using Mutual;

    /// <summary>
    ///   WCF implementation of the <see cref = "IFeatureStore" /> interface.
    /// </summary>
    public class FeatureStoreService : IFeatureStoreService
    {
        /// <summary>
        ///   Indicates the name of the storage configuration section of the config file.
        /// </summary>
        private const string StorageConfigurationSectionName = "featureStore.StorageContainer";

        /// <summary>
        ///   The actual implementation of the IFeatureStore interface.
        /// </summary>
        private readonly StandardFeatureStore m_FeatureStoreImp;

        /// <summary>
        ///   Initializes a new instance of the <see cref = "FeatureStoreService" /> class.
        /// </summary>
        public FeatureStoreService()
            : this(
                StorageContainerFactory.Create(
                    (StorageContainerConfigurationSection)
                    ConfigurationManager.GetSection(StorageConfigurationSectionName)))
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "FeatureStoreService" /> class.
        /// </summary>
        /// <param name = "storageContainer">The storage container.</param>
        public FeatureStoreService(IStorageContainer storageContainer)
        {
            m_FeatureStoreImp = new StandardFeatureStore(storageContainer);
        }

        #region IFeatureStoreService Members

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
            try
            {
                response = m_FeatureStoreImp.CreateFeature(request);
            }
            catch (CreateFeatureException e)
            {
                throw new FaultException<FeatureStoreFault>(
                    FeatureStoreFault.Create(e.Message),
                    new FaultReason(
                        new FaultReasonText(
                            e.Message,
                            CultureInfo.CurrentCulture)));
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
            try
            {
                response = m_FeatureStoreImp.CheckFeatureState(request);
            }
            catch (CheckFeatureStateException e)
            {
                throw new FaultException<FeatureStoreFault>(
                    FeatureStoreFault.Create(e.Message),
                    new FaultReason(
                        new FaultReasonText(
                            e.Message,
                            CultureInfo.CurrentCulture)));
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
            try
            {
                response = m_FeatureStoreImp.UpdateFeatureState(request);
            }
            catch (UpdateFeatureStateException e)
            {
                throw new FaultException<FeatureStoreFault>(
                    FeatureStoreFault.Create(e.Message),
                    new FaultReason(
                        new FaultReasonText(
                            e.Message,
                            CultureInfo.CurrentCulture)));
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
            try
            {
                response = m_FeatureStoreImp.RetrieveDefinedFeatures(request);
            }
            catch (RetrieveDefinedFeaturesException e)
            {
                throw new FaultException<FeatureStoreFault>(
                    FeatureStoreFault.Create(e.Message),
                    new FaultReason(
                        new FaultReasonText(
                            e.Message,
                            CultureInfo.CurrentCulture)));
            }

            return response;
        }

        #endregion
    }
}