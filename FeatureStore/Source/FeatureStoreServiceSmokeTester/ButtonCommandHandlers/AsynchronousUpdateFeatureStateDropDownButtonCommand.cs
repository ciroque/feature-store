namespace FeatureStoreServiceSmokeTester.ButtonCommandHandlers
{
    using System;
    using System.ComponentModel;
    using Ciroque.Foundations.FeatureStore.Mutual;
    using Ciroque.Foundations.FeatureStore.ServiceProxy;
    using Controllers;
    using Utility;

    internal class AsynchronousUpdateFeatureStateDropDownButtonCommand : UpdateFeatureStateDropDownButtonCommand,
                                                                         IDropDownButtonCommand
    {
        private readonly object m_AsyncKey = new object();
        private IServiceMethodUiBridge m_ServiceMethodUiBridge;

        #region IDropDownButtonCommand Members

        public void Execute(IServiceMethodUiBridge serviceMethodUiBridge)
        {
            m_ServiceMethodUiBridge = serviceMethodUiBridge;
            AsyncOperation asyncOperation = AsyncOperationManager.CreateOperation(m_AsyncKey);

            try
            {
                FeatureKey featureKey = BuildFeatureKey(serviceMethodUiBridge);

                UpdateFeatureStateRequest request = UpdateFeatureStateRequest.Create(
                    MessageIdFactory.GenerateMessageId(),
                    featureKey,
                    serviceMethodUiBridge.FeatureStoreMethodArguments.State);

                IFeatureStoreServiceProxy featureStoreServiceProxy = new FeatureStoreServiceProxy();

                featureStoreServiceProxy.BeginUpdateFeatureState(
                    request,
                    ar =>
                        {
                            string rtfResults;
                            try
                            {
                                UpdateFeatureStateResponse response =
                                    featureStoreServiceProxy.EndUpdateFeatureState(ar);

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