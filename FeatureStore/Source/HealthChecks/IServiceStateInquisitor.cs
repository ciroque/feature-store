// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IServiceStateInquisitor.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Defines the interface used to determine the current state of the Feature State service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.HealthChecks
{
    /// <summary>
    ///   Defines the interface used to determine the current state of the Feature State service.
    /// </summary>
    public interface IServiceStateInquisitor
    {
        /// <summary>
        ///   Services the is running.
        /// </summary>
        /// <returns><code>true</code> if the service is running, <code>false</code> otherwise.</returns>
        /// <exception cref = "ServiceNotInstalledException"> is thrown when the service cannot be found in the service control manager database and a subsequent check of the WMI Win32_Product database fails to locate the service.</exception>
        bool ServiceIsRunning();
    }
}