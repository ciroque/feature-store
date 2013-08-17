namespace FeatureStoreServiceSmokeTester.Controls
{
    using System.ComponentModel;
    using Resources;

    /// <summary>
    ///   Wraps the CheckFeatureState service call in a strongly-typed TabPage.
    /// </summary>
    [ToolboxItem(true)]
    public class CheckFeatureState : TabPageBase
    {
        public CheckFeatureState()
            : base(ControlTextsResource.TAB_PAGE_TEXT_CHECK_FEATURE_STATE, ArgumentFieldsLayout.CheckFeatureState)
        {
        }
    }
}