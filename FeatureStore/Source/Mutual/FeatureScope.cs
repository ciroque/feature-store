// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FeatureScope.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Represents the Space and Owner of a Feature. Used for searching the storage cache.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.Mutual
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    ///   Represents the Space and Owner of a Feature. Used for searching the storage cache.
    /// </summary>
    [Serializable]
    [DataContract]
    public class FeatureScope
    {
        /// <summary>
        ///   Prevents a default instance of the <see cref = "FeatureScope" /> class from being created.
        /// </summary>
        private FeatureScope()
        {
        }

        /// <summary>
        ///   Gets the owner id.
        /// </summary>
        /// <value>The owner id.</value>
        [DataMember]
        public Guid OwnerId { get; private set; }

        /// <summary>
        ///   Gets the space.
        /// </summary>
        /// <value>The space.</value>
        [DataMember]
        public Guid Space { get; private set; }

        /// <summary>
        ///   Creates the specified owner id.
        /// </summary>
        /// <param name = "ownerId">The owner id.</param>
        /// <param name = "space">The space.</param>
        /// <returns>An initialized instance of the <see cref = "FeatureScope" /> class.</returns>
        public static FeatureScope Create(Guid ownerId, Guid space)
        {
            return new FeatureScope
                       {
                           OwnerId = ownerId,
                           Space = space
                       };
        }
    }
}