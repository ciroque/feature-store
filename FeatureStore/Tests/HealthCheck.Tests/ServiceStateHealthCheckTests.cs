namespace Ciroque.Foundations.HealthCheck.Tests
{
    using FeatureStore.HealthChecks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Rhino.Mocks;

    [TestClass]
    public class ServiceStateHealthCheckTests
    {
        private readonly MockRepository m_MockRepository = new MockRepository();

        [TestMethod]
        public void ServiceIsRunning()
        {
            IHealthCheckResult result = RunHealthCheck(true);

            Assert.IsTrue(result.Passed);
            StringAssert.Contains(result.Message, "The Feature Store service is running.");
            m_MockRepository.VerifyAll();
        }

        [TestMethod]
        public void ServiceIsNotRunning()
        {
            IHealthCheckResult result = RunHealthCheck(false);

            Assert.IsFalse(result.Passed);
            StringAssert.Contains(result.Message, "The Feature Store service is not running.");
            m_MockRepository.VerifyAll();
        }

        [TestMethod]
        public void ServiceIsNotInstalled()
        {
            IServiceStateInquisitor serviceStateInquisitor = m_MockRepository.StrictMock<IServiceStateInquisitor>();
            IHealthCheck healthCheck = FeatureStoreServiceStateHealthCheck.Create(serviceStateInquisitor);

            using (m_MockRepository.Record())
            {
                Expect.Call(serviceStateInquisitor.ServiceIsRunning()).Throw(new ServiceNotInstalledException());
                m_MockRepository.ReplayAll();
            }

            IHealthCheckResult result = healthCheck.Execute();

            Assert.IsFalse(result.Passed);
            StringAssert.Contains(result.Message, "The Feature Store service is not installed.");
        }

        private IHealthCheckResult RunHealthCheck(bool isRunning)
        {
            IServiceStateInquisitor serviceStateInquisitor = m_MockRepository.StrictMock<IServiceStateInquisitor>();
            IHealthCheck healthCheck = FeatureStoreServiceStateHealthCheck.Create(serviceStateInquisitor);

            using (m_MockRepository.Record())
            {
                Expect.Call(serviceStateInquisitor.ServiceIsRunning()).Return(isRunning);
                m_MockRepository.ReplayAll();
            }

            return healthCheck.Execute();
        }
    }
}