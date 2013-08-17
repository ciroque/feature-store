namespace Ciroque.Foundations.HealthCheck.Tests
{
    using System.IO;
    using System.Reflection;
    using System.Xml.XPath;
    using FeatureStore.HealthChecks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Rhino.Mocks;

    [TestClass]
    public class Log4NetConfigurationHealthCheckTests
    {
        private readonly MockRepository m_MockRepository = new MockRepository();

        [TestMethod]
        public void Log4NetConfigurationIsCorrect()
        {
            IHealthCheckResult result = GetHealthCheckResult(Log4NetConfigurationKeys.Correct);

            Assert.IsTrue(result.Passed);
            StringAssert.Contains(result.Message, "The log4net configuration is correct.");
        }

        [TestMethod]
        public void DateParameterMissing()
        {
            IHealthCheckResult result = GetHealthCheckResult(Log4NetConfigurationKeys.DateParameterMissing);

            Assert.IsFalse(result.Passed);
            StringAssert.Contains(result.Message, "The definition of the @Date parameter is missing.");
        }

        private IHealthCheckResult GetHealthCheckResult(string configResourceName)
        {
            ILog4NetConfigurationResolver resolver = m_MockRepository.StrictMock<ILog4NetConfigurationResolver>();
            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream stream = assembly.GetManifestResourceStream(configResourceName);
            XPathDocument document = new XPathDocument(stream);

            using (m_MockRepository.Record())
            {
                Expect.Call(resolver.LoadConfiguration()).Return(document);
                m_MockRepository.ReplayAll();
            }

            IHealthCheck healthCheck = Log4NetConfigurationHealthCheck.Create(resolver);

            return healthCheck.Execute();
        }
    }
}