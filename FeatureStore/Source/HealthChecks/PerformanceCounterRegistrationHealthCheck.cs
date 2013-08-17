// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PerformanceCounterRegistrationHealthCheck.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Implements an <see cref="IHealthCheck" /> that ensures the Performance Counters have been successfully registered.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.HealthChecks
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using Instrumentation;

    /// <summary>
    ///   Implements an <see cref = "IHealthCheck" /> that ensures the Performance Counters have been successfully registered.
    /// </summary>
    public class PerformanceCounterRegistrationHealthCheck : IHealthCheck
    {
        /// <summary>
        ///   Represents the implementation used to query whether or not the Category and Counters have been registered.
        /// </summary>
        private readonly IPerformanceCounterInquisitor m_Inquisitor;

        /// <summary>
        ///   Initializes a new instance of the <see cref = "PerformanceCounterRegistrationHealthCheck" /> class.
        /// </summary>
        /// <param name = "inquisitor">The inquisitor.</param>
        private PerformanceCounterRegistrationHealthCheck(IPerformanceCounterInquisitor inquisitor)
        {
            m_Inquisitor = inquisitor;
        }

        /// <summary>
        ///   Creates the specified inquisitor.
        /// </summary>
        /// <param name = "inquisitor">The inquisitor.</param>
        /// <returns>An instance of the PerformanceCounterRegistrationHealthCheck class as an <see cref = "IHealthCheck" /></returns>
        public static IHealthCheck Create(IPerformanceCounterInquisitor inquisitor)
        {
            return new PerformanceCounterRegistrationHealthCheck(inquisitor);
        }

        #region IHealthCheck Members

        /// <summary>
        ///   Executes the Health Check implementation.
        /// </summary>
        /// <returns>
        ///   An <see cref = "IHealthCheckResult" /> instance that specifies the outcome of the Health Check.
        /// </returns>
        public IHealthCheckResult Execute()
        {
            IHealthCheckResult result;
            List<string> missingCategories = new List<string>();

            if (m_Inquisitor.CheckCategoryExists(PerformanceCounterRegistrar.CategoryName))
            {
                missingCategories.AddRange(from counterCreationData in PerformanceCounterRegistrar.CounterDefinitions
                                           where !m_Inquisitor.CheckCounterExists(counterCreationData.CounterName)
                                           select counterCreationData.CounterName);

                if (missingCategories.Count == 0)
                {
                    result = HealthCheckResult.Create(true, MessageResources.PERFORMANCE_CATEGORY_AND_COUNTERS_DEFINED);
                }
                else
                {
                    string message = BuildMessage(missingCategories);
                    result = HealthCheckResult.Create(
                        false,
                        string.Format(CultureInfo.CurrentUICulture, MessageResources.PERFORMANCE_CATEGORY_DEFINED_MISSING_COUNTERS, message));
                }
            }
            else
            {
                result = HealthCheckResult.Create(false, MessageResources.PERFORMANCE_CATEGORY_NOT_DEFINED);
            }

            return result;
        }

        #endregion

        /// <summary>
        ///   Builds the message.
        /// </summary>
        /// <param name = "missingCategories">The missing categories.</param>
        /// <returns>A string containing the comma-separated list of missing categories.</returns>
        private static string BuildMessage(List<string> missingCategories)
        {
            int count = missingCategories.Count;
            int last = count - 1;
            StringBuilder builder = new StringBuilder();
            for (int index = 0; index < count; index++)
            {
                if (index == last)
                {
                    builder.Append(missingCategories[index]);
                }
                else
                {
                    builder.AppendFormat(CultureInfo.CurrentUICulture, "{0}, ", missingCategories[index]);
                }
            }

            return builder.ToString();
        }
    }
}