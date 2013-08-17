// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JsonToStringHelper.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Utility class to format an object to JSON
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.Mutual
{
    using System.IO;
    using System.Runtime.Serialization.Json;

    /// <summary>
    ///   Utility class to format an object to JSON
    /// </summary>
    public static class JsonToStringHelper
    {
        /// <summary>
        ///   Serializes the given object to a JSON-formatted string.
        /// </summary>
        /// <typeparam name = "T">The type to be serialized.</typeparam>
        /// <param name = "toJson">The object to be serialized.</param>
        /// <returns>A string containing the </returns>
        public static string ObjectToJson<T>(T toJson)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            MemoryStream stream = new MemoryStream();
            serializer.WriteObject(stream, toJson);
            stream.Position = 0;
            return new StreamReader(stream).ReadToEnd();
        }
    }
}