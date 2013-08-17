namespace FeatureStoreServiceSmokeTester.TabPages
{
    using System.Windows.Forms;

    public sealed class ServiceMethodUserInterfaceDefinition
    {
        public string Name { get; private set; }
        public TabPage TabPage { get; private set; }
        public ToolStripDropDownButton DropDownButton { get; private set; }

        public static ServiceMethodUserInterfaceDefinition Create(string name, TabPage tabPage)
        {
            ServiceMethodUserInterfaceDefinition definition = new ServiceMethodUserInterfaceDefinition();
            definition.Name = name;
            tabPage.Tag = name;
            definition.TabPage = tabPage;
            definition.DropDownButton = BuildDropDownButton(name);
            return definition;
        }

        private static ToolStripDropDownButton BuildDropDownButton(string name)
        {
            ToolStripDropDownButton button = new ToolStripDropDownButton(name);
            button.DropDownItems.Add("Asynchronous");
            button.DropDownItems.Add("Synchronous");
            button.Tag = name;
            return button;
        }
    }
}