namespace FeatureStoreServiceSmokeTester.ButtonCommandHandlers
{
    using System;
    using System.ComponentModel;
    using Ciroque.Foundations.FeatureStore.Mutual;
    using Ciroque.Foundations.FeatureStore.ServiceProxy;
    using Controllers;
    using Utility;

    internal class AsynchronousCheckFeatureStateDropDownButtonCommand : CheckFeatureStateDropDownButtonCommand,
                                                                        IDropDownButtonCommand
    {
        private readonly object m_AsyncKey = new object();
        private IServiceMethodUiBridge m_ServiceMethodUiBridge;

        #region IDropDownButtonCommand Members

        public void Execute(IServiceMethodUiBridge serviceMethodUiBridge)
        {
            m_ServiceMethodUiBridge = serviceMethodUiBridge;
            AsyncOperation asyncOperation = AsyncOperationManager.CreateOperation(m_AsyncKey);

            FeatureKey featureKey = BuildFeatureKey(serviceMethodUiBridge);

            try
            {
                CheckFeatureStateRequest request = CheckFeatureStateRequest.Create(
                    MessageIdFactory.GenerateMessageId(), featureKey);
                IFeatureStoreServiceProxy featureStoreServiceProxy = new FeatureStoreServiceProxy();

                featureStoreServiceProxy.BeginCheckFeatureState(
                    request,
                    ar =>
                        {
                            string rtfResults;
                            try
                            {
                                CheckFeatureStateResponse response = featureStoreServiceProxy.EndCheckFeatureState(ar);
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

        private void HandleEndAsync(object state)
        {
            m_ServiceMethodUiBridge.DisplayResults((string) state);
        }
    }
}