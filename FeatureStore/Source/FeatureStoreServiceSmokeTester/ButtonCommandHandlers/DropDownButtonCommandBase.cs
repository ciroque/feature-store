namespace FeatureStoreServiceSmokeTester.ButtonCommandHandlers
{
    using System;
    using System.Globalization;
    using System.Text;
    using Ciroque.Foundations.FeatureStore.Mutual;
    using Resources;

    /// <summary>
    ///   SUMMARY
    /// </summary>
    public abstract class DropDownButtonCommandBase
    {
        /// <summary>
        ///   Builds the message header rich text.
        /// </summary>
        /// <param name = "messageHeader">The message header.</param>
        /// <returns></returns>
        protected static string BuildMessageHeaderRichText(MessageHeader messageHeader)
        {
            return string.Format(
                CultureInfo.CurrentUICulture,
                RtfResources.MESSAGE_HEADER_FORMAT,
                messageHeader.MessageId,
                messageHeader.OriginationIpAddress,
                messageHeader.OriginationHostName);
        }

        /// <summary>
        ///   Builds the feature key rich text.
        /// </summary>
        /// <param name = "featureKey">The feature key.</param>
        /// <returns></returns>
        protected static string BuildFeatureKeyRichText(FeatureKey featureKey)
        {
            return string.Format(
                CultureInfo.CurrentUICulture,
                RtfResources.FEATURE_KEY_FORMAT,
                featureKey.Id,
                featureKey.OwnerId,
                featureKey.Space);
        }

        /// <summary>
        ///   Builds the feature rich text.
        /// </summary>
        /// <param name = "feature">The feature.</param>
        /// <returns></returns>
        protected static string BuildFeatureRichText(Feature feature)
        {
            if (feature == null)
            {
                return RtfResources.NO_RESULTS;
            }

            return string.Format(
                CultureInfo.CurrentUICulture,
                RtfResources.FEATURE_FORMAT,
                feature.Id,
                feature.OwnerId,
                feature.Space,
                feature.Name,
                feature.Enabled);
        }

        /// <summary>
        ///   Builds the exception rich text.
        /// </summary>
        /// <param name = "exception">The exception.</param>
        /// <returns></returns>
        protected static string BuildExceptionRichText(Exception exception)
        {
            StringBuilder builder = new StringBuilder(RtfResources.RTF_PREAMBLE);
            FormatException(builder, exception);
            return builder.ToString();
        }

        /// <summary>
        ///   Formats the exception.
        /// </summary>
        /// <param name = "builder">The builder.</param>
        /// <param name = "exception">The exception.</param>
        protected static void FormatException(StringBuilder builder, Exception exception)
        {
            builder.AppendFormat(
                CultureInfo.CurrentUICulture,
                RtfResources.EXCEPTION_MESSAGE_FORMAT,
                exception.Message);

            string stackTrace;
            if (exception.StackTrace == null)
            {
                stackTrace = CommonResources.NO_STACK_TRACE_AVAILABLE;
            }
            else
            {
                stackTrace = exception.StackTrace;
            }

            builder.AppendFormat(
                CultureInfo.CurrentUICulture,
                RtfResources.STACK_TRACE_FORMAT,
                stackTrace);
            if (exception.InnerException != null)
            {
                builder.Append(RtfResources.INNER_EXCEPTION_DELIMETER);
                FormatException(builder, exception.InnerException);
            }
        }

        /// <summary>
        ///   Builds the feature scope rich text.
        /// </summary>
        /// <param name = "featureScope">The feature scope.</param>
        /// <returns></returns>
        protected string BuildFeatureScopeRichText(FeatureScope featureScope)
        {
            return string.Format(
                CultureInfo.CurrentUICulture,
                RtfResources.FEATURE_SCOPE_FORMAT,
                featureScope.OwnerId,
                featureScope.Space);
        }
    }
}