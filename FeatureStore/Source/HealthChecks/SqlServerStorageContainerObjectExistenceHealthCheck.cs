// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlServerStorageContainerObjectExistenceHealthCheck.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Represents a IHealthCheck that ensures the necessary object exist in the SqlServerStorageContainer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.HealthChecks
{
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.Text;
    using log4net;
    using log4net.Config;

    /// <summary>
    ///   Represents a IHealthCheck that ensures the necessary object exist in the SqlServerStorageContainer.
    /// </summary>
    public class SqlServerStorageContainerObjectExistenceHealthCheck :
        SqlServerStorageContainerConnectivityHealthCheckBase
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref = "SqlServerStorageContainerObjectExistenceHealthCheck" /> class.
        /// </summary>
        /// <param name = "connection">The connection.</param>
        private SqlServerStorageContainerObjectExistenceHealthCheck(DbConnection connection)
        {
            XmlConfigurator.Configure();
            Log = LogManager.GetLogger(typeof(SqlServerStorageContainerObjectExistenceHealthCheck));
            Connection = connection;
        }

        /// <summary>
        ///   Creates the specified connection.
        /// </summary>
        /// <param name = "connection">The connection.</param>
        /// <returns>An initialized instance of the SqlServerStorageContainerObjectExistenceHealthCheck class as an <see cref = "IHealthCheck" /></returns>
        public static IHealthCheck Create(DbConnection connection)
        {
            return new SqlServerStorageContainerObjectExistenceHealthCheck(connection);
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
                DbCommand command = Connection.CreateCommand();
                command.CommandText = "IsHealthy_ObjectExistenceCheck";
                command.CommandType = CommandType.StoredProcedure;
                using (DbDataReader dataReader = command.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        bool success = true;
                        StringBuilder builder = new StringBuilder();
                        int fieldCount = dataReader.FieldCount;
                        for (int index = 0; index < fieldCount; index++)
                        {
                            bool objectFound = dataReader.GetBoolean(index);
                            if (!objectFound)
                            {
                                success = false;
                            }

                            string messageFormat = objectFound
                                                       ? MessageResources.OBJECT_FOUND_MESSAGE_FORMAT
                                                       : MessageResources.OBJECT_NOT_FOUND_MESSAGE_FORMAT;
                            builder.AppendFormat(CultureInfo.CurrentUICulture, messageFormat, dataReader.GetName(index));
                            builder.AppendLine();
                        }

                        result = HealthCheckResult.Create(success, builder.ToString());
                    }
                    else
                    {
                        result = HealthCheckResult.Create(false, MessageResources.EXISTENCE_SPROC_RETURNED_NO_ROWS);
                    }
                }
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