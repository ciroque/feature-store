// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CheckFeatureStateResponse.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Return value for the CheckFeatureState method on the <see cref="IFeatureStore" /> interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.Mutual
{
    using System.Runtime.Serialization;

    /// <summary>
    ///   Return value for the CheckFeatureState method on the <see cref = "IFeatureStore" /> interface.
    /// </summary>
    [KnownType(typeof(CheckFeatureStateResponse))]
    [DataContract]
    public class CheckFeatureStateResponse : IExtensibleDataObject
    {
        /// <summary>
        ///   Gets the result.
        /// </summary>
        /// <value>The result.</value>
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
        /// <returns>An initialized CheckFeatureStateResponse instance.</returns>
        public static CheckFeatureStateResponse Create(string messageId, Feature feature)
        {
            return new CheckFeatureStateResponse
                       {
                           Header = MessageHeader.Create(messageId),
                           Result = feature
                       };
        }
    }
}