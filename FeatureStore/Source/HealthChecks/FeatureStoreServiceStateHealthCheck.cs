// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FeatureStoreServiceStateHealthCheck.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   An implementation of the <see cref="IHealthCheck" /> interface that checks the state of the Feature Store service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.HealthChecks
{
    /// <summary>
    ///   An implementation of the <see cref = "IHealthCheck" /> interface that checks the state of the Feature Store service.
    /// </summary>
    public class FeatureStoreServiceStateHealthCheck : IHealthCheck
    {
        /// <summary>
        ///   Instance of an <see cref = "IServiceStateInquisitor" /> implementation used to inquire as to the state of the Feature Store service.
        /// </summary>
        private readonly IServiceStateInquisitor m_ServiceStateInquisitor;

        /// <summary>
        ///   Initializes a new instance of the <see cref = "FeatureStoreServiceStateHealthCheck" /> class.
        /// </summary>
        /// <param name = "serviceStateInquisitor">The service state inquisitor.</param>
        private FeatureStoreServiceStateHealthCheck(IServiceStateInquisitor serviceStateInquisitor)
        {
            m_ServiceStateInquisitor = serviceStateInquisitor;
        }

        #region IHealthCheck Members

        /// <summary>
        ///   Creates the specified service state inquisitor.
        /// </summary>
        /// <param name = "serviceStateInquisitor">The service state inquisitor.</param>
        /// <returns>An initialzed instance of the FeatureStoreServiceStateHealthCheck class as an <see cref = "IHealthCheck" />.</returns>
        public static IHealthCheck Create(IServiceStateInquisitor serviceStateInquisitor)
        {
            return new FeatureStoreServiceStateHealthCheck(serviceStateInquisitor);
        }

        /// <summary>
        ///   Executes the Health Check implementation.
        /// </summary>
        /// <returns>
        ///   An <see cref = "IHealthCheckResult" /> instance that specifies the outcome of the Health Check.
        /// </returns>
        public IHealthCheckResult Execute()
        {
            HealthCheckResult result;

            try
            {
                result = 
                    this.m_ServiceStateInquisitor.ServiceIsRunning() 
                    ? HealthCheckResult.Create(true, MessageResources.FEATURE_STORE_SERVICE_IS_RUNNING) 
                    : HealthCheckResult.Create(false, MessageResources.FEATURE_STORE_SERVICE_IS_NOT_RUNNING);
            }
            catch (ServiceNotInstalledException)
            {
                result = HealthCheckResult.Create(false, ExceptionMessageResources.SERVICE_NOT_INSTALLED);
            }

            return result;
        }

        #endregion
    }
}