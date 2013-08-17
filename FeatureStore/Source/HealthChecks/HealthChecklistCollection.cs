// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HealthChecklistCollection.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Represents a list of Health Check implementations
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.HealthChecks
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    ///   Represents a list of Health Check implementations
    /// </summary>
    public class HealthChecklistCollection : Collection<IHealthCheck>
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref = "HealthChecklistCollection" /> class.
        /// </summary>
        /// <param name = "healthChecks">The health checks.</param>
        public HealthChecklistCollection(IList<IHealthCheck> healthChecks) : base(healthChecks)
        {
        }
    }
}