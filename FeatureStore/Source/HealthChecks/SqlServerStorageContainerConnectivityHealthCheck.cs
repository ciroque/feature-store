// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlServerStorageContainerConnectivityHealthCheck.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Represents a check against a SqlServerStorageContainer implementation that checks that the given database is available and a connection can be established.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.HealthChecks
{
    using System;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Globalization;
    using log4net;
    using log4net.Config;

    /// <summary>
    ///   Represents a check against a SqlServerStorageContainer implementation that checks that the given database is available and a connection can be established.
    /// </summary>
    public class SqlServerStorageContainerConnectivityHealthCheck : SqlServerStorageContainerConnectivityHealthCheckBase
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref = "SqlServerStorageContainerConnectivityHealthCheck" /> class.
        /// </summary>
        /// <param name = "connection">The connection.</param>
        private SqlServerStorageContainerConnectivityHealthCheck(DbConnection connection)
        {
            XmlConfigurator.Configure();
            Log = LogManager.GetLogger(typeof(SqlServerStorageContainerConnectivityHealthCheck));
            Connection = connection;
        }

        /// <summary>
        ///   Creates the specified connection.
        /// </summary>
        /// <param name = "connection">The connection.</param>
        /// <returns>An initialized instance of the SqlServerStorageContainerConnectivityHealhCheck class.</returns>
        public static IHealthCheck Create(DbConnection connection)
        {
            return new SqlServerStorageContainerConnectivityHealthCheck(connection);
        }

        /// <summary>
        ///   Executes the Health Check implementation.
        /// </summary>
        /// <returns>
        ///   An <see cref = "IHealthCheckResult" /> instance that specifies the outcome of the Health Check.
        /// </returns>
        public override IHealthCheckResult Execute()
        {
            IHealthCheckResult result;
            try
            {
                Connection.Open();
                string message = string.Format(
                    CultureInfo.CurrentUICulture,
                    MessageResources.SQL_CONNECTION_OPEN_SUCCEEDED,
                    Connection.ConnectionString);

                result = HealthCheckResult.Create(true, message);
                Log.Debug(message);
            }
            catch (InvalidOperationException e)
            {
                result = ProcessException(e, ExceptionMessageResources.SQL_CONNECTION_OPEN_FAILED);
            }
            catch (SqlException e)
            {
                result = ProcessException(e, ExceptionMessageResources.SQL_CONNECTION_OPEN_FAILED);
            }
            catch (ArgumentException e)
            {
                result = ProcessException(e, ExceptionMessageResources.SQL_CONNECTION_OPEN_FAILED);
            }

            return result;
        }
    }
}