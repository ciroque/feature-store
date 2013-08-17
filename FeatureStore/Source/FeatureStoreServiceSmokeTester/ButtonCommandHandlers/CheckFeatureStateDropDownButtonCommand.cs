namespace FeatureStoreServiceSmokeTester.ButtonCommandHandlers
{
    using System.Globalization;
    using System.Text;
    using Ciroque.Foundations.FeatureStore.Mutual;
    using Controllers;
    using Resources;

    /// <summary>
    ///   Base class for methods used by both the synchronous and asynchronous CheckFeatureState commands
    /// </summary>
    public abstract class CheckFeatureStateDropDownButtonCommand : DropDownButtonCommandBase
    {
        /// <summary>
        ///   Builds the check feature state results rich text.
        /// </summary>
        /// <param name = "request">The request.</param>
        /// <param name = "response">The response.</param>
        /// <param name = "commandName">Name of the command.</param>
        /// <returns></returns>
        protected static string BuildResultsRichText(CheckFeatureStateRequest request,
                                                     CheckFeatureStateResponse response, string commandName)
        {
            StringBuilder builder = new StringBuilder(RtfResources.RTF_PREAMBLE);
            builder.AppendFormat(CultureInfo.CurrentUICulture, RtfResources.RTF_HEADER_FORMAT, commandName);
            builder.Append(RtfResources.REQUEST_SECTION);
            builder.Append(BuildMessageHeaderRichText(request.Header));
            builder.Append(BuildFeatureKeyRichText(request.Key));
            builder.Append(RtfResources.RESPONSE_SECTION);
            builder.Append(BuildMessageHeaderRichText(response.Header));
            builder.Append(BuildFeatureRichText(response.Result));
            builder.Append(RtfResources.RTF_CLOSE);
            return builder.ToString();
        }

        /// <summary>
        ///   Builds the feature key.
        /// </summary>
        /// <param name = "serviceMethodUiBridge">The service method UI bridge.</param>
        /// <returns></returns>
        protected FeatureKey BuildFeatureKey(IServiceMethodUiBridge serviceMethodUiBridge)
        {
            return FeatureKey.Create(
                serviceMethodUiBridge.FeatureStoreMethodArguments.Id,
                serviceMethodUiBridge.FeatureStoreMethodArguments.OwnerId,
                serviceMethodUiBridge.FeatureStoreMethodArguments.Space);
        }
    }
}