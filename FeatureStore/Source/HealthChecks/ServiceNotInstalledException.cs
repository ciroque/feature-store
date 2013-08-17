// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceNotInstalledException.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Represents the exception thrown when the service is not found in the Service Control Manager database (it has not been installed).
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.HealthChecks
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    ///   Represents the exception thrown when the service is not found in the Service Control Manager database (it has not been installed).
    /// </summary>
    public class ServiceNotInstalledException : Exception
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref = "ServiceNotInstalledException" /> class.
        /// </summary>
        public ServiceNotInstalledException()
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "ServiceNotInstalledException" /> class.
        /// </summary>
        /// <param name = "message">The message.</param>
        public ServiceNotInstalledException(string message) : base(message)
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "ServiceNotInstalledException" /> class.
        /// </summary>
        /// <param name = "message">The message.</param>
        /// <param name = "innerException">The inner exception.</param>
        public ServiceNotInstalledException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "ServiceNotInstalledException" /> class.
        /// </summary>
        /// <param name = "info">The <see cref = "T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name = "context">The <see cref = "T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        /// <exception cref = "T:System.ArgumentNullException">The <paramref name = "info" /> parameter is null. </exception>
        /// <exception cref = "T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref = "P:System.Exception.HResult" /> is zero (0). </exception>
        protected ServiceNotInstalledException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}