// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FeatureStoreFault.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Fault thrown when an error occurs calling the CreateFeature method via WCF.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.Service
{
    /// <summary>
    ///   Fault thrown when an error occurs calling the CreateFeature method via WCF.
    /// </summary>
    public class FeatureStoreFault
    {
        /// <summary>
        ///   Prevents a default instance of the <see cref = "FeatureStoreFault" /> class from being created.
        /// </summary>
        private FeatureStoreFault()
        {
        }

        /// <summary>
        ///   Gets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; private set; }

        /// <summary>
        ///   Creates the specified message.
        /// </summary>
        /// <param name = "message">The message.</param>
        /// <returns>An initialized instance of the FeatureStoreFault class.</returns>
        public static FeatureStoreFault Create(string message)
        {
            return new FeatureStoreFault
                       {
                           Message = message
                       };
        }
    }
}