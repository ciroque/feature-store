// ****************************************************************************
// <copyright file="ProjectInstaller.Designer.cs" company="Microsoft Corporation">
//  Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// 
// *****************************************************************************
namespace Ciroque.Foundations.FeatureStore.FeatureStoreServiceHosting
{
    /// <summary>
    /// The Designer implementation for the ProjectInstaller class.
    /// </summary>
    public partial class ProjectInstaller
    {
        /// <summary>
        /// The service process installer
        /// </summary>
        private System.ServiceProcess.ServiceProcessInstaller m_ServiceProcessInstaller;

        /// <summary>
        /// The service installer
        /// </summary>
        private System.ServiceProcess.ServiceInstaller m_ServiceInstaller;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_ServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.m_ServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // m_ServiceProcessInstaller
            // 
            this.m_ServiceProcessInstaller.Password = null;
            this.m_ServiceProcessInstaller.Username = null;
            // 
            // m_ServiceInstaller
            // 
            this.m_ServiceInstaller.Description = "Manages feature sets in a central location for enterprise-wide use. ";
            this.m_ServiceInstaller.ServiceName = "Feature Store Service";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.m_ServiceProcessInstaller,
            this.m_ServiceInstaller});

        }

        #endregion
    }
}