// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PerformanceCounterReporterType.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Defines the various Performance Counter Reporter types available.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.Instrumentation
{
    /// <summary>
    ///   Defines the various Performance Counter Reporter types available.
    /// </summary>
    public enum PerformanceCounterReporterType
    {
        /// <summary>
        ///   Represents the CreateFeature method.
        /// </summary>
        CreateFeature,

        /// <summary>
        ///   Represents the CheckFeatureState method.
        /// </summary>
        CheckFeatureState,

        /// <summary>
        ///   Represents the UpdateFeatureState method.
        /// </summary>
        UpdateFeatureState,

        /// <summary>
        ///   Represents the RetrieveDefinedFeatures method.
        /// </summary>
        RetrieveDefinedFeatures
    }
}