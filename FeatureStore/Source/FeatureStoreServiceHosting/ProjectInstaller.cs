// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectInstaller.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Handles custom installers for the project
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.FeatureStoreServiceHosting
{
    using System.ComponentModel;
    using System.Configuration.Install;

    /// <summary>
    ///   Handles custom installers for the project
    /// </summary>
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref = "ProjectInstaller" /> class.
        /// </summary>
        public ProjectInstaller()
        {
            InitializeComponent();
        }
    }
}