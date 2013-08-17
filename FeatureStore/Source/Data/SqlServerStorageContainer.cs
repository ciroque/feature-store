// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlServerStorageContainer.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Implementation of the IStorageContainer interface that uses Sql Server as the backing store.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.Data
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Globalization;
    using Mutual;

    /// <summary>
    ///   Implementation of the IStorageContainer interface that uses Sql Server as the backing store.
    /// </summary>
    public class SqlServerStorageContainer : IStorageContainer
    {
        /// <summary>
        ///   The key used to index the connection string information from the configuration file.
        /// </summary>
        private readonly string m_ConnectionStringKey;

        /// <summary>
        ///   Initializes a new instance of the <see cref = "SqlServerStorageContainer" /> class.
        /// </summary>
        /// <param name = "connectionStringKey">The connection string key.</param>
        public SqlServerStorageContainer(string connectionStringKey)
        {
            m_ConnectionStringKey = connectionStringKey;
        }

        #region IStorageContainer Members

        /// <summary>
        ///   Stores the specified entity.
        /// </summary>
        /// <param name = "feature">The entity to be stored.</param>
        /// <returns>The entity that was saved.</returns>
        public Feature Store(Feature feature)
        {
            using (SqlConnection connection = CreateSqlConnection())
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "[FeatureStore].[Store]";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", feature.Id);
                command.Parameters.AddWithValue("@OwnerUid", feature.OwnerId);
                command.Parameters.AddWithValue("@SpaceUid", feature.Space);
                command.Parameters.AddWithValue("@Name", feature.Name);
                command.Parameters.AddWithValue("@Enabled", feature.Enabled);

                connection.Open();

                command.ExecuteNonQuery();
            }

            return feature;
        }

        /// <summary>
        ///   Searches for a <see cref = "Feature" /> matching the criteria specified in the query.
        /// </summary>
        /// <param name = "key">The query.</param>
        /// <returns>
        ///   A <see cref = "Feature" /> instance matching the parameters specified by the <see cref = "FeatureKey" /> or null.
        /// </returns>
        public Feature Retrieve(FeatureKey key)
        {
            Feature feature = null;
            using (SqlConnection connection = CreateSqlConnection())
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "[FeatureStore].[RetrieveOne]";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", key.Id);
                command.Parameters.AddWithValue("@OwnerUid", key.OwnerId);
                command.Parameters.AddWithValue("@SpaceUid", key.Space);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        long id = long.Parse(reader[0].ToString(), CultureInfo.CurrentCulture);
                        Guid owner = new Guid(reader[1].ToString());
                        Guid space = new Guid(reader[2].ToString());
                        string name = reader.GetString(3);

                        feature = Feature.Create(
                            id,
                            owner,
                            space,
                            name);

                        feature.Enabled = reader.GetBoolean(4);
                    }
                }
            }

            return feature;
        }

        /// <summary>
        ///   Retrieves the specified feature space.
        /// </summary>
        /// <param name = "featureScope">The feature space.</param>
        /// <returns>
        ///   An IEnumerable containing the results of the query.
        /// </returns>
        public IList<Feature> Retrieve(FeatureScope featureScope)
        {
            List<Feature> features = new List<Feature>();
            using (SqlConnection connection = CreateSqlConnection())
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "[FeatureStore].[Retrieve]";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@OwnerUid",
                                                featureScope.OwnerId == Guid.Empty
                                                    ? (object) DBNull.Value
                                                    : featureScope.OwnerId);
                command.Parameters.AddWithValue("@SpaceUid",
                                                featureScope.Space == Guid.Empty
                                                    ? (object) DBNull.Value
                                                    : featureScope.Space);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        long id = long.Parse(reader[0].ToString(), CultureInfo.CurrentCulture);
                        Guid owner = new Guid(reader[1].ToString());
                        Guid space = new Guid(reader[2].ToString());
                        string name = reader.GetString(3);

                        Feature feature = Feature.Create(
                            id,
                            owner,
                            space,
                            name);

                        feature.Enabled = reader.GetBoolean(4);

                        features.Add(feature);
                    }
                }
            }

            return features;
        }

        #endregion

        /// <summary>
        ///   Creates the SQL connection.
        /// </summary>
        /// <returns>An initialized instance of the <see cref = "SqlConnection" /> class.</returns>
        private SqlConnection CreateSqlConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings[m_ConnectionStringKey].ConnectionString);
        }
    }
}