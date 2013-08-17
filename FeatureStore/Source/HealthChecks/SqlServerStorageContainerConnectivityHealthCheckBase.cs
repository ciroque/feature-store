// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlServerStorageContainerConnectivityHealthCheckBase.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Base class for <see cref="IHealthCheck" /> implementations that perform checks against the SqlServerStorageContainer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.HealthChecks
{
    using System;
    using System.Data.Common;
    using System.Globalization;
    using log4net;

    /// <summary>
    ///   Base class for <see cref = "IHealthCheck" /> implementations that perform checks against the SqlServerStorageContainer.
    /// </summary>
    public abstract class SqlServerStorageContainerConnectivityHealthCheckBase : IHealthCheck
    {
        /// <summary>
        ///   Gets or sets the DbConnection instance against which the checks will be performed.
        /// </summary>
        protected DbConnection Connection { get; set; }

        /// <summary>
        ///   Gets or sets ILog instance used to write events to the log.
        /// </summary>
        protected ILog Log { get; set; }

        #region IHealthCheck Members

        /// <summary>
        ///   Executes the Health Check implementation.
        /// </summary>
        /// <returns>
        ///   An <see cref = "IHealthCheckResult" /> instance that specifies the outcome of the Health Check.
        /// </returns>
        public abstract IHealthCheckResult Execute();

        #endregion

        /// <summary>
        ///   Processes the exception by building a <see cref = "HealthCheckResult" /> instance and writing the exception to the log.
        /// </summary>
        /// <param name = "e">The exception to be processed.</param>
        /// <param name = "messageFormat">The format string to be used in the string.Format call.</param>
        /// <returns><see cref = "IHealthCheckResult" /> instance representing the failed state of the health check.</returns>
        protected IHealthCheckResult ProcessException(Exception e, string messageFormat)
        {
            string message = string.Format(
                CultureInfo.CurrentUICulture,
                messageFormat,
                e.Message,
                Connection.ConnectionString);

            Log.Error(message, e);

            return HealthCheckResult.Create(false, message);
        }
    }
}