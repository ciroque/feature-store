// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Feature.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Represents a defined feature in the system.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.Mutual
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    ///   Represents a defined feature in the system.
    /// </summary>
    [Serializable]
    [DataContract]
    public class Feature
    {
        /// <summary>
        ///   Prevents a default instance of the Feature class from being created.
        /// </summary>
        private Feature()
        {
            Id = -1;
        }

        /// <summary>
        ///   Gets the name.
        /// </summary>
        /// <value>The display name of the Feature.</value>
        [DataMember]
        public string Name { get; private set; }

        /// <summary>
        ///   Gets the id.
        /// </summary>
        /// <value>The unique identifier for the Feature.</value>
        [DataMember]
        public long Id { get; private set; }

        /// <summary>
        ///   Gets distinct space of the Feature.
        /// </summary>
        /// <value>The Guid representing the unique space of the Feature.</value>
        [DataMember]
        public Guid Space { get; private set; }

        /// <summary>
        ///   Gets or sets the owner id.
        /// </summary>
        /// <value>The owner id.</value>
        [DataMember]
        public Guid OwnerId { get; set; }

        /// <summary>
        ///   Gets or sets a value indicating whether this <see cref = "Feature" /> is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        [DataMember]
        public bool Enabled { get; set; }

        /// <summary>
        ///   Creates a Feature instance with the specified id, space and name.
        /// </summary>
        /// <param name = "id">The id of the Feature.</param>
        /// <param name = "ownerId">The distinct id of the owner of this Feature.</param>
        /// <param name = "space">The distinct space of the Feature.</param>
        /// <param name = "name">The display name of the Feature.</param>
        /// <returns>A new instance of the Feature class.</returns>
        public static Feature Create(long id, Guid ownerId, Guid space, string name)
        {
            return new Feature
                       {
                           Id = id,
                           Name = name,
                           OwnerId = ownerId,
                           Space = space
                       };
        }

        /// <summary>
        ///   Equalses the specified other.
        /// </summary>
        /// <param name = "other">The other.</param>
        /// <returns><c>true</c> if the specified Feature is equal to this Feature instance, <c>false</c> otherwise.</returns>
        public bool Equals(Feature other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Equals(other.Name, Name) && other.Id == Id && other.Space.Equals(Space) &&
                   other.OwnerId.Equals(OwnerId) && other.Enabled.Equals(Enabled);
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
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != typeof(Feature))
            {
                return false;
            }

            return Equals((Feature)obj);
        }

        /// <summary>
        ///   Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        ///   A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int result = Name != null ? Name.GetHashCode() : 0;
                result = (result * 397) ^ Id.GetHashCode();
                result = (result * 397) ^ Space.GetHashCode();
                result = (result * 397) ^ OwnerId.GetHashCode();
                result = (result * 397) ^ Enabled.GetHashCode();
                return result;
            }
        }
    }
}