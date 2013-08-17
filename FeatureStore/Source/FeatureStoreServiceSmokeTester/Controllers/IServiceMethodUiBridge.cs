namespace FeatureStoreServiceSmokeTester.Controllers
{
    /// <summary>
    ///   Responsible for allowing an <see cref = "IDropDownButtonCommand" /> instance to report the results.
    /// </summary>
    public interface IServiceMethodUiBridge
    {
        /// <summary>
        ///   Gets the feature store method arguments.
        /// </summary>
        /// <value>The feature store method arguments.</value>
        IFeatureStoreMethodArguments FeatureStoreMethodArguments { get; }

        /// <summary>
        ///   Displays the results.
        /// </summary>
        /// <param name = "results">The results.</param>
        void DisplayResults(string results);
    }
}