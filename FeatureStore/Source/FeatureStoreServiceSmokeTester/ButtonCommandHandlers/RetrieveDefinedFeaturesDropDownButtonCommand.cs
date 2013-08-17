namespace FeatureStoreServiceSmokeTester.ButtonCommandHandlers
{
    using System.Globalization;
    using System.Text;
    using Ciroque.Foundations.FeatureStore.Mutual;
    using Controllers;
    using Resources;

    /// <summary>
    ///   Base class for methods used by both the synchronous and asynchronous RetrieveDefinedFeatures commands
    /// </summary>
    public abstract class RetrieveDefinedFeaturesDropDownButtonCommand : DropDownButtonCommandBase
    {
        /// <summary>
        ///   Builds the results rich text.
        /// </summary>
        /// <param name = "request">The request.</param>
        /// <param name = "response">The response.</param>
        /// <param name = "commandName"></param>
        /// <returns></returns>
        protected string BuildResultsRichText(RetrieveDefinedFeaturesRequest request,
                                              RetrieveDefinedFeaturesResponse response, string commandName)
        {
            StringBuilder builder = new StringBuilder(RtfResources.RTF_PREAMBLE);
            builder.AppendFormat(CultureInfo.CurrentUICulture, RtfResources.RTF_HEADER_FORMAT, commandName);
            builder.Append(RtfResources.REQUEST_SECTION);
            builder.Append(BuildMessageHeaderRichText(request.Header));
            builder.Append(BuildFeatureScopeRichText(request.FeatureScope));
            builder.Append(RtfResources.RESPONSE_SECTION);
            builder.Append(BuildMessageHeaderRichText(response.Header));

            foreach (Feature feature in response.Result)
            {
                builder.Append(BuildFeatureRichText(feature));
            }

            builder.Append(RtfResources.RTF_CLOSE);
            return builder.ToString();
        }

        /// <summary>
        ///   Builds the feature scope.
        /// </summary>
        /// <param name = "serviceMethodUiBridge">The service method UI bridge.</param>
        /// <returns></returns>
        protected FeatureScope BuildFeatureScope(IServiceMethodUiBridge serviceMethodUiBridge)
        {
            return FeatureScope.Create(
                serviceMethodUiBridge.FeatureStoreMethodArguments.OwnerId,
                serviceMethodUiBridge.FeatureStoreMethodArguments.Space);
        }
    }
}