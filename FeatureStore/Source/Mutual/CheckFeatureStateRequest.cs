// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CheckFeatureStateRequest.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Parameter for the CheckFeatureState method on the <see cref="IFeatureStore" /> interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.Mutual
{
    using System.Runtime.Serialization;

    /// <summary>
    ///   Parameter for the CheckFeatureState method on the <see cref = "IFeatureStore" /> interface.
    /// </summary>
    [KnownType(typeof(CheckFeatureStateRequest))]
    [DataContract]
    public class CheckFeatureStateRequest : IExtensibleDataObject
    {
        /// <summary>
        ///   Gets the header.
        /// </summary>
        /// <value>The header.</value>
        [DataMember]
        public MessageHeader Header { get; private set; }

        /// <summary>
        ///   Gets the query.
        /// </summary>
        /// <value>The query.</value>
        [DataMember]
        public FeatureKey Key { get; private set; }

        #region IExtensibleDataObject Members

        /// <summary>
        ///   Gets or sets the structure that contains extra data.
        /// </summary>
        /// <value></value>
        /// <returns>An <see cref = "T:System.Runtime.Serialization.ExtensionDataObject" /> that contains data that is not recognized as belonging to the data contract.</returns>
        public ExtensionDataObject ExtensionData { get; set; }

        #endregion

        /// <summary>
        ///   Creates the specified query.
        /// </summary>
        /// <param name = "messageId">The caller-defined id for the message.</param>
        /// <param name = "query">The query.</param>
        /// <returns>An initialized CheckFeatureStateRequest instance.</returns>
        public static CheckFeatureStateRequest Create(string messageId, FeatureKey query)
        {
            return new CheckFeatureStateRequest
                       {
                           Header = MessageHeader.Create(messageId),
                           Key = query
                       };
        }
    }
}