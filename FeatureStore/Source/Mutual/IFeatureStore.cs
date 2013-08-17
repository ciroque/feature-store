// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFeatureStore.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Defines the public interface to the FeatureStore service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.Mutual
{
    using System.ServiceModel;

    /// <summary>
    ///   Defines the public interface to the FeatureStore service.
    /// </summary>
    [ServiceContract]
    public interface IFeatureStore
    {
        /// <summary>
        ///   Stores a feature that can be queried and updated.
        /// </summary>
        /// <param name = "request"><see cref = "CreateFeatureRequest" /> instance that defines the state required to create a new Feature.</param>
        /// <returns><see cref = "CreateFeatureResponse" /> containing the results of the request to create a new Feature.</returns>
        [OperationContract(Action = OperationContractNames.CreateFeatureAction,
            ReplyAction = OperationContractNames.CreateFeatureReplyAction)]
        CreateFeatureResponse CreateFeature(CreateFeatureRequest request);

        /// <summary>
        ///   Checks the state of an existing Feature.
        /// </summary>
        /// <param name = "request"><see cref = "CheckFeatureStateResponse" /> instance that defines the criteria by which the Feature will be queried.</param>
        /// <returns><see cref = "CheckFeatureStateRequest" /> containing the results of the request for the state of a Feature.</returns>
        [OperationContract(Action = OperationContractNames.CheckFeatureStateAction,
            ReplyAction = OperationContractNames.CheckFeatureStateReplyAction)]
        CheckFeatureStateResponse CheckFeatureState(CheckFeatureStateRequest request);

        /// <summary>
        ///   Updates the state of a specified feature.
        /// </summary>
        /// <param name = "request"><see cref = "UpdateFeatureStateRequest" /> instance that defines the state necessary to update a specified Feature.</param>
        /// <returns><see cref = "UpdateFeatureStateResponse" /> containing the results of the request to update the state of a specified Feature.</returns>
        [OperationContract(Action = OperationContractNames.UpdateFeatureStateAction,
            ReplyAction = OperationContractNames.UpdateFeatureStateReplyAction)]
        UpdateFeatureStateResponse UpdateFeatureState(UpdateFeatureStateRequest request);

        /// <summary>
        ///   Retrieves the defined features.
        /// </summary>
        /// <param name = "request"><see cref = "RetrieveDefinedFeaturesRequest" /> instance containing the criteria by which the <see
        ///    cref = "Feature" />s are selected.</param>
        /// <returns><see cref = "RetrieveDefinedFeaturesResponse" /> instance containing the results based on the given criteria.</returns>
        [OperationContract(Action = OperationContractNames.RetrieveDefinedFeaturesAction,
            ReplyAction = OperationContractNames.RetrieveDefinedFeaturesReplyAction)]
        RetrieveDefinedFeaturesResponse RetrieveDefinedFeatures(RetrieveDefinedFeaturesRequest request);
    }
}