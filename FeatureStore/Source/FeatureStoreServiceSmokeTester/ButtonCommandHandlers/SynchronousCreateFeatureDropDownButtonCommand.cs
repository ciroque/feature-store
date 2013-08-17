namespace FeatureStoreServiceSmokeTester.ButtonCommandHandlers
{
    using System;
    using Ciroque.Foundations.FeatureStore.Mutual;
    using Ciroque.Foundations.FeatureStore.ServiceProxy;
    using Controllers;
    using Utility;

    public class SynchronousCreateFeatureDropDownButtonCommand : CreateFeatureDropDownButtonCommand,
                                                                 IDropDownButtonCommand
    {
        #region IDropDownButtonCommand Members

        public void Execute(IServiceMethodUiBridge serviceMethodUiBridge)
        {
            Feature feature = BuildFeature(serviceMethodUiBridge);


            try
            {
                CreateFeatureRequest request = CreateFeatureRequest.Create(MessageIdFactory.GenerateMessageId(), feature);
                IFeatureStoreServiceProxy featureStoreServiceProxy = new FeatureStoreServiceProxy();
                CreateFeatureResponse response = featureStoreServiceProxy.CreateFeature(request);
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