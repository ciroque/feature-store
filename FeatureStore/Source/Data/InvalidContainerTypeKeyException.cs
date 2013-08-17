// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InvalidContainerTypeKeyException.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   The exception thrown when an invalid StorageContainerTypeKey is specified.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.Data
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;

    /// <summary>
    ///   The exception thrown when an invalid StorageContainerTypeKey is specified.
    /// </summary>
    [Serializable]
    public class InvalidContainerTypeKeyException : Exception
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref = "InvalidContainerTypeKeyException" /> class.
        /// </summary>
        public InvalidContainerTypeKeyException()
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "InvalidContainerTypeKeyException" /> class.
        /// </summary>
        /// <param name = "message">The message.</param>
        public InvalidContainerTypeKeyException(string message)
            : base(
                string.Format(
                    CultureInfo.CurrentUICulture,
                    ExceptionMessageResources.INVALID_CONTAINER_TYPE_KEY,
                    message))
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "InvalidContainerTypeKeyException" /> class.
        /// </summary>
        /// <param name = "message">The message.</param>
        /// <param name = "innerException">The inner exception.</param>
        public InvalidContainerTypeKeyException(string message, Exception innerException)
            : base(
                string.Format(
                    CultureInfo.CurrentUICulture,
                    ExceptionMessageResources.INVALID_CONTAINER_TYPE_KEY,
                    message),
                innerException)
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "InvalidContainerTypeKeyException" /> class.
        /// </summary>
        /// <param name = "info">The <see cref = "T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name = "context">The <see cref = "T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        /// <exception cref = "T:System.ArgumentNullException">The <paramref name = "info" /> parameter is null. </exception>
        /// <exception cref = "T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref = "P:System.Exception.HResult" /> is zero (0). </exception>
        protected InvalidContainerTypeKeyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}