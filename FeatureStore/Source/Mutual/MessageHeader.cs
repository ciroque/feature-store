// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageHeader.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Contains fields generic to all interactions with the IFeatureStore interface
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.Mutual
{
    using System;
    using System.Globalization;
    using System.Net;
    using System.Runtime.Serialization;
    using System.Text;

    /// <summary>
    ///   Contains fields generic to all interactions with the IFeatureStore interface
    /// </summary>
    [Serializable]
    [DataContract]
    public class MessageHeader
    {
        /// <summary>
        ///   Prevents a default instance of the <see cref = "MessageHeader" /> class from being created.
        /// </summary>
        private MessageHeader()
        {
        }

        /// <summary>
        ///   Gets the message id.
        /// </summary>
        /// <value>The message id.</value>
        [DataMember]
        public string MessageId { get; private set; }

        /// <summary>
        ///   Gets the source ip address.
        /// </summary>
        /// <value>The source ip address.</value>
        [DataMember]
        public string OriginationIpAddress { get; private set; }

        /// <summary>
        ///   Gets the name of the originzation host.
        /// </summary>
        /// <value>The name of the originzation host.</value>
        [DataMember]
        public string OriginationHostName { get; private set; }

        /// <summary>
        ///   Creates the specified message id.
        /// </summary>
        /// <param name = "messageId">The message id.</param>
        /// <returns>An initialized MessageHeader instance.</returns>
        public static MessageHeader Create(string messageId)
        {
            string hostName = Dns.GetHostName();
            StringBuilder builder = new StringBuilder();
            foreach (IPAddress address in Dns.GetHostAddresses(hostName))
            {
                builder.AppendFormat(CultureInfo.CurrentUICulture, "({0})", address);
            }

            return new MessageHeader
                       {
                           MessageId = messageId,
                           OriginationHostName = hostName,
                           OriginationIpAddress = builder.ToString()
                       };
        }
    }
}