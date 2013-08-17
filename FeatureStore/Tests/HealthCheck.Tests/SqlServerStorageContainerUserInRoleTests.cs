namespace Ciroque.Foundations.HealthCheck.Tests
{
    using System;
    using System.Data.Common;
    using FeatureStore.HealthChecks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Rhino.Mocks;

    [TestClass]
    public class SqlServerStorageContainerUserInRoleTests
    {
        private readonly MockRepository m_MockRepository = new MockRepository();

        [TestMethod]
        public void UserIsFoundInRole()
        {
            IHealthCheck healthCheck = BuildHealthCheckAndSetExpectations(true, true);
            IHealthCheckResult result = healthCheck.Execute();

            Assert.IsTrue(result.Passed);
            StringAssert.Contains(result.Message, "found in the necessary role.");

            m_MockRepository.VerifyAll();
        }

        [TestMethod]
        public void UserIsNotFoundInRole()
        {
            IHealthCheck healthCheck = BuildHealthCheckAndSetExpectations(false, true);
            IHealthCheckResult result = healthCheck.Execute();

            Assert.IsFalse(result.Passed);
            StringAssert.Contains(result.Message, "not found in the necessary role.");

            m_MockRepository.VerifyAll();
        }

        [TestMethod]
        public void NoRowsReturnedFromRoleMemberQuery()
        {
            IHealthCheck healthCheck = BuildHealthCheckAndSetExpectations(true, false);
            IHealthCheckResult result = healthCheck.Execute();

            Assert.IsFalse(result.Passed);
            StringAssert.Contains(result.Message,
                                  "The call to check if the user was in the necessary role returned no row.");

            m_MockRepository.VerifyAll();
        }

        [TestMethod]
        public void InvalidOperationExceptionIsHandledCorrectly()
        {
            const string exceptionMessage = "An invalid operation was attempted.";
            DbConnection connection = m_MockRepository.StrictMock<DbConnection>();
            IHealthCheck healthCheck = SqlServerStorageContainerUserInRoleHealthCheck.Create(connection);
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

        private IHealthCheck BuildHealthCheckAndSetExpectations(bool userFoundInRole, bool rowsReturned)
        {
            DbConnection connection = m_MockRepository.StrictMock<DbConnection>();
            DbCommand command = m_MockRepository.DynamicMock<DbCommand>();
            DbDataReader dataReader = m_MockRepository.StrictMock<DbDataReader>();

            IHealthCheck healthCheck = SqlServerStorageContainerUserInRoleHealthCheck.Create(connection);

            using (m_MockRepository.Record())
            {
                Expect.Call(connection.CreateCommand()).Return(command);
                Expect.Call(command.ExecuteReader()).Return(dataReader);
                Expect.Call(dataReader.Read()).Return(rowsReturned);
                Expect.Call(((IDisposable) dataReader).Dispose);

                if (rowsReturned)
                    Expect.Call(dataReader.GetBoolean(0)).Return(userFoundInRole);
            }

            m_MockRepository.ReplayAll();
            return healthCheck;
        }
    }
}