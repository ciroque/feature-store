namespace FeatureStoreServiceSmokeTester.Controllers
{
    using System.Windows.Forms;
    using Controls;
    using Resources;

    /// <summary>
    ///   SUMMARY
    /// </summary>
    public static class TabPagePopulator
    {
        /// <summary>
        ///   Populates the tab pages.
        /// </summary>
        /// <param name = "tabControl">The tab control.</param>
        /// <param name = "toolStrip">The tool strip.</param>
        public static void PopulateTabPages(TabControl tabControl, ToolStrip toolStrip)
        {
            var defs = new[]
                           {
                               ServiceMethodUserInterfaceDefinition.Create(
                                   ControlTextsResource.TAB_PAGE_TEXT_CHECK_FEATURE_STATE, new CheckFeatureState()),
                               ServiceMethodUserInterfaceDefinition.Create(
                                   ControlTextsResource.TAB_PAGE_TEXT_CREATE_FEATURE, new CreateFeature()),
                               ServiceMethodUserInterfaceDefinition.Create(
                                   ControlTextsResource.TAB_PAGE_TEXT_RETRIEVE_DEFINED_FEATURES,
                                   new RetrieveDefinedFeatures()),
                               ServiceMethodUserInterfaceDefinition.Create(
                                   ControlTextsResource.TAB_PAGE_TEXT_UPDATE_FEATURE_STATE, new UpdateFeatureState())
                           };

            tabControl.TabPages.Add(new HelpTabPage());

            foreach (ServiceMethodUserInterfaceDefinition definition in defs)
            {
                tabControl.TabPages.Add(definition.TabPage);
                toolStrip.Items.Add(definition.DropDownButton);
            }
        }
    }
}