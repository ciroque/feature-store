namespace FeatureStoreServiceSmokeTester.ButtonCommandHandlers
{
    using System;
    using System.ComponentModel;
    using Ciroque.Foundations.FeatureStore.Mutual;
    using Ciroque.Foundations.FeatureStore.ServiceProxy;
    using Controllers;
    using Utility;

    internal class AsynchronousCreateFeatureDropDownButtonCommand : CreateFeatureDropDownButtonCommand,
                                                                    IDropDownButtonCommand
    {
        private readonly object m_AsyncKey = new object();
        private IServiceMethodUiBridge m_ServiceMethodUiBridge;

        #region IDropDownButtonCommand Members

        /// <summary>
        ///   Executes the specified service method completion sink.
        /// </summary>
        /// <param name = "serviceMethodUiBridge">The service method completion sink.</param>
        public void Execute(IServiceMethodUiBridge serviceMethodUiBridge)
        {
            m_ServiceMethodUiBridge = serviceMethodUiBridge;
            AsyncOperation asyncOperation = AsyncOperationManager.CreateOperation(m_AsyncKey);

            try
            {
                Feature feature = BuildFeature(serviceMethodUiBridge);
                CreateFeatureRequest request = CreateFeatureRequest.Create(MessageIdFactory.GenerateMessageId(), feature);
                IFeatureStoreServiceProxy featureStoreServiceProxy = new FeatureStoreServiceProxy();
                featureStoreServiceProxy.BeginCreateFeature(
                    request,
                    ar =>
                        {
                            string rtfResults;
                            try
                            {
                                CreateFeatureResponse response = featureStoreServiceProxy.EndCreateFeature(ar);
                                rtfResults = BuildResultsRichText(request, response, GetType().Name);
                            }
                            catch (Exception e)
                            {
                                rtfResults = BuildExceptionRichText(e);
                            }

                            asyncOperation.PostOperationCompleted(HandleEndAsync, rtfResults);
                        },
                    null);
            }
            catch (Exception e)
            {
                serviceMethodUiBridge.DisplayResults(BuildExceptionRichText(e));
            }
        }

        #endregion

        /// <summary>
        ///   Handles the end async.
        /// </summary>
        /// <param name = "state">The state.</param>
        private void HandleEndAsync(object state)
        {
            m_ServiceMethodUiBridge.DisplayResults((string) state);
        }
    }
}