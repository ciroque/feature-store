namespace Ciroque.Foundations.HealthCheck.Tests
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using FeatureStore.HealthChecks;
    using FeatureStore.Instrumentation;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Rhino.Mocks;

    [TestClass]
    public class InstrumentationHealthTests
    {
        private readonly MockRepository m_MockRepository = new MockRepository();

        private readonly List<CounterCreationData> m_PerformanceCounterDefinitions =
            (List<CounterCreationData>) PerformanceCounterRegistrar.CounterDefinitions;

        [TestMethod]
        public void PerformanceCountersDefinedHealthCheck()
        {
            IHealthCheckResult result = BuildHealthCheckAndSetExpectations(true, BuildAllTrueArray());
            m_MockRepository.VerifyAll();
            Assert.IsTrue(result.Passed);
            StringAssert.Contains(result.Message,
                                  "The performance counter category and all counters have been registered.");
        }

        [TestMethod]
        public void CategoryNotDefined()
        {
            IHealthCheckResult result = BuildHealthCheckAndSetExpectations(false, BuildAllTrueArray());
            m_MockRepository.VerifyAll();
            Assert.IsFalse(result.Passed);
            StringAssert.Contains(result.Message, "The performance counter category has not been registered.");
        }

        [TestMethod]
        public void CategoryDefinedMissingTwoCounters()
        {
            IHealthCheckResult result = BuildHealthCheckAndSetExpectations(true, BuildAlternatingBoolArray());
            m_MockRepository.VerifyAll();
            Assert.IsFalse(result.Passed);
            StringAssert.Contains(result.Message, "category has been registered, but counter(s) are missing");
            StringAssert.Contains(result.Message, m_PerformanceCounterDefinitions[1].CounterName);
        }

        private bool[] CreateBoolArray()
        {
            return new bool[m_PerformanceCounterDefinitions.Count];
        }

        private bool[] BuildAlternatingBoolArray()
        {
            bool[] bools = CreateBoolArray();
            for (int index = 0; index < bools.Length; index++)
            {
                bools[index] = index%2 == 0 ? true : false;
            }

            return bools;
        }

        private bool[] BuildAllTrueArray()
        {
            bool[] bools = CreateBoolArray();
            for (int index = 0; index < bools.Length; index++)
            {
                bools[index] = true;
            }

            return bools;
        }

        private IHealthCheckResult BuildHealthCheckAndSetExpectations(bool categoryFound, bool[] counterExistence)
        {
            IPerformanceCounterInquisitor inquisitor = m_MockRepository.StrictMock<IPerformanceCounterInquisitor>();
            IHealthCheck healthCheck = PerformanceCounterRegistrationHealthCheck.Create(inquisitor);

            using (m_MockRepository.Record())
            {
                Expect.Call(inquisitor.CheckCategoryExists(PerformanceCounterRegistrar.CategoryName)).Return(
                    categoryFound);

                Debug.Assert(counterExistence.Length == m_PerformanceCounterDefinitions.Count);

                if (categoryFound)
                {
                    for (int index = 0; index < m_PerformanceCounterDefinitions.Count; index++)
                    {
                        Expect.Call(inquisitor.CheckCounterExists(m_PerformanceCounterDefinitions[index].CounterName)).
                            Return(counterExistence[index]);
                    }
                }

                m_MockRepository.ReplayAll();
            }

            return healthCheck.Execute();
        }
    }
}