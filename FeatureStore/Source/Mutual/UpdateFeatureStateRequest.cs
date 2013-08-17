// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateFeatureStateRequest.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Represents the state necessary to execute the UpdateFeature method.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.Mutual
{
    using System.Runtime.Serialization;

    /// <summary>
    ///   Represents the state necessary to execute the UpdateFeature method.
    /// </summary>
    [KnownType(typeof(UpdateFeatureStateRequest))]
    [DataContract]
    public class UpdateFeatureStateRequest : IExtensibleDataObject
    {
        /// <summary>
        ///   Gets the query.
        /// </summary>
        /// <value>The query.</value>
        [DataMember]
        public FeatureKey Key { get; private set; }

        /// <summary>
        ///   Gets a value indicating whether [new state].
        /// </summary>
        /// <value><c>true</c> if [new state]; otherwise, <c>false</c>.</value>
        [DataMember]
        public bool NewState { get; private set; }

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
        ///   Creates the specified feature query.
        /// </summary>
        /// <param name = "messageId">The caller-defined id for the message.</param>
        /// <param name = "query">The feature query.</param>
        /// <param name = "newState">if set to <c>true</c> [new state].</param>
        /// <returns>An initialized UpdateFeatureStateRequest instance.</returns>
        public static UpdateFeatureStateRequest Create(string messageId, FeatureKey query, bool newState)
        {
            return new UpdateFeatureStateRequest
                       {
                           Header = MessageHeader.Create(messageId),
                           NewState = newState,
                           Key = query
                       };
        }
    }
}