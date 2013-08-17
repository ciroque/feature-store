// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FeatureKey.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Represents the key into the Dictionary that holds the Features.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.Mutual
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    ///   Represents the key into the Dictionary that holds the Features.
    /// </summary>
    [Serializable]
    [DataContract]
    public class FeatureKey // : FeatureScope
    {
        /// <summary>
        ///   Gets the Id of the Feature.
        /// </summary>
        [DataMember]
        public long Id { get; private set; }

        /// <summary>
        ///   Gets or sets the owner id.
        /// </summary>
        /// <value>The owner id.</value>
        [DataMember]
        public Guid OwnerId { get; set; }

        /// <summary>
        ///   Gets the Guid that represents the Space in which the Feature is defined.
        /// </summary>
        [DataMember]
        public Guid Space { get; private set; }

        /// <summary>
        ///   Creates the specified id.
        /// </summary>
        /// <param name = "id">The Features unique id.</param>
        /// <param name = "ownerId">The distinct identifier of the owner.</param>
        /// <param name = "space">The Features unique space.</param>
        /// <returns>An initialized FeatureKey instance.</returns>
        public static FeatureKey Create(long id, Guid ownerId, Guid space)
        {
            return new FeatureKey
                       {
                           Id = id,
                           OwnerId = ownerId,
                           Space = space
                       };
        }

        /// <summary>
        ///   Implements the operator ==.
        /// </summary>
        /// <param name = "left">The left FeatureKey to be compared.</param>
        /// <param name = "right">The right FeatureKey to be compared.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(FeatureKey left, FeatureKey right)
        {
            return right != null && (left != null && (left.Id == right.Id && left.Space == right.Space && left.OwnerId == right.OwnerId));
        }

        /// <summary>
        ///   Implements the operator ==.
        /// </summary>
        /// <param name = "left">The left FeatureKey to be compared.</param>
        /// <param name = "right">The right FeatureKey to be compared.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(FeatureKey left, FeatureKey right)
        {
            return !(left == right);
        }

        /// <summary>
        ///   Determines whether the specified <see cref = "System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name = "obj">The <see cref = "System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref = "System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            FeatureKey key = obj as FeatureKey;
            return !ReferenceEquals(key, null) ? this == key : false;
        }

        /// <summary>
        ///   Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        ///   A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ Space.GetHashCode();
        }
    }
}