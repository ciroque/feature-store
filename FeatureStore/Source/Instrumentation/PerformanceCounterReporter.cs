// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PerformanceCounterReporter.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Wraps the creation and reporting of operation count and execution time performance counters.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.Instrumentation
{
    using System;
    using System.Diagnostics;

    /// <summary>
    ///   Wraps the creation and reporting of operation count and execution time performance counters.
    /// </summary>
    public class PerformanceCounterReporter : IDisposable
    {
        /// <summary>
        ///   The counter used to report the duration of the operation.
        /// </summary>
        private readonly PerformanceCounter m_ExecutionTimeCounter;

        /// <summary>
        ///   The counter used to record the number of times the operation is executed.
        /// </summary>
        private readonly PerformanceCounter m_OperationCountCounter;

        /// <summary>
        ///   Used to time the execution of the operations within the using context.
        /// </summary>
        private readonly Stopwatch m_Stopwatch;

        /// <summary>
        ///   Initializes a new instance of the <see cref = "PerformanceCounterReporter" /> class.
        /// </summary>
        /// <param name = "categoryName">Name of the category.</param>
        /// <param name = "operationCountCounterName">Name of the operation count counter.</param>
        /// <param name = "executionTimeCounterName">Name of the execution time counter.</param>
        internal PerformanceCounterReporter(
            string categoryName, string operationCountCounterName, string executionTimeCounterName)
        {
            m_OperationCountCounter = new PerformanceCounter(categoryName, operationCountCounterName, false);
            m_ExecutionTimeCounter = new PerformanceCounter(categoryName, executionTimeCounterName, false);
            m_Stopwatch = new Stopwatch();
            m_Stopwatch.Start();
        }

        /// <summary>
        ///   Gets the operation count value.
        /// </summary>
        /// <value>The operation count value.</value>
        public float OperationCountValue
        {
            get { return m_OperationCountCounter == null ? 0 : m_OperationCountCounter.NextValue(); }
        }

        /// <summary>
        ///   Gets the execution time value.
        /// </summary>
        /// <value>The execution time value.</value>
        public float ExecutionTimeValue
        {
            get { return m_ExecutionTimeCounter.NextValue(); }
        }

        #region IDisposable Members

        /// <summary>
        ///   Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        /// <summary>
        ///   Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name = "disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                m_Stopwatch.Stop();
                m_ExecutionTimeCounter.RawValue = m_Stopwatch.ElapsedMilliseconds;
                m_OperationCountCounter.Increment();

                m_ExecutionTimeCounter.Dispose();
                m_OperationCountCounter.Dispose();
            }
        }
    }
}