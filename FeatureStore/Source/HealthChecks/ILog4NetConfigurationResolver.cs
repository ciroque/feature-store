// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILog4NetConfigurationResolver.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Interface that defines the necessary operations to resolve the location of the log4net configuration xml.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.HealthChecks
{
    using System.Xml.XPath;

    /// <summary>
    ///   Interface that defines the necessary operations to resolve the location of the log4net configuration xml.
    /// </summary>
    public interface ILog4NetConfigurationResolver
    {
        /// <summary>
        ///   Loads the log4net configuration.
        /// </summary>
        /// <returns>An initialized <see cref = "XPathDocument" /> that contains the contents of the log4net configuration.</returns>
        XPathDocument LoadConfiguration();
    }
}