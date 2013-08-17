namespace FeatureStoreServiceSmokeTester.ButtonCommandHandlers
{
    using System;
    using Ciroque.Foundations.FeatureStore.Mutual;
    using Ciroque.Foundations.FeatureStore.ServiceProxy;
    using Controllers;
    using Utility;

    public class SynchronousRetrieveDefinedFeaturesDropDownButtonCommand : RetrieveDefinedFeaturesDropDownButtonCommand,
                                                                           IDropDownButtonCommand
    {
        #region IDropDownButtonCommand Members

        public void Execute(IServiceMethodUiBridge serviceMethodUiBridge)
        {
            FeatureScope featureScope = BuildFeatureScope(serviceMethodUiBridge);

            try
            {
                RetrieveDefinedFeaturesRequest request =
                    RetrieveDefinedFeaturesRequest.Create(MessageIdFactory.GenerateMessageId(), featureScope);
                IFeatureStoreServiceProxy featureStoreServiceProxy = new FeatureStoreServiceProxy();
                RetrieveDefinedFeaturesResponse response = featureStoreServiceProxy.RetrieveDefinedFeatures(request);
                serviceMethodUiBridge.DisplayResults(BuildResultsRichText(request, response, GetType().Name));
            }
            catch (Exception e)
            {
                serviceMethodUiBridge.DisplayResults(BuildExceptionRichText(e));
            }
        }

        #endregion
    }
}