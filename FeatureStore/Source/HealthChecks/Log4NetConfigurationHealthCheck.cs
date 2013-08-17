// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Log4NetConfigurationHealthCheck.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   An implementation of the <see cref="IHealthCheck" /> interface that checks the state of the log4net configuration.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.HealthChecks
{
    using System.Xml.XPath;

    /// <summary>
    ///   An implementation of the <see cref = "IHealthCheck" /> interface that checks the state of the log4net configuration.
    /// </summary>
    public class Log4NetConfigurationHealthCheck : IHealthCheck
    {
        /// <summary>
        ///   The <see cref = "ILog4NetConfigurationResolver" /> to be used in resolving the log4net configuration.
        /// </summary>
        private readonly XPathNavigator m_XpathNavigator;

        /// <summary>
        ///   Initializes a new instance of the <see cref = "Log4NetConfigurationHealthCheck" /> class.
        /// </summary>
        /// <param name = "xpathNavigator">The resolver.</param>
        private Log4NetConfigurationHealthCheck(XPathNavigator xpathNavigator)
        {
            this.m_XpathNavigator = xpathNavigator;
        }

        /// <summary>
        ///   Creates the specified resolver.
        /// </summary>
        /// <param name = "resolver">The resolver.</param>
        /// <returns>An initialized instance of the Log4NetConfigurationHealthCheck class as an <see cref = "IHealthCheck" />.</returns>
        public static IHealthCheck Create(ILog4NetConfigurationResolver resolver)
        {
            XPathDocument document = resolver.LoadConfiguration();
            XPathNavigator navigator = document.CreateNavigator();
            return new Log4NetConfigurationHealthCheck(navigator);
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
            XPathExpression expression = XPathExpression.Compile(@"boolean(//parameter/parameterName[@value='@Date'])");
            object dateAttributeExists = this.m_XpathNavigator.Evaluate(expression);
            bool xpathSucceeded = dateAttributeExists != null && (bool)dateAttributeExists;

            return HealthCheckResult.Create(xpathSucceeded, MessageResources.LOG4NET_CONFIGURATION_IS_CORRECT);
        }

        #endregion
    }
}