namespace FeatureStoreServiceSmokeTester.Controllers
{
    using System;

    /// <summary>
    ///   SUMMARY
    /// </summary>
    public interface IFeatureStoreMethodArguments
    {
        /// <summary>
        ///   Gets the id.
        /// </summary>
        /// <value>The id.</value>
        long Id { get; }

        /// <summary>
        ///   Gets the owner id.
        /// </summary>
        /// <value>The owner id.</value>
        Guid OwnerId { get; }

        /// <summary>
        ///   Gets the space.
        /// </summary>
        /// <value>The space.</value>
        Guid Space { get; }

        /// <summary>
        ///   Gets the name.
        /// </summary>
        /// <value>The name.</value>
        string FeatureName { get; }

        /// <summary>
        ///   Gets a value indicating whether this <see cref = "IFeatureStoreMethodArguments" /> is state.
        /// </summary>
        /// <value><c>true</c> if state; otherwise, <c>false</c>.</value>
        bool State { get; }
    }
}