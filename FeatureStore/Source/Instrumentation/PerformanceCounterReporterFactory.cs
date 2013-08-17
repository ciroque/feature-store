// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PerformanceCounterReporterFactory.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Responsible for creating instances of the <see cref="PerformanceCounterReporter" /> class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.Instrumentation
{
    using System;
    using System.Text;
    using System.Threading;
    using log4net;
    using log4net.Config;

    /// <summary>
    ///   Responsible for creating instances of the <see cref = "PerformanceCounterReporter" /> class.
    /// </summary>
    public static class PerformanceCounterReporterFactory
    {
        /// <summary>
        ///   Creates the reporter.
        /// </summary>
        /// <param name = "reporterType">Type of the reporter.</param>
        /// <returns>An intialized <see cref = "PerformanceCounterReporter" /> instance.</returns>
        public static PerformanceCounterReporter CreateReporter(PerformanceCounterReporterType reporterType)
        {
            string operationCounterName;
            string executionTimeCounterName;

            switch (reporterType)
            {
                case PerformanceCounterReporterType.CheckFeatureState:
                    {
                        operationCounterName = PerformanceCounterNameResources.CHECK_FEATURE_STATE_CALL_COUNT;
                        executionTimeCounterName = PerformanceCounterNameResources.CHECK_FEATURE_STATE_EXECUTION_TIME;
                        break;
                    }

                case PerformanceCounterReporterType.CreateFeature:
                    {
                        operationCounterName = PerformanceCounterNameResources.CREATE_FEATURE_CALL_COUNT;
                        executionTimeCounterName = PerformanceCounterNameResources.CREATE_FEATURE_EXECUTION_TIME;
                        break;
                    }

                case PerformanceCounterReporterType.UpdateFeatureState:
                    {
                        operationCounterName = PerformanceCounterNameResources.UPDATE_FEATURE_STATE_COUNT;
                        executionTimeCounterName = PerformanceCounterNameResources.UPDATE_FEATURE_STATE_EXECUTION_TIME;
                        break;
                    }

                case PerformanceCounterReporterType.RetrieveDefinedFeatures:
                    {
                        operationCounterName = PerformanceCounterNameResources.RETRIEVE_DEFINED_FEATURES_COUNT;
                        executionTimeCounterName =
                            PerformanceCounterNameResources.RETRIEVE_DEFINED_FEATURES_EXECUTION_TIME;
                        break;
                    }

                default:
                    {
                        throw new UnknownReporterTypeException(reporterType);
                    }
            }

            return
                CreateReporter(
                    PerformanceCounterNameResources.PERFORMANCE_COUNTER_CATEGORY_NAME,
                    operationCounterName,
                    executionTimeCounterName);
        }

        /// <summary>
        ///   Creates the reporter.
        /// </summary>
        /// <param name = "categoryName">Name of the category.</param>
        /// <param name = "operationCounterName">Name of the operation counter.</param>
        /// <param name = "executionTimeCounterName">Name of the execution time counter.</param>
        /// <returns>An intialized <see cref = "PerformanceCounterReporter" /> instance.</returns>
        public static PerformanceCounterReporter CreateReporter(
            string categoryName, 
            string operationCounterName,
            string executionTimeCounterName)
        {
            PerformanceCounterReporter reporter = null;
            try
            {
                reporter = new PerformanceCounterReporter(categoryName, operationCounterName, executionTimeCounterName);
            }
            catch (NullReferenceException nre)
            {
                RecordException(nre);
            }
            catch (InvalidOperationException e)
            {
                RecordException(e);
            }

            return reporter;
        }

        /// <summary>
        ///   Records the exception.
        /// </summary>
        /// <param name = "e">The exception instance to be logged.</param>
        private static void RecordException(Exception e)
        {
            ThreadPool.QueueUserWorkItem(ex =>
                                             {
                                                 StringBuilder builder = new StringBuilder();

                                                 builder.AppendFormat(
                                                     ErrorMessageResources.COUNTER_REPORTER_CREATION_FAILED, e.Message);
                                                 builder.AppendLine(e.ToString());

                                                 Exception inner = e.InnerException;
                                                 while (inner != null)
                                                 {
                                                     builder.AppendLine(inner.ToString());
                                                     inner = inner.InnerException;
                                                 }

                                                 XmlConfigurator.Configure();
                                                 ILog log =
                                                     LogManager.GetLogger(typeof(PerformanceCounterReporterFactory));
                                                 log.Error(builder.ToString());
                                             });
        }
    }
}