// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateFeatureResponse.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Represents the result of calling the CreateFeature method.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.Mutual
{
    using System.Runtime.Serialization;

    /// <summary>
    ///   Represents the result of calling the CreateFeature method.
    /// </summary>
    [KnownType(typeof(CreateFeatureResponse))]
    [DataContract]
    public class CreateFeatureResponse : IExtensibleDataObject
    {
        /// <summary>
        ///   Gets the Feature.
        /// </summary>
        /// <value>The instance of the Feature class resulting from the call to the CreateFeature method.</value>
        [DataMember]
        public Feature Result { get; private set; }

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
        ///   Creates the specified feature.
        /// </summary>
        /// <param name = "messageId">The caller-defined id for the message.</param>
        /// <param name = "feature">The feature.</param>
        /// <returns>A new instance of the CreateFeatureResponse.</returns>
        public static CreateFeatureResponse Create(string messageId, Feature feature)
        {
            return new CreateFeatureResponse
                       {
                           Header = MessageHeader.Create(messageId),
                           Result = feature
                       };
        }
    }
}