namespace FeatureStoreServiceSmokeTester.Controllers
{
    using System.Windows.Forms;

    /// <summary>
    ///   Coordinates synchronizing the toolstrip buttons with the tabs.
    /// </summary>
    public class TabToolStripCoordinator
    {
        /// <summary>
        /// </summary>
        private TabControl m_TabControl;

        /// <summary>
        /// </summary>
        private ToolStrip m_Toolstrip;

        /// <summary>
        ///   Initializes a new instance of the <see cref = "TabToolStripCoordinator" /> class.
        /// </summary>
        private TabToolStripCoordinator()
        {
        }

        /// <summary>
        ///   Creates the specified tool strip.
        /// </summary>
        /// <param name = "tabControl">The tab control.</param>
        /// <param name = "toolStrip">The tool strip.</param>
        /// <returns></returns>
        public static TabToolStripCoordinator Create(TabControl tabControl, ToolStrip toolStrip)
        {
            TabToolStripCoordinator coordinator = new TabToolStripCoordinator();
            coordinator.m_Toolstrip = toolStrip;
            coordinator.m_TabControl = tabControl;
            coordinator.HookUpEvents();
            coordinator.DisableServiceToolStripButtons();
            return coordinator;
        }

        /// <summary>
        ///   Hooks up events.
        /// </summary>
        private void HookUpEvents()
        {
            m_TabControl.Selected += HandleTabControlSelected;
            m_Toolstrip.Items.ForEachDropDownButton(
                button => button.DropDownItemClicked += HandleDropDownButtonItemClicked);
        }

        /// <summary>
        ///   Handles the drop down button item clicked.
        /// </summary>
        /// <param name = "sender">The sender.</param>
        /// <param name = "e">The <see cref = "System.Windows.Forms.ToolStripItemClickedEventArgs" /> instance containing the event data.</param>
        private void HandleDropDownButtonItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            IDropDownButtonCommand command = (IDropDownButtonCommand) e.ClickedItem.Tag;
            var resultSink = m_TabControl.TabPages[e.ClickedItem.OwnerItem.Name] as IServiceMethodUiBridge;
            command.Execute(resultSink);
        }

        /// <summary>
        ///   Handles the tab control selected.
        /// </summary>
        /// <param name = "sender">The sender.</param>
        /// <param name = "e">The <see cref = "System.Windows.Forms.TabControlEventArgs" /> instance containing the event data.</param>
        private void HandleTabControlSelected(object sender, TabControlEventArgs e)
        {
            DisableServiceToolStripButtons();

            ToolStripItem item = m_Toolstrip.Items.FindByName(e.TabPage.Name);

            if (item != null)
            {
                item.Enabled = true;
            }
        }

        /// <summary>
        ///   Disables the service tool strip buttons.
        /// </summary>
        private void DisableServiceToolStripButtons()
        {
            m_Toolstrip.Items.ForEachDropDownButton(button => button.Enabled = false);
        }

        /// <summary>
        ///   Cleans up.
        /// </summary>
        public void CleanUp()
        {
            m_TabControl.Selected -= HandleTabControlSelected;
            m_Toolstrip.Items.ForEachDropDownButton(
                button => button.DropDownItemClicked -= HandleDropDownButtonItemClicked);
        }
    }
}