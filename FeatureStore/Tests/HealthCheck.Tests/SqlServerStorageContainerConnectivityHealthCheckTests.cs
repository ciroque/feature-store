namespace Ciroque.Foundations.HealthCheck.Tests
{
    using System;
    using System.Data.Common;
    using System.Data.SqlClient;
    using FeatureStore.HealthChecks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Rhino.Mocks;

    [TestClass]
    public class SqlServerStorageContainerConnectivityHealthCheckTests
    {
        private readonly MockRepository m_MockRepository = new MockRepository();

        [TestMethod]
        public void SqlServerStorageContainerConnectivityHealthCheckPasses()
        {
            DbConnection connection = m_MockRepository.StrictMock<DbConnection>();
            IHealthCheck healthCheck = SqlServerStorageContainerConnectivityHealthCheck.Create(connection);

            using (m_MockRepository.Record())
            {
                Expect.Call(connection.Open);
                Expect.Call(connection.ConnectionString).Return("SampleConnectionString");
                m_MockRepository.ReplayAll();

                IHealthCheckResult result = healthCheck.Execute();

                Assert.IsTrue(result.Passed);
                StringAssert.Contains(result.Message, "Connection to SqlServer database succeeded");
                StringAssert.Contains(result.Message, "SampleConnectionString");

                m_MockRepository.VerifyAll();
            }
        }

        [TestMethod]
        public void SqlServerStorageContainerConnectivityHealthCheckFailsOnInvalidOperationException()
        {
            const string exceptionMessage = "Cannot open a connection without specifying a data source or server.";
            AssertExceptionHandled<InvalidOperationException>(exceptionMessage);
        }

        /// <summary>
        ///   <see cref = "SqlException" /> is sealed cannot mock that.
        /// </summary>
        [TestMethod, Ignore]
        public void SqlServerStorageContainerConnectivityHealthCheckHandlesSqlException()
        {
            const string exceptionMessage = "Some freaky SqlException occurred";
            AssertExceptionHandled<SqlException>(exceptionMessage);
        }

        /// <summary>
        ///   <see cref = "ArgumentException" /> is sealed. Cannot mock that.
        /// </summary>
        [TestMethod, Ignore]
        public void SqlServerStorageContainerConnectivityHealthCheckHandlesArgumentException()
        {
            const string exceptionMessage = "One of the arguments was bad";
            AssertExceptionHandled<SqlException>(exceptionMessage);
        }

        private void AssertExceptionHandled<T>(string exceptionMessage) where T : Exception
        {
            DbConnection connection = m_MockRepository.StrictMock<DbConnection>();
            IHealthCheck healthCheck = SqlServerStorageContainerConnectivityHealthCheck.Create(connection);
            T exception = m_MockRepository.StrictMock<T>();

            using (m_MockRepository.Record())
            {
                Expect.Call(connection.Open).Throw(exception);
                Expect.Call(connection.ConnectionString).Return("SampleConnectionString");
                Expect.Call(exception.Message).Return(exceptionMessage);

                m_MockRepository.ReplayAll();
            }

            IHealthCheckResult result = healthCheck.Execute();

            Assert.IsFalse(result.Passed);
            StringAssert.Contains(result.Message, "An exception occured opening the connection to the database:");
            StringAssert.Contains(result.Message, exceptionMessage);

            m_MockRepository.VerifyAll();
        }
    }
}