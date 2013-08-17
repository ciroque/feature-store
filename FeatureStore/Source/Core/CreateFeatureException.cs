// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateFeatureException.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   The exception that is thrown when there is a problem creating a new <see cref="Feature" />.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.Core
{
    using System;
    using System.Runtime.Serialization;
    using Mutual;

    /// <summary>
    ///   The exception that is thrown when there is a problem creating a new <see cref = "Feature" />.
    /// </summary>
    [Serializable]
    public class CreateFeatureException : Exception
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref = "CreateFeatureException" /> class.
        /// </summary>
        public CreateFeatureException()
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "CreateFeatureException" /> class.
        /// </summary>
        /// <param name = "message">The message.</param>
        /// <param name = "inner">The inner.</param>
        public CreateFeatureException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "CreateFeatureException" /> class.
        /// </summary>
        /// <param name = "message">The message.</param>
        public CreateFeatureException(string message) : base(message)
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "CreateFeatureException" /> class.
        /// </summary>
        /// <param name = "info">The <see cref = "T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name = "context">The <see cref = "T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        /// <exception cref = "T:System.ArgumentNullException">The <paramref name = "info" /> parameter is null. </exception>
        /// <exception cref = "T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref = "P:System.Exception.HResult" /> is zero (0). </exception>
        protected CreateFeatureException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}