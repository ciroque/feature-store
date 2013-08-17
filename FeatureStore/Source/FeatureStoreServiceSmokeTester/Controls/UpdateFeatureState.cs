namespace FeatureStoreServiceSmokeTester.Controls
{
    using System.ComponentModel;
    using Resources;

    /// <summary>
    ///   SUMMARY
    /// </summary>
    [ToolboxItem(true)]
    public class UpdateFeatureState : TabPageBase
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref = "UpdateFeatureState" /> class.
        /// </summary>
        public UpdateFeatureState()
            : base(ControlTextsResource.TAB_PAGE_TEXT_UPDATE_FEATURE_STATE, ArgumentFieldsLayout.UpdateFeature)
        {
        }
    }
}