// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStorageContainer.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Defines access points for a storage container implementation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.Data
{
    using System.Collections.Generic;
    using Mutual;

    /// <summary>
    ///   Defines access points for a storage container implementation.
    /// </summary>
    public interface IStorageContainer
    {
        /// <summary>
        ///   Stores the specified entity.
        /// </summary>
        /// <param name = "feature">The entity to be stored.</param>
        /// <returns>The entity that was saved.</returns>
        Feature Store(Feature feature);

        /// <summary>
        ///   Searches for a <see cref = "Feature" /> matching the criteria specified in the query.
        /// </summary>
        /// <param name = "key">The query.</param>
        /// <returns>A <see cref = "Feature" /> instance matching the parameters specified by the <see cref = "FeatureKey" /> or null.</returns>
        Feature Retrieve(FeatureKey key);

        /// <summary>
        ///   Retrieves the specified feature space.
        /// </summary>
        /// <param name = "featureScope">The feature space.</param>
        /// <returns>An IEnumerable containing the results of the query.</returns>
        IList<Feature> Retrieve(FeatureScope featureScope);
    }
}