// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnknownReporterTypeException.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   The exception that is thrown when an error occurs when an unknown <see cref="PerformanceCounterReporterType" /> is used.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.Instrumentation
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;

    /// <summary>
    ///   The exception that is thrown when an error occurs when an unknown <see cref = "PerformanceCounterReporterType" /> is used.
    /// </summary>
    [Serializable]
    public class UnknownReporterTypeException : Exception
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref = "UnknownReporterTypeException" /> class.
        /// </summary>
        public UnknownReporterTypeException()
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "UnknownReporterTypeException" /> class.
        /// </summary>
        /// <param name = "reporterType">Type of the reporter.</param>
        public UnknownReporterTypeException(PerformanceCounterReporterType reporterType)
            : base(
                string.Format(CultureInfo.CurrentUICulture, ErrorMessageResources.INVALID_PERFORMANCE_COUNTER_REPORTER_TYPE, reporterType))
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "UnknownReporterTypeException" /> class.
        /// </summary>
        /// <param name = "message">The message.</param>
        public UnknownReporterTypeException(string message) : base(message)
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "UnknownReporterTypeException" /> class.
        /// </summary>
        /// <param name = "message">The message.</param>
        /// <param name = "innerException">The inner exception.</param>
        public UnknownReporterTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "UnknownReporterTypeException" /> class.
        /// </summary>
        /// <param name = "info">The <see cref = "T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name = "context">The <see cref = "T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        /// <exception cref = "T:System.ArgumentNullException">The <paramref name = "info" /> parameter is null. </exception>
        /// <exception cref = "T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref = "P:System.Exception.HResult" /> is zero (0). </exception>
        protected UnknownReporterTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}