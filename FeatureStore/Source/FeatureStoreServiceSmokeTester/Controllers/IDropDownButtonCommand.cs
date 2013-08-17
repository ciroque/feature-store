namespace FeatureStoreServiceSmokeTester.Controllers
{
    /// <summary>
    /// </summary>
    public interface IDropDownButtonCommand
    {
        /// <summary>
        ///   Executes the specified service method completion sink.
        /// </summary>
        /// <param name = "serviceMethodUiBridge">The service method completion sink.</param>
        void Execute(IServiceMethodUiBridge serviceMethodUiBridge);
    }
}