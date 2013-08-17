// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PerformanceCounterRegistrar.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Encapsulates the Performance Monitor implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.Instrumentation
{
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    ///   Encapsulates the Performance Monitor implementation
    /// </summary>
    public static class PerformanceCounterRegistrar
    {
        /// <summary>
        ///   Gets the counter definitions.
        /// </summary>
        /// <value>The counter definitions.</value>
        public static IEnumerable<CounterCreationData> CounterDefinitions
        {
            get
            {
                return new List<CounterCreationData>
                           {
                               new CounterCreationData(
                                   PerformanceCounterNameResources.CHECK_FEATURE_STATE_CALL_COUNT,
                                   PerformanceCounterNameResources.CHECK_FEATURE_STATE_CALL_COUNT_HELP,
                                   PerformanceCounterType.NumberOfItems64),
                               new CounterCreationData(
                                   PerformanceCounterNameResources.CHECK_FEATURE_STATE_EXECUTION_TIME,
                                   PerformanceCounterNameResources.CHECK_FEATURE_STATE_EXECUTION_TIME_HELP,
                                   PerformanceCounterType.NumberOfItems64),
                               new CounterCreationData(
                                   PerformanceCounterNameResources.CREATE_FEATURE_CALL_COUNT,
                                   PerformanceCounterNameResources.CREATE_FEATURE_CALL_COUNT_HELP,
                                   PerformanceCounterType.NumberOfItems64),
                               new CounterCreationData(
                                   PerformanceCounterNameResources.CREATE_FEATURE_EXECUTION_TIME,
                                   PerformanceCounterNameResources.CREATE_FEATURE_EXECUTION_TIME_HELP,
                                   PerformanceCounterType.NumberOfItems64),
                               new CounterCreationData(
                                   PerformanceCounterNameResources.UPDATE_FEATURE_STATE_COUNT,
                                   PerformanceCounterNameResources.UPDATE_FEATURE_STATE_COUNT_HELP,
                                   PerformanceCounterType.NumberOfItems64),
                               new CounterCreationData(
                                   PerformanceCounterNameResources.UPDATE_FEATURE_STATE_EXECUTION_TIME,
                                   PerformanceCounterNameResources.UPDATE_FEATURE_STATE_EXECUTION_TIME_HELP,
                                   PerformanceCounterType.NumberOfItems64),
                               new CounterCreationData(
                                   PerformanceCounterNameResources.RETRIEVE_DEFINED_FEATURES_COUNT,
                                   PerformanceCounterNameResources.RETRIEVE_DEFINED_FEATURES_COUNT_HELP,
                                   PerformanceCounterType.NumberOfItems64),
                               new CounterCreationData(
                                   PerformanceCounterNameResources.RETRIEVE_DEFINED_FEATURES_EXECUTION_TIME,
                                   PerformanceCounterNameResources.RETRIEVE_DEFINED_FEATURES_EXECUTION_TIME_HELP,
                                   PerformanceCounterType.NumberOfItems64)
                           };
            }
        }

        /// <summary>
        ///   Gets the name of the category.
        /// </summary>
        /// <value>The name of the category.</value>
        public static string CategoryName
        {
            get { return PerformanceCounterNameResources.PERFORMANCE_COUNTER_CATEGORY_NAME; }
        }

        /// <summary>
        ///   Ensures the Performance Counters exist.
        /// </summary>
        public static void EnsureExist()
        {
            if (!PerformanceCounterCategory.Exists(CategoryName))
            {
                CounterCreationDataCollection counters = new CounterCreationDataCollection();

                foreach (CounterCreationData counterCreationData in CounterDefinitions)
                {
                    counters.Add(counterCreationData);
                }

                PerformanceCounterCategory.Create(
                    CategoryName,
                    PerformanceCounterNameResources.PERFORMANCE_COUNTER_CATEGORY_HELP,
                    PerformanceCounterCategoryType.SingleInstance,
                    counters);
            }
        }

        /// <summary>
        ///   Removes this instance.
        /// </summary>
        public static void Remove()
        {
            if (PerformanceCounterCategory.Exists(CategoryName))
            {
                PerformanceCounterCategory.Delete(CategoryName);
            }
        }
    }
}