// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFeatureStoreServiceProxy.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Client-side implemementation of the IFeatureStoreService interface. Adds asynchronous method calls.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.ServiceProxy
{
    using System;
    using System.ServiceModel;
    using Mutual;
    using Service;

    /// <summary>
    ///   Client-side implemementation of the IFeatureStoreService interface. Adds asynchronous method calls.
    /// </summary>
    [ServiceContract]
    public interface IFeatureStoreServiceProxy : IFeatureStoreService
    {
        /// <summary>
        ///   Begins the create feature.
        /// </summary>
        /// <param name = "request">The request.</param>
        /// <param name = "callback">The callback.</param>
        /// <param name = "state">The state.</param>
        /// <returns><see cref = "IAsyncResult" /> instance used to query the state of the asynchronous operation.</returns>
        [OperationContract(AsyncPattern = true, Action = OperationContractNames.CreateFeatureAction,
            ReplyAction = OperationContractNames.CreateFeatureReplyAction)]
        IAsyncResult BeginCreateFeature(CreateFeatureRequest request, AsyncCallback callback, object state);

        /// <summary>
        ///   Ends the create feature.
        /// </summary>
        /// <param name = "result">The result.</param>
        /// <returns><see cref = "CreateFeatureResponse" /> instance containing the results of the service call.</returns>
        CreateFeatureResponse EndCreateFeature(IAsyncResult result);

        /// <summary>
        ///   Begins the state of the check feature.
        /// </summary>
        /// <param name = "request">The request.</param>
        /// <param name = "callback">The async callback.</param>
        /// <param name = "state">The state.</param>
        /// <returns><see cref = "IAsyncResult" /> instance used to query the state of the asynchronous operation.</returns>
        [OperationContract(AsyncPattern = true, Action = OperationContractNames.CheckFeatureStateAction,
            ReplyAction = OperationContractNames.CheckFeatureStateReplyAction)]
        IAsyncResult BeginCheckFeatureState(CheckFeatureStateRequest request, AsyncCallback callback, object state);

        /// <summary>
        ///   Ends the state of the check feature.
        /// </summary>
        /// <param name = "result">The result.</param>
        /// <returns><see cref = "CheckFeatureStateResponse" /> instance containing the results of the service call.</returns>
        CheckFeatureStateResponse EndCheckFeatureState(IAsyncResult result);

        /// <summary>
        ///   Begins the state of the update feature.
        /// </summary>
        /// <param name = "request">The request.</param>
        /// <param name = "callback">The callback.</param>
        /// <param name = "state">The state.</param>
        /// <returns><see cref = "IAsyncResult" /> instance used to query the state of the asynchronous operation.</returns>
        [OperationContract(AsyncPattern = true, Action = OperationContractNames.UpdateFeatureStateAction,
            ReplyAction = OperationContractNames.UpdateFeatureStateReplyAction)]
        IAsyncResult BeginUpdateFeatureState(UpdateFeatureStateRequest request, AsyncCallback callback, object state);

        /// <summary>
        ///   Ends the state of the update feature.
        /// </summary>
        /// <param name = "result">The result.</param>
        /// <returns><see cref = "UpdateFeatureStateResponse" /> instance containing the results of the service call.</returns>
        UpdateFeatureStateResponse EndUpdateFeatureState(IAsyncResult result);

        /// <summary>
        ///   Begins the retrieve defined features.
        /// </summary>
        /// <param name = "request">The request.</param>
        /// <param name = "callback">The callback.</param>
        /// <param name = "state">The state.</param>
        /// <returns><see cref = "IAsyncResult" /> instance used to query the state of the asynchronous operation.</returns>
        [OperationContract(AsyncPattern = true, Action = OperationContractNames.RetrieveDefinedFeaturesAction,
            ReplyAction = OperationContractNames.RetrieveDefinedFeaturesReplyAction)]
        IAsyncResult BeginRetrieveDefinedFeatures(
            RetrieveDefinedFeaturesRequest request, AsyncCallback callback, object state);

        /// <summary>
        ///   Ends the retrieve defined features.
        /// </summary>
        /// <param name = "result">The result.</param>
        /// <returns><see cref = "RetrieveDefinedFeaturesResponse" /> instance containing the results of the service call.</returns>
        RetrieveDefinedFeaturesResponse EndRetrieveDefinedFeatures(IAsyncResult result);
    }
}