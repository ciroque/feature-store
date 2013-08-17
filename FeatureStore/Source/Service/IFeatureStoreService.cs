// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFeatureStoreService.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   WCF Service attributed and decorated derivation of the <see cref="IFeatureStore" /> interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.Service
{
    using System.ServiceModel;
    using Mutual;

    /// <summary>
    ///   WCF Service attributed and decorated derivation of the <see cref = "IFeatureStore" /> interface.
    /// </summary>
    [ServiceContract]
    public interface IFeatureStoreService : IFeatureStore
    {
    }
}