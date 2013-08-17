namespace Ciroque.Foundations.HealthCheck.Tests
{
    using System;
    using System.Data;
    using System.Data.Common;
    using FeatureStore.HealthChecks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Rhino.Mocks;

    [TestClass]
    public class SqlServerStorageContainerObjectExistenceTests
    {
        private readonly MockRepository m_MockRepository = new MockRepository();

        [TestMethod]
        public void AllObjectsExist()
        {
            var results = new[]
                              {
                                  new Tuple<bool, string>(true, "Role1Exists"),
                                  new Tuple<bool, string>(true, "Role2Exists"),
                                  new Tuple<bool, string>(true, "Table1Exists"),
                                  new Tuple<bool, string>(true, "Table2Exists"),
                                  new Tuple<bool, string>(true, "Table3Exists"),
                                  new Tuple<bool, string>(true, "Sproc1Exists"),
                                  new Tuple<bool, string>(true, "Sproc2Exists")
                              };

            IHealthCheck healthCheck = GetHealthCheck(results, true);

            IHealthCheckResult result = healthCheck.Execute();
            Assert.IsTrue(result.Passed);

            Console.WriteLine(result.Message);

            m_MockRepository.VerifyAll();
        }

        [TestMethod]
        public void OneObjectDoesNotExist()
        {
            var results = new[]
                              {
                                  new Tuple<bool, string>(true, "Role1Exists"),
                                  new Tuple<bool, string>(true, "Role2Exists"),
                                  new Tuple<bool, string>(false, "Table1Exists"),
                                  new Tuple<bool, string>(true, "Table2Exists"),
                                  new Tuple<bool, string>(true, "Table3Exists"),
                                  new Tuple<bool, string>(true, "Sproc1Exists"),
                                  new Tuple<bool, string>(true, "Sproc2Exists")
                              };

            IHealthCheck healthCheck = GetHealthCheck(results, true);

            IHealthCheckResult result = healthCheck.Execute();
            Assert.IsFalse(result.Passed);

            StringAssert.Contains(result.Message, "Failed to find 'Table1Exists' object in database.");

            m_MockRepository.VerifyAll();
        }

        [TestMethod]
        public void NoResultsReturnedFromSproc()
        {
            IHealthCheck healthCheck = GetHealthCheck(null, false);
            IHealthCheckResult result = healthCheck.Execute();

            Assert.IsFalse(result.Passed);
            StringAssert.Contains(result.Message, "The IsHealthy_ObjectExistenceCheck sproc produced no result row.");
        }

        [TestMethod]
        public void InvalidOperationExceptionIsHandledCorrectly()
        {
            const string exceptionMessage = "An invalid operation was attempted.";
            DbConnection connection = m_MockRepository.StrictMock<DbConnection>();
            IHealthCheck healthCheck = SqlServerStorageContainerObjectExistenceHealthCheck.Create(connection);
            InvalidOperationException exception = m_MockRepository.StrictMock<InvalidOperationException>();

            using (m_MockRepository.Record())
            {
                Expect.Call(connection.CreateCommand()).Throw(exception);
                Expect.Call(exception.Message).Return(exceptionMessage);
                Expect.Call(connection.ConnectionString).Return("SampleConnectionString");

                m_MockRepository.ReplayAll();
            }

            IHealthCheckResult result = healthCheck.Execute();

            Assert.IsFalse(result.Passed);
            StringAssert.Contains(result.Message, "An exception occured opening the connection to the database:");
            StringAssert.Contains(result.Message, exceptionMessage);

            m_MockRepository.VerifyAll();
        }

        private IHealthCheck GetHealthCheck(Tuple<bool, string>[] results, bool readReturnValue)
        {
            DbConnection connection = m_MockRepository.StrictMock<DbConnection>();
            DbCommand sprocCommand = m_MockRepository.StrictMock<DbCommand>();
            DbDataReader dataReader = m_MockRepository.StrictMock<DbDataReader>();

            IHealthCheck healthCheck = SqlServerStorageContainerObjectExistenceHealthCheck.Create(connection);

            using (m_MockRepository.Record())
            {
                Expect.Call(connection.CreateCommand()).Return(sprocCommand);
                Expect.Call(sprocCommand.ExecuteReader()).Return(dataReader);
                Expect.Call(() => { sprocCommand.CommandText = "IsHealthy_ObjectExistenceCheck"; });
                Expect.Call(() => { sprocCommand.CommandType = CommandType.StoredProcedure; });
                Expect.Call(dataReader.Read()).Return(readReturnValue);
                Expect.Call(((IDisposable) dataReader).Dispose);

                if (readReturnValue)
                {
                    int fieldCount = results.Length;
                    Expect.Call(dataReader.FieldCount).Return(fieldCount);

                    for (int index = 0; index < fieldCount; index++)
                    {
                        Expect.Call(dataReader.GetBoolean(index)).Return(results[index].Item1);
                        Expect.Call(dataReader.GetName(index)).Return(results[index].Item2);
                    }
                }
            }

            m_MockRepository.ReplayAll();
            return healthCheck;
        }
    }
}