// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHealthCheck.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Represents the elements necessary to execute a Health Check.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.HealthChecks
{
    /// <summary>
    ///   Represents the elements necessary to execute a Health Check.
    /// </summary>
    public interface IHealthCheck
    {
        /// <summary>
        ///   Executes the Health Check implementation.
        /// </summary>
        /// <returns>An <see cref = "IHealthCheckResult" /> instance that specifies the outcome of the Health Check.</returns>
        IHealthCheckResult Execute();
    }
}