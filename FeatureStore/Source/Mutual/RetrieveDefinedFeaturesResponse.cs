// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RetrieveDefinedFeaturesResponse.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Return value for the RetrieveDefinedFeatures method on the <see cref="IFeatureStore" /> interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.Mutual
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    ///   Return value for the RetrieveDefinedFeatures method on the <see cref = "IFeatureStore" /> interface.
    /// </summary>
    [KnownType(typeof(RetrieveDefinedFeaturesResponse))]
    [DataContract]
    public class RetrieveDefinedFeaturesResponse : IExtensibleDataObject
    {
        /// <summary>
        ///   Gets the result.
        /// </summary>
        /// <value>An IList implementation that contains <see cref = "Feature" /> instances.</value>
        [DataMember]
        public IList<Feature> Result { get; private set; }

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
        ///   Creates the specified features.
        /// </summary>
        /// <param name = "messageId">The unique Id for the message.</param>
        /// <param name = "features">The features.</param>
        /// <returns>An initialized instance of the RetrieveDefinedFeaturesResponse class.</returns>
        public static RetrieveDefinedFeaturesResponse Create(string messageId, IList<Feature> features)
        {
            return new RetrieveDefinedFeaturesResponse
                       {
                           Result = features,
                           Header = MessageHeader.Create(messageId)
                       };
        }
    }
}