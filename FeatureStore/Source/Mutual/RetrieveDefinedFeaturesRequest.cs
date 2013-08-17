// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RetrieveDefinedFeaturesRequest.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Parameter for the RetrieveDefinedFeatures method on the <see cref="IFeatureStore" /> interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.Mutual
{
    using System.Runtime.Serialization;

    /// <summary>
    ///   Parameter for the RetrieveDefinedFeatures method on the <see cref = "IFeatureStore" /> interface.
    /// </summary>
    [KnownType(typeof(RetrieveDefinedFeaturesRequest))]
    [DataContract]
    public class RetrieveDefinedFeaturesRequest : IExtensibleDataObject
    {
        /// <summary>
        ///   Gets the feature scope.
        /// </summary>
        /// <value>The feature scope.</value>
        [DataMember]
        public FeatureScope FeatureScope { get; private set; }

        /// <summary>
        ///   Gets the header.
        /// </summary>
        /// <value>The header.</value>
        [DataMember]
        public MessageHeader Header { get; private set; }

        #region IExtensibleDataObject Members

        /// <summary>
        ///   Gets or sets the structure that contains extra data.
        /// </summary>
        /// <value></value>
        /// <returns>An <see cref = "T:System.Runtime.Serialization.ExtensionDataObject" /> that contains data that is not recognized as belonging to the data contract.</returns>
        public ExtensionDataObject ExtensionData { get; set; }

        #endregion

        /// <summary>
        ///   Creates the specified feature scope.
        /// </summary>
        /// <param name = "messageId">The unique identifier for the message.</param>
        /// <param name = "featureScope">The feature scope.</param>
        /// <returns>An initialized instance of the <see cref = "RetrieveDefinedFeaturesRequest" /></returns>
        /// class.
        public static RetrieveDefinedFeaturesRequest Create(string messageId, FeatureScope featureScope)
        {
            return new RetrieveDefinedFeaturesRequest
                       {
                           FeatureScope = featureScope,
                           Header = MessageHeader.Create(messageId)
                       };
        }
    }
}