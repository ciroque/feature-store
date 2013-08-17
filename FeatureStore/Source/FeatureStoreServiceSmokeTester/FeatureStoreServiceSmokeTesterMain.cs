namespace FeatureStoreServiceSmokeTester
{
    using System;
    using System.Windows.Forms;
    using Controllers;

    /// <summary>
    /// </summary>
    public partial class FeatureStoreServiceSmokeTesterMain : Form
    {
        private TabToolStripCoordinator m_TabToolStripCoordinator;

        /// <summary>
        ///   Initializes a new instance of the <see cref = "FeatureStoreServiceSmokeTesterMain" /> class.
        /// </summary>
        public FeatureStoreServiceSmokeTesterMain()
        {
            InitializeComponent();
            InitializeInterface();
        }

        /// <summary>
        ///   Initializes the interface.
        /// </summary>
        private void InitializeInterface()
        {
            TabPagePopulator.PopulateTabPages(m_MasterTabs, m_MasterToolStrip);
            m_TabToolStripCoordinator = TabToolStripCoordinator.Create(m_MasterTabs, m_MasterToolStrip);
        }

        /// <summary>
        ///   Handles the exit TSB click.
        /// </summary>
        /// <param name = "sender">The sender.</param>
        /// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
        private void HandleExitTsbClick(object sender, EventArgs e)
        {
            m_TabToolStripCoordinator.CleanUp();
            Close();
        }
    }
}