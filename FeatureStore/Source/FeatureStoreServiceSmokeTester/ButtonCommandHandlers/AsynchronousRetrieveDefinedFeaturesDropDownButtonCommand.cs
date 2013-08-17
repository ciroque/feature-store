namespace FeatureStoreServiceSmokeTester.ButtonCommandHandlers
{
    using System;
    using System.ComponentModel;
    using Ciroque.Foundations.FeatureStore.Mutual;
    using Ciroque.Foundations.FeatureStore.ServiceProxy;
    using Controllers;
    using Utility;

    internal class AsynchronousRetrieveDefinedFeaturesDropDownButtonCommand :
        RetrieveDefinedFeaturesDropDownButtonCommand, IDropDownButtonCommand
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
                FeatureScope featureScope = BuildFeatureScope(serviceMethodUiBridge);

                RetrieveDefinedFeaturesRequest request =
                    RetrieveDefinedFeaturesRequest.Create(MessageIdFactory.GenerateMessageId(), featureScope);

                IFeatureStoreServiceProxy featureStoreServiceProxy = new FeatureStoreServiceProxy();


                featureStoreServiceProxy.BeginRetrieveDefinedFeatures(
                    request,
                    ar =>
                        {
                            string rtfResults;
                            try
                            {
                                RetrieveDefinedFeaturesResponse response =
                                    featureStoreServiceProxy.EndRetrieveDefinedFeatures(ar);

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