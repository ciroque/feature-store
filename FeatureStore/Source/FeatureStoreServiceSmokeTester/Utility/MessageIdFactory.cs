namespace FeatureStoreServiceSmokeTester.Utility
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Threading;
    using Ciroque.Foundations.FeatureStore.Mutual;
    using Controllers;

    /// <summary>
    ///   Helper class to generate a unique message id to be used to populate the <see cref = "MessageHeader" />s MessageId property for tracing.
    /// </summary>
    public static class MessageIdFactory
    {
        /// <summary>
        ///   Generates the message id.
        /// </summary>
        /// <returns>A string to be used as the message id for the call. The string consists of the name of the calling <see
        ///    cref = "IDropDownButtonCommand" /> implementation, the <see cref = "DateTime" /> the message id was created, the current machines name and the managed thread id of the thread on which the message id was generated.</returns>
        public static string GenerateMessageId()
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame[] frames = stackTrace.GetFrames();

            string commandHandlerName;

            if (frames != null && frames.Length > 0)
            {
                commandHandlerName = frames[1].GetMethod().DeclaringType.Name;
            }
            else
            {
                commandHandlerName = Guid.NewGuid().ToString();
            }

            return string.Format(
                CultureInfo.InvariantCulture,
                "{0}_{1}_{2}_{3}",
                commandHandlerName,
                DateTime.Now.ToString("yyyyMMddThhmmss"),
                Environment.MachineName,
                Thread.CurrentThread.ManagedThreadId);
        }
    }
}