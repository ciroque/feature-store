// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HealthCheckResult.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Represents the results of a Health Check algorithm
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.HealthChecks
{
    /// <summary>
    ///   Represents the results of a Health Check algorithm
    /// </summary>
    public class HealthCheckResult : IHealthCheckResult
    {
        /// <summary>
        ///   Prevents a default instance of the HealthCheckResult class from being created.
        /// </summary>
        private HealthCheckResult()
        {
        }

        #region IHealthCheckResult Members

        /// <summary>
        ///   Gets a value indicating whether this <see cref = "HealthCheckResult" /> is passed.
        /// </summary>
        /// <value><c>true</c> if passed; otherwise, <c>false</c>.</value>
        public bool Passed { get; private set; }

        /// <summary>
        ///   Gets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; private set; }

        #endregion

        /// <summary>
        ///   Creates the specified passed.
        /// </summary>
        /// <param name = "passed">if set to <c>true</c> [passed].</param>
        /// <param name = "message">The message.</param>
        /// <returns>An intitialized instance of the HealthCheckResult class.</returns>
        public static HealthCheckResult Create(bool passed, string message)
        {
            return new HealthCheckResult { Passed = passed, Message = message };
        }
    }
}