// ****************************************************************************
// <copyright file="FeatureStoreServiceHost.Designer.cs" company="Microsoft Corporation">
//  Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// 
// *****************************************************************************
namespace Ciroque.Foundations.FeatureStore.FeatureStoreServiceHosting
{
    /// <summary>
    /// Windows service implementation to host the FeatureStore WCF web service.
    /// </summary>
    public partial class FeatureStoreServiceHost
    {
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
                m_FeatureStoreServiceImpl.Dispose();
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
            components = new System.ComponentModel.Container();
            this.ServiceName = "FeatureStoreServiceHost";
        }

        #endregion
    }
}
