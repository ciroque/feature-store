// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateFeatureRequest.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Represents the state necessary to execute the CreateFeature method.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.Mutual
{
    using System.Runtime.Serialization;

    /// <summary>
    ///   Represents the state necessary to execute the CreateFeature method.
    /// </summary>
    [KnownType(typeof(CreateFeatureRequest))]
    [DataContract]
    public class CreateFeatureRequest : IExtensibleDataObject
    {
        /// <summary>
        ///   Prevents a default instance of the <see cref = "CreateFeatureRequest" /> class from being created.
        /// </summary>
        private CreateFeatureRequest()
        {
        }

        /// <summary>
        ///   Gets the feature.
        /// </summary>
        /// <value>The feature to be created.</value>
        [DataMember]
        public Feature Feature { get; private set; }

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
        /// <param name = "feature">The feature to be created.</param>
        /// <returns>A new  instance of the CreateFeatureRequest class with the specified Feature</returns>
        public static CreateFeatureRequest Create(string messageId, Feature feature)
        {
            return new CreateFeatureRequest
                       {
                           Header = MessageHeader.Create(messageId),
                           Feature = feature
                       };
        }
    }
}