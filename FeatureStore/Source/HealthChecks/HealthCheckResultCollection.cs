// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HealthCheckResultCollection.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Represents a list of <see cref="IHealthCheckResult" /> instances.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.HealthChecks
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    ///   Represents a list of <see cref = "IHealthCheckResult" /> instances.
    /// </summary>
    public class HealthCheckResultCollection : Collection<IHealthCheckResult>
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref = "HealthCheckResultCollection" /> class.
        /// </summary>
        /// <param name = "results">The results.</param>
        public HealthCheckResultCollection(IList<IHealthCheckResult> results) : base(results)
        {
        }
    }
}