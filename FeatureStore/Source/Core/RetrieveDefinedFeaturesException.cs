// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RetrieveDefinedFeaturesException.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   The exception that is thrown when an error occurs calling the RetrieveDefinedFeatures method
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.Core
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    ///   The exception that is thrown when an error occurs calling the RetrieveDefinedFeatures method
    /// </summary>
    [Serializable]
    public class RetrieveDefinedFeaturesException : Exception
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref = "RetrieveDefinedFeaturesException" /> class.
        /// </summary>
        public RetrieveDefinedFeaturesException()
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "RetrieveDefinedFeaturesException" /> class.
        /// </summary>
        /// <param name = "message">The message.</param>
        public RetrieveDefinedFeaturesException(string message) : base(message)
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "RetrieveDefinedFeaturesException" /> class.
        /// </summary>
        /// <param name = "message">The message.</param>
        /// <param name = "innerException">The inner exception.</param>
        public RetrieveDefinedFeaturesException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "RetrieveDefinedFeaturesException" /> class.
        /// </summary>
        /// <param name = "info">The <see cref = "T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name = "context">The <see cref = "T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        /// <exception cref = "T:System.ArgumentNullException">The <paramref name = "info" /> parameter is null. </exception>
        /// <exception cref = "T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref = "P:System.Exception.HResult" /> is zero (0). </exception>
        protected RetrieveDefinedFeaturesException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}