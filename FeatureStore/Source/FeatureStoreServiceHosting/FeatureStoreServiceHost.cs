// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FeatureStoreServiceHost.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Windows service implementation to host the FeatureStore WCF web service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.FeatureStoreServiceHosting
{
    using System;
    using System.ServiceProcess;

    using Ciroque.Foundations.FeatureStore.Service;

    using log4net;
    using log4net.Config;

    /// <summary>
    ///   Windows service implementation to host the FeatureStore WCF web service.
    /// </summary>
    public partial class FeatureStoreServiceHost : ServiceBase
    {
        /// <summary>
        ///   The actual implementation of the service.
        /// </summary>
        private readonly WcfService<FeatureStoreService> m_FeatureStoreServiceImpl;

        /// <summary>
        ///   Access to the logging implementation.
        /// </summary>
        private readonly ILog m_Logger;

        /// <summary>
        ///   Initializes a new instance of the <see cref = "FeatureStoreServiceHost" /> class.
        /// </summary>
        public FeatureStoreServiceHost()
        {
            InitializeComponent();

            XmlConfigurator.Configure();
            m_Logger = LogManager.GetLogger(typeof(FeatureStoreServiceHost));

            m_FeatureStoreServiceImpl = new WcfService<FeatureStoreService>();
        }

        /// <summary>
        ///   When implemented in a derived class, executes when a Start command is sent to the service by the Service Control Manager (SCM) or when the operating system starts (for a service that starts automatically). Specifies actions to take when the service starts.
        /// </summary>
        /// <param name = "args">Data passed by the start command.</param>
        protected override void OnStart(string[] args)
        {
            try
            {
                m_FeatureStoreServiceImpl.Start();
            }
            catch (Exception e)
            {
                m_Logger.Error(e);
                throw;
            }
        }

        /// <summary>
        ///   When implemented in a derived class, executes when a Stop command is sent to the service by the Service Control Manager (SCM). Specifies actions to take when a service stops running.
        /// </summary>
        protected override void OnStop()
        {
            try
            {
                m_FeatureStoreServiceImpl.Stop();
            }
            catch (Exception e)
            {
                m_Logger.Error(e);
                throw;
            }
        }
    }
}