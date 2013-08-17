namespace Ciroque.Foundations.HealthCheck.Tests
{
    using FeatureStore.HealthChecks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Rhino.Mocks;

    [TestClass]
    public class HealthCheckRunnerTests
    {
        private readonly MockRepository m_Repository = new MockRepository();

        [TestMethod]
        public void HealthCheckRunnerExecutesListOfHealthChecksAndAllSucceed()
        {
            IHealthCheck firstHealthCheck = m_Repository.StrictMock<IHealthCheck>();
            IHealthCheck secondHealthCheck = m_Repository.StrictMock<IHealthCheck>();
            IHealthCheck thirdHealthCheck = m_Repository.StrictMock<IHealthCheck>();

            IHealthCheckResult firstHealthCheckResult = m_Repository.StrictMock<IHealthCheckResult>();
            IHealthCheckResult secondHealthCheckResult = m_Repository.StrictMock<IHealthCheckResult>();
            IHealthCheckResult thirdHealthCheckResult = m_Repository.StrictMock<IHealthCheckResult>();

            using (m_Repository.Record())
            {
                Expect.Call(firstHealthCheck.Execute()).Return(firstHealthCheckResult);
                Expect.Call(secondHealthCheck.Execute()).Return(secondHealthCheckResult);
                Expect.Call(thirdHealthCheck.Execute()).Return(thirdHealthCheckResult);

                Expect.Call(firstHealthCheckResult.Passed).Return(true);
                Expect.Call(firstHealthCheckResult.Message).Return("The Health Check was successful.");

                Expect.Call(secondHealthCheckResult.Passed).Return(true);
                Expect.Call(secondHealthCheckResult.Message).Return("The Health Check was successful.");

                Expect.Call(thirdHealthCheckResult.Passed).Return(true);
                Expect.Call(thirdHealthCheckResult.Message).Return("The Health Check was successful.");

                m_Repository.ReplayAll();

                HealthCheckResultCollection resultCollection =
                    HealthCheckRunner.RunHealthChecks(
                        new HealthChecklistCollection(new[] {firstHealthCheck, secondHealthCheck, thirdHealthCheck}));

                Assert.AreEqual(3, resultCollection.Count);
                foreach (IHealthCheckResult healthCheckResult in resultCollection)
                {
                    Assert.IsTrue(healthCheckResult.Passed);
                    StringAssert.Contains(healthCheckResult.Message, "success");
                }

                m_Repository.VerifyAll();
            }
        }

        [TestMethod]
        public void ExceptionThrownInHealthCheckImplementationIsReportedAsFailedHealthCheckResult()
        {
            IHealthCheck firstHealthCheck = m_Repository.StrictMock<IHealthCheck>();
            HealthCheckException healthCheckException = m_Repository.StrictMock<HealthCheckException>();

            using (m_Repository.Record())
            {
                Expect.Call(firstHealthCheck.Execute()).Throw(healthCheckException);
                Expect.Call(healthCheckException.Message).Return("Sample Exception");
                m_Repository.ReplayAll();

                HealthCheckResultCollection resultCollection =
                    HealthCheckRunner.RunHealthChecks(new HealthChecklistCollection(new[] {firstHealthCheck}));

                Assert.AreEqual(1, resultCollection.Count);

                IHealthCheckResult result = resultCollection[0];

                Assert.IsFalse(result.Passed);
                StringAssert.StartsWith(result.Message,
                                        "An exception was thrown during the execution of the Health Check");
                StringAssert.Contains(result.Message, "Sample Exception");

                m_Repository.VerifyAll();
            }
        }
    }
}