// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlServerStorageContainerUserInRoleHealthCheck.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Represents a IHealthCheck that ensures the current credentials exist in the correct roles in the database.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.HealthChecks
{
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using log4net;
    using log4net.Config;

    /// <summary>
    ///   Represents a IHealthCheck that ensures the current credentials exist in the correct roles in the database.
    /// </summary>
    public class SqlServerStorageContainerUserInRoleHealthCheck : SqlServerStorageContainerConnectivityHealthCheckBase
    {
        /// <summary>
        ///   Initializes a new instance of the SqlServerStorageContainerUserInRoleHealthCheck class.
        /// </summary>
        /// <param name = "connection">The connection.</param>
        private SqlServerStorageContainerUserInRoleHealthCheck(DbConnection connection)
        {
            XmlConfigurator.Configure();
            Log = LogManager.GetLogger(typeof(SqlServerStorageContainerUserInRoleHealthCheck));
            Connection = connection;
        }

        /// <summary>
        ///   Creates the specified connection.
        /// </summary>
        /// <param name = "connection">The connection.</param>
        /// <returns>An initialized instance of the SqlServerStorageContainerUserInRoleHealthCheck as an <see cref = "IHealthCheck" /></returns>
        public static IHealthCheck Create(DbConnection connection)
        {
            return new SqlServerStorageContainerUserInRoleHealthCheck(connection);
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
                bool success;
                string message;

                DbCommand command = Connection.CreateCommand();
                command.CommandText = "SELECT IS_ROLEMEMBER('FeatureStore_Reader', @UserName)";
                command.CommandType = CommandType.Text;
                using (DbDataReader dataReader = command.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        success = dataReader.GetBoolean(0);
                        message = success
                                      ? MessageResources.USER_FOUND_IN_ROLE
                                      : MessageResources.USER_NOT_FOUND_IN_ROLE;
                    }
                    else
                    {
                        success = false;
                        message = MessageResources.IS_ROLEMEMBER_CHECK_RETURNED_NO_RECORDS;
                    }
                }

                result = HealthCheckResult.Create(success, message);
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