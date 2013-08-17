namespace FeatureStoreServiceSmokeTester.Controls
{
    using System.ComponentModel;
    using Resources;

    /// <summary>
    ///   SUMMARY
    /// </summary>
    [ToolboxItem(true)]
    public class RetrieveDefinedFeatures : TabPageBase
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref = "RetrieveDefinedFeatures" /> class.
        /// </summary>
        public RetrieveDefinedFeatures()
            : base(
                ControlTextsResource.TAB_PAGE_TEXT_RETRIEVE_DEFINED_FEATURES,
                ArgumentFieldsLayout.RetrieveDefinedFeatures)
        {
        }
    }
}