// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WcfService.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Wraps initialization and Start / Stop commands for a WCF Service implementation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.Service
{
    using System;
    using System.ServiceModel;
    using log4net;
    using log4net.Config;

    /// <summary>
    ///   Wraps initialization and Start / Stop commands for a WCF Service implementation.
    /// </summary>
    /// <typeparam name = "T">A type that implements a Service Contract</typeparam>
    public class WcfService<T> : IDisposable
    {
        /// <summary>
        ///   The Service Host.
        /// </summary>
        private readonly ServiceHost m_Host = new ServiceHost(typeof(T));

        /// <summary>
        ///   Initializes a new instance of the <see cref = "WcfService&lt;T&gt;" /> class.
        /// </summary>
        public WcfService()
        {
            XmlConfigurator.Configure();
            Log = LogManager.GetLogger(typeof(WcfService<T>));
            ServiceTypeName = typeof(T).Name;
        }

        /// <summary>
        ///   Gets or sets the log.
        /// </summary>
        /// <value>The ILogsed to write to the log.</value>
        private ILog Log { get; set; }

        /// <summary>
        ///   Gets or sets the name of the service type.
        /// </summary>
        /// <value>The name of the service type.</value>
        private string ServiceTypeName { get; set; }

        #region IDisposable Members

        /// <summary>
        ///   Releases unmanaged and - optionally - managed resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        /// <summary>
        ///   Starts this instance.
        /// </summary>
        public void Start()
        {
            Log.DebugFormat(LoggingResources.STARTING_SERVICE, ServiceTypeName);
            m_Host.Open();
        }

        /// <summary>
        ///   Stops this instance.
        /// </summary>
        public void Stop()
        {
            if (m_Host.State == CommunicationState.Opened)
            {
                Log.DebugFormat(LoggingResources.STOPPING_SERVICE, ServiceTypeName);
                m_Host.Close();
            }
        }

        /// <summary>
        ///   Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name = "disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Stop();
            }
        }
    }
}