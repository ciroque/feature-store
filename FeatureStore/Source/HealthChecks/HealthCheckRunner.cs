// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HealthCheckRunner.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Manages running <see cref="IHealthCheck" /> implementations and reports the results.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.HealthChecks
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using log4net;
    using log4net.Config;

    /// <summary>
    ///   Manages running <see cref = "IHealthCheck" /> implementations and reports the results.
    /// </summary>
    public class HealthCheckRunner
    {
        /// <summary>
        ///   Access to the log.
        /// </summary>
        private readonly ILog m_Log;

        /// <summary>
        ///   Prevents a default instance of the <see cref = "HealthCheckRunner" /> class from being created.
        /// </summary>
        private HealthCheckRunner()
        {
            XmlConfigurator.Configure();
            m_Log = LogManager.GetLogger(typeof(HealthCheckRunner));
        }

        /// <summary>
        ///   Runs the health checks.
        /// </summary>
        /// <param name = "healthChecks">The health check list.</param>
        /// <returns>A <see cref = "HealthCheckResultCollection" /> containing the results of the given list of <see cref = "IHealthCheck" /> implementations.</returns>
        public static HealthCheckResultCollection RunHealthChecks(HealthChecklistCollection healthChecks)
        {
            HealthCheckRunner runner = new HealthCheckRunner();
            return
                new HealthCheckResultCollection(
                    new List<IHealthCheckResult>(from check in healthChecks select runner.RunHealthChecks(check)));
        }

        /// <summary>
        ///   Runs the health checks.
        /// </summary>
        /// <param name = "healthCheck">The health check.</param>
        /// <returns>A <see cref = "IHealthCheckResult" /> containing the results of the given <see cref = "IHealthCheck" /> implementation.</returns>
        private IHealthCheckResult RunHealthChecks(IHealthCheck healthCheck)
        {
            IHealthCheckResult result;
            try
            {
                result = healthCheck.Execute();
            }
            catch (Exception e)
            {
                m_Log.Warn(e);
                result = HealthCheckResult.Create(
                    false,
                    string.Format(CultureInfo.CurrentUICulture, ExceptionMessageResources.EXCEPTION_THROWN_IN_HEALTH_CHECK, e.Message));
            }

            return result;
        }
    }
}