namespace FeatureStoreServiceSmokeTester.ButtonCommandHandlers
{
    using System;
    using Ciroque.Foundations.FeatureStore.Mutual;
    using Ciroque.Foundations.FeatureStore.ServiceProxy;
    using Controllers;
    using Utility;

    public class SynchronousUpdateFeatureStateDropDownButtonCommand : UpdateFeatureStateDropDownButtonCommand,
                                                                      IDropDownButtonCommand
    {
        #region IDropDownButtonCommand Members

        public void Execute(IServiceMethodUiBridge serviceMethodUiBridge)
        {
            FeatureKey featureKey = BuildFeatureKey(serviceMethodUiBridge);

            try
            {
                UpdateFeatureStateRequest request = UpdateFeatureStateRequest.Create(
                    MessageIdFactory.GenerateMessageId(),
                    featureKey,
                    serviceMethodUiBridge.FeatureStoreMethodArguments.State);

                IFeatureStoreServiceProxy featureStoreServiceProxy = new FeatureStoreServiceProxy();

                UpdateFeatureStateResponse response = featureStoreServiceProxy.UpdateFeatureState(request);

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