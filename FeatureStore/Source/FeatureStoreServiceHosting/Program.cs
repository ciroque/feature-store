// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Bootstrapper for the services.b
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.FeatureStoreServiceHosting
{
    using System.ServiceProcess;

    /// <summary>
    ///   Bootstrapper for the services.b
    /// </summary>
    public static class Program
    {
        /// <summary>
        ///   The main entry point for the application.
        /// </summary>
        public static void Main()
        {
            ServiceBase[] servicesToRun = new ServiceBase[]
                {
                    new FeatureStoreServiceHost()
                };

            ServiceBase.Run(servicesToRun);
        }
    }
}