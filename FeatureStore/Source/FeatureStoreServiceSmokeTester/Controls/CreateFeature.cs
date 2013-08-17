namespace FeatureStoreServiceSmokeTester.Controls
{
    using System.ComponentModel;
    using Resources;

    /// <summary>
    ///   SUMMARY
    /// </summary>
    [ToolboxItem(true)]
    public class CreateFeature : TabPageBase
    {
        public CreateFeature()
            : base(ControlTextsResource.TAB_PAGE_TEXT_CREATE_FEATURE, ArgumentFieldsLayout.CreateFeature)
        {
        }
    }
}