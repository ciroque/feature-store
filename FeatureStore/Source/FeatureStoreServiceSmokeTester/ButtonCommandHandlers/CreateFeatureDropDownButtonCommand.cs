namespace FeatureStoreServiceSmokeTester.ButtonCommandHandlers
{
    using System.Globalization;
    using System.Text;
    using Ciroque.Foundations.FeatureStore.Mutual;
    using Controllers;
    using Resources;

    /// <summary>
    ///   Base class for methods used by both the synchronous and asynchronous CreateFeature commands
    /// </summary>
    public abstract class CreateFeatureDropDownButtonCommand : DropDownButtonCommandBase
    {
        /// <summary>
        ///   Builds the feature.
        /// </summary>
        /// <param name = "serviceMethodUiBridge">The service method UI bridge.</param>
        /// <returns></returns>
        protected Feature BuildFeature(IServiceMethodUiBridge serviceMethodUiBridge)
        {
            Feature feature = Feature.Create(
                serviceMethodUiBridge.FeatureStoreMethodArguments.Id,
                serviceMethodUiBridge.FeatureStoreMethodArguments.OwnerId,
                serviceMethodUiBridge.FeatureStoreMethodArguments.Space,
                serviceMethodUiBridge.FeatureStoreMethodArguments.FeatureName);

            feature.Enabled = serviceMethodUiBridge.FeatureStoreMethodArguments.State;

            return feature;
        }

        /// <summary>
        ///   Builds the results rich text.
        /// </summary>
        /// <param name = "request">The request.</param>
        /// <param name = "response">The response.</param>
        /// <param name = "commandName">Name of the command.</param>
        /// <returns></returns>
        protected string BuildResultsRichText(CreateFeatureRequest request, CreateFeatureResponse response,
                                              string commandName)
        {
            StringBuilder builder = new StringBuilder(RtfResources.RTF_PREAMBLE);
            builder.AppendFormat(CultureInfo.CurrentUICulture, RtfResources.RTF_HEADER_FORMAT, commandName);
            builder.Append(RtfResources.REQUEST_SECTION);
            builder.Append(BuildMessageHeaderRichText(request.Header));
            builder.Append(BuildFeatureRichText(request.Feature));
            builder.Append(RtfResources.RESPONSE_SECTION);
            builder.Append(BuildMessageHeaderRichText(response.Header));
            builder.Append(BuildFeatureRichText(response.Result));
            builder.Append(RtfResources.RTF_CLOSE);
            return builder.ToString();
        }
    }
}