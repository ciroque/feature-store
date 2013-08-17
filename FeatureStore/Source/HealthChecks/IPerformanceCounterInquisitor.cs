// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPerformanceCounterInquisitor.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Defines the interface used to query the Performance Counter database
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.HealthChecks
{
    /// <summary>
    ///   Defines the interface used to query the Performance Counter database
    /// </summary>
    public interface IPerformanceCounterInquisitor
    {
        /// <summary>
        ///   Checks the counter exists.
        /// </summary>
        /// <param name = "counterName">Name of the counter.</param>
        /// <returns><code>true</code> if the counter exists, <code>false</code> otherwise.</returns>
        bool CheckCounterExists(string counterName);

        /// <summary>
        ///   Checks the category exists.
        /// </summary>
        /// <param name = "categoryName">Name of the category.</param>
        /// <returns><code>true</code> if the category exists, <code>false</code> otherwise.</returns>
        bool CheckCategoryExists(string categoryName);
    }
}