namespace FeatureStoreServiceSmokeTester.ButtonCommandHandlers
{
    using System;
    using Ciroque.Foundations.FeatureStore.Mutual;
    using Ciroque.Foundations.FeatureStore.ServiceProxy;
    using Controllers;
    using Utility;

    public class SynchronousCheckFeatureStateDropDownButtonCommand : CheckFeatureStateDropDownButtonCommand,
                                                                     IDropDownButtonCommand
    {
        #region IDropDownButtonCommand Members

        public void Execute(IServiceMethodUiBridge serviceMethodUiBridge)
        {
            FeatureKey featureKey = BuildFeatureKey(serviceMethodUiBridge);

            try
            {
                CheckFeatureStateRequest request = CheckFeatureStateRequest.Create(
                    MessageIdFactory.GenerateMessageId(), featureKey);
                IFeatureStoreServiceProxy featureStoreServiceProxy = new FeatureStoreServiceProxy();
                CheckFeatureStateResponse response = featureStoreServiceProxy.CheckFeatureState(request);

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