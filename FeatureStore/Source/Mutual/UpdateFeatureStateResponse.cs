// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateFeatureStateResponse.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Represents the results of calling the UpdateFeatureState method.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.Mutual
{
    using System.Runtime.Serialization;

    /// <summary>
    ///   Represents the results of calling the UpdateFeatureState method.
    /// </summary>
    [KnownType(typeof(UpdateFeatureStateResponse))]
    [DataContract]
    public class UpdateFeatureStateResponse : IExtensibleDataObject
    {
        /// <summary>
        ///   Gets the header.
        /// </summary>
        /// <value>The header.</value>
        [DataMember]
        public MessageHeader Header { get; private set; }

        /// <summary>
        ///   Gets the result.
        /// </summary>
        /// <value>The result.</value>
        [DataMember]
        public Feature Result { get; private set; }

        #region IExtensibleDataObject Members

        /// <summary>
        ///   Gets or sets the structure that contains extra data.
        /// </summary>
        /// <value></value>
        /// <returns>An <see cref = "T:System.Runtime.Serialization.ExtensionDataObject" /> that contains data that is not recognized as belonging to the data contract.</returns>
        public ExtensionDataObject ExtensionData { get; set; }

        #endregion

        /// <summary>
        ///   Creates the specified message id.
        /// </summary>
        /// <param name = "messageId">The message id.</param>
        /// <param name = "feature">The feature.</param>
        /// <returns>An initialized UpdateFeatureStateResponse instance.</returns>
        public static UpdateFeatureStateResponse Create(string messageId, Feature feature)
        {
            return new UpdateFeatureStateResponse
                       {
                           Header = MessageHeader.Create(messageId),
                           Result = feature
                       };
        }
    }
}