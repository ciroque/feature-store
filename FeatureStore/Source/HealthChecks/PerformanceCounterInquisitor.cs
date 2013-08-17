// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PerformanceCounterInquisitor.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Implements the <see cref="IPerformanceCounterInquisitor" /> interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.HealthChecks
{
    using System.Diagnostics;
    using System.Linq;
    using Instrumentation;

    /// <summary>
    ///   Implements the <see cref = "IPerformanceCounterInquisitor" /> interface.
    /// </summary>
    public class PerformanceCounterInquisitor : IPerformanceCounterInquisitor
    {
        /// <summary>
        ///   Prevents a default instance of the <see cref = "PerformanceCounterInquisitor" /> class from being created.
        /// </summary>
        private PerformanceCounterInquisitor()
        {
        }

        /// <summary>
        ///   Creates an instance of the PerformanceCounterInquisitor class.
        /// </summary>
        /// <returns>An initialized instance of the PerformanceCounterInquisitor class as an <see cref = "IPerformanceCounterInquisitor" /></returns>
        public static IPerformanceCounterInquisitor Create()
        {
            return new PerformanceCounterInquisitor();
        }

        #region IPerformanceCounterInquisitor Members

        /// <summary>
        ///   Checks the counter exists.
        /// </summary>
        /// <param name = "counterName">Name of the counter.</param>
        /// <returns>
        ///   <code>true</code> if the counter exists, <code>false</code> otherwise.
        /// </returns>
        public bool CheckCounterExists(string counterName)
        {
            return PerformanceCounterCategory.CounterExists(counterName, PerformanceCounterRegistrar.CategoryName);
        }

        /// <summary>
        ///   Checks the category exists.
        /// </summary>
        /// <param name = "categoryName">Name of the category.</param>
        /// <returns>
        ///   <code>true</code> if the category exists, <code>false</code> otherwise.
        /// </returns>
        public bool CheckCategoryExists(string categoryName)
        {
            var q = from category in PerformanceCounterCategory.GetCategories()
                    where category.CategoryName == categoryName
                    select category;
            return q.Count() > 0;
        }

        #endregion
    }
}