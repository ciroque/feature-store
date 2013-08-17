// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FeatureStoreServiceProxy.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Implementation of the <see cref="IFeatureStoreServiceProxy" /> interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.ServiceProxy
{
    using System;
    using System.ServiceModel;
    using Mutual;

    /// <summary>
    ///   Implementation of the <see cref = "IFeatureStoreServiceProxy" /> interface.
    /// </summary>
    public class FeatureStoreServiceProxy : ClientBase<IFeatureStoreServiceProxy>, IFeatureStoreServiceProxy
    {
        #region CreateFeature

        /// <summary>
        ///   Stores a feature that can be queried and updated.
        /// </summary>
        /// <param name = "request"><see cref = "CreateFeatureRequest" /> instance that defines the state required to create a new Feature.</param>
        /// <returns>
        ///   <see cref = "CreateFeatureResponse" /> containing the results of the request to create a new Feature.
        /// </returns>
        public CreateFeatureResponse CreateFeature(CreateFeatureRequest request)
        {
            return Channel.CreateFeature(request);
        }

        /// <summary>
        ///   Begins the create feature.
        /// </summary>
        /// <param name = "request">The request.</param>
        /// <param name = "callback">The callback.</param>
        /// <param name = "state">The state.</param>
        /// <returns><see cref = "IAsyncResult" /> instance used to query the state of the asynchronous operation.</returns>
        public IAsyncResult BeginCreateFeature(CreateFeatureRequest request, AsyncCallback callback, object state)
        {
            return Channel.BeginCreateFeature(request, callback, state);
        }

        /// <summary>
        ///   Ends the create feature.
        /// </summary>
        /// <param name = "result">The result.</param>
        /// <returns>
        ///   <see cref = "CreateFeatureResponse" /> instance containing the results of the service call.
        /// </returns>
        public CreateFeatureResponse EndCreateFeature(IAsyncResult result)
        {
            return Channel.EndCreateFeature(result);
        }

        /// <summary>
        ///   Begins the state of the check feature.
        /// </summary>
        /// <param name = "request">The request.</param>
        /// <param name = "callback">The async callback.</param>
        /// <param name = "state">The state.</param>
        /// <returns>
        ///   <see cref = "IAsyncResult" /> instance used to query the state of the asynchronous operation.
        /// </returns>
        public IAsyncResult BeginCheckFeatureState(
            CheckFeatureStateRequest request, AsyncCallback callback, object state)
        {
            return Channel.BeginCheckFeatureState(request, callback, state);
        }

        /// <summary>
        ///   Ends the state of the check feature.
        /// </summary>
        /// <param name = "result">The result.</param>
        /// <returns>
        ///   <see cref = "CheckFeatureStateResponse" /> instance containing the results of the service call.
        /// </returns>
        public CheckFeatureStateResponse EndCheckFeatureState(IAsyncResult result)
        {
            return Channel.EndCheckFeatureState(result);
        }

        /// <summary>
        ///   Begins the state of the update feature.
        /// </summary>
        /// <param name = "request">The request.</param>
        /// <param name = "callback">The callback.</param>
        /// <param name = "state">The state.</param>
        /// <returns>
        ///   <see cref = "IAsyncResult" /> instance used to query the state of the asynchronous operation.
        /// </returns>
        public IAsyncResult BeginUpdateFeatureState(
            UpdateFeatureStateRequest request, AsyncCallback callback, object state)
        {
            return Channel.BeginUpdateFeatureState(request, callback, state);
        }

        /// <summary>
        ///   Ends the state of the update feature.
        /// </summary>
        /// <param name = "result">The result.</param>
        /// <returns>
        ///   <see cref = "UpdateFeatureStateResponse" /> instance containing the results of the service call.
        /// </returns>
        public UpdateFeatureStateResponse EndUpdateFeatureState(IAsyncResult result)
        {
            return Channel.EndUpdateFeatureState(result);
        }

        /// <summary>
        ///   Begins the retrieve defined features.
        /// </summary>
        /// <param name = "request">The request.</param>
        /// <param name = "callback">The callback.</param>
        /// <param name = "state">The state.</param>
        /// <returns>
        ///   <see cref = "IAsyncResult" /> instance used to query the state of the asynchronous operation.
        /// </returns>
        public IAsyncResult BeginRetrieveDefinedFeatures(
            RetrieveDefinedFeaturesRequest request, AsyncCallback callback, object state)
        {
            return Channel.BeginRetrieveDefinedFeatures(request, callback, state);
        }

        /// <summary>
        ///   Ends the retrieve defined features.
        /// </summary>
        /// <param name = "result">The result.</param>
        /// <returns>
        ///   <see cref = "RetrieveDefinedFeaturesResponse" /> instance containing the results of the service call.
        /// </returns>
        public RetrieveDefinedFeaturesResponse EndRetrieveDefinedFeatures(IAsyncResult result)
        {
            return Channel.EndRetrieveDefinedFeatures(result);
        }

        #endregion

        #region CheckFeatureState

        /// <summary>
        ///   Checks the state of an existing Feature.
        /// </summary>
        /// <param name = "request"><see cref = "CheckFeatureStateResponse" /> instance that defines the criteria by which the Feature will be queried.</param>
        /// <returns>
        ///   <see cref = "CheckFeatureStateRequest" /> containing the results of the request for the state of a Feature.
        /// </returns>
        public CheckFeatureStateResponse CheckFeatureState(CheckFeatureStateRequest request)
        {
            return Channel.CheckFeatureState(request);
        }

        #endregion

        #region UpdateFeatureState

        /// <summary>
        ///   Updates the state of a specified feature.
        /// </summary>
        /// <param name = "request"><see cref = "UpdateFeatureStateRequest" /> instance that defines the state necessary to update a specified Feature.</param>
        /// <returns>
        ///   <see cref = "UpdateFeatureStateResponse" /> containing the results of the request to update the state of a specified Feature.
        /// </returns>
        public UpdateFeatureStateResponse UpdateFeatureState(UpdateFeatureStateRequest request)
        {
            return Channel.UpdateFeatureState(request);
        }

        #endregion

        #region RetrieveDefinedFeatures

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
            return Channel.RetrieveDefinedFeatures(request);
        }

        #endregion
    }
}