// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OperationContractNames.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Contains static strings that represent the System.ServiceModel naming requirements
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.Mutual
{
    using System.ServiceModel;

    /// <summary>
    ///   Contains static strings that represent the System.ServiceModel naming requirements
    /// </summary>
    public static class OperationContractNames
    {
        /// <summary>
        ///   WCF-required value for the Action parameter of the <see cref = "OperationContractAttribute" /> class on the CheckFeatureState method of the <see
        ///    cref = "IFeatureStore" /> interface.
        /// </summary>
        public const string CheckFeatureStateAction = "CheckFeatureStateAction";

        /// <summary>
        ///   WCF-required value for the ReplyAction parameter of the <see cref = "OperationContractAttribute" /> class on the CheckFeatureState method of the <see
        ///    cref = "IFeatureStore" /> interface.
        /// </summary>
        public const string CheckFeatureStateReplyAction = "CheckFeatureStateReplyAction";

        /// <summary>
        ///   WCF-required value for the Action parameter of the <see cref = "OperationContractAttribute" /> class on the CreateFeature method of the <see
        ///    cref = "IFeatureStore" /> interface.
        /// </summary>
        public const string CreateFeatureAction = "CreateFeatureAction";

        /// <summary>
        ///   WCF-required value for the ReplyAction parameter of the <see cref = "OperationContractAttribute" /> class on the CreateFeature method of the <see
        ///    cref = "IFeatureStore" /> interface.
        /// </summary>
        public const string CreateFeatureReplyAction = "CreateFeatureReplyAction";

        /// <summary>
        ///   WCF-required value for the Action parameter of the <see cref = "OperationContractAttribute" /> class on the RetrieveDefinedFeatures method of the <see
        ///    cref = "IFeatureStore" /> interface.
        /// </summary>
        public const string RetrieveDefinedFeaturesAction = "RetrieveDefinedFeaturesAction";

        /// <summary>
        ///   WCF-required value for the ReplyAction parameter of the <see cref = "OperationContractAttribute" /> class on the RetrieveDefinedFeatures method of the <see
        ///    cref = "IFeatureStore" /> interface.
        /// </summary>
        public const string RetrieveDefinedFeaturesReplyAction = "RetrieveDefinedFeaturesReplyAction";

        /// <summary>
        ///   WCF-required value for the Action parameter of the <see cref = "OperationContractAttribute" /> class on the UpdateFeatureState method of the <see
        ///    cref = "IFeatureStore" /> interface.
        /// </summary>
        public const string UpdateFeatureStateAction = "UpdateFeatureStateAction";

        /// <summary>
        ///   WCF-required value for the ReplyAction parameter of the <see cref = "OperationContractAttribute" /> class on the UpdateFeatureStateReply method of the <see
        ///    cref = "IFeatureStore" /> interface.
        /// </summary>
        public const string UpdateFeatureStateReplyAction = "UpdateFeatureStateReplyAction";
    }
}