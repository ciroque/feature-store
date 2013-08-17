namespace FeatureStoreServiceSmokeTester.Controllers
{
    using System.Windows.Forms;
    using Resources;

    public sealed class ServiceMethodUserInterfaceDefinition
    {
        public string Name { get; private set; }
        public TabPage TabPage { get; private set; }
        public ToolStripDropDownButton DropDownButton { get; private set; }

        /// <summary>
        ///   Creates the specified name.
        /// </summary>
        /// <param name = "name">The name.</param>
        /// <param name = "tabPage">The tab page.</param>
        /// <returns></returns>
        public static ServiceMethodUserInterfaceDefinition Create(string name, TabPage tabPage)
        {
            ServiceMethodUserInterfaceDefinition definition = new ServiceMethodUserInterfaceDefinition();
            definition.Name = name;
            tabPage.Name = name;
            definition.TabPage = tabPage;
            definition.DropDownButton = BuildDropDownButton(name);
            return definition;
        }

        /// <summary>
        ///   Builds the drop down button.
        /// </summary>
        /// <param name = "name">The name.</param>
        /// <returns></returns>
        private static ToolStripDropDownButton BuildDropDownButton(string name)
        {
            ToolStripDropDownButton button = new ToolStripDropDownButton(name);

            string asyncText = CommonResources.TEXT_ASYNCHRONOUS;
            ToolStripItem async = new ToolStripMenuItem(asyncText);
            async.Tag = DropDownButtonClickedCommandFactory.GetCommand(name, asyncText);
            button.DropDownItems.Add(async);

            string syncText = CommonResources.TEXT_SYNCHRONOUS;
            ToolStripItem sync = new ToolStripMenuItem(syncText);
            sync.Tag = DropDownButtonClickedCommandFactory.GetCommand(name, syncText);
            button.DropDownItems.Add(sync);

            button.Name = name;
            return button;
        }
    }
}