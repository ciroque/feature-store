// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHealthCheckResult.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Defines the elements necessary to report the results of an <see cref="IHealthCheck" /> implementation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.HealthChecks
{
    /// <summary>
    ///   Defines the elements necessary to report the results of an <see cref = "IHealthCheck" /> implementation.
    /// </summary>
    public interface IHealthCheckResult
    {
        /// <summary>
        ///   Gets a value indicating whether this <see cref = "IHealthCheckResult" /> is passed.
        /// </summary>
        /// <value><c>true</c> if passed; otherwise, <c>false</c>.</value>
        bool Passed { get; }

        /// <summary>
        ///   Gets the message.
        /// </summary>
        /// <value>The message.</value>
        string Message { get; }
    }
}