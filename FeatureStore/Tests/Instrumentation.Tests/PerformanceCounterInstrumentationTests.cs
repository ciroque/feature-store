namespace Ciroque.Foundations.FeatureStore.Instrumentation.Tests
{
    using System.Diagnostics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PerformanceCounterInstrumentationTests
    {
        [TestMethod]
        public void CategoryAndCountersCanBeCreatedAndDeleted()
        {
            PerformanceCounterRegistrar.EnsureExist();

            Assert.IsTrue(PerformanceCounterCategory.Exists(PerformanceCounterRegistrar.CategoryName));

            foreach (CounterCreationData counterDefinition in PerformanceCounterRegistrar.CounterDefinitions)
            {
                Assert.IsTrue(PerformanceCounterCategory.CounterExists(counterDefinition.CounterName,
                                                                       PerformanceCounterRegistrar.CategoryName));
            }

            PerformanceCounterRegistrar.Remove();

            Assert.IsFalse(PerformanceCounterCategory.Exists(PerformanceCounterRegistrar.CategoryName));
        }

        [TestMethod]
        public void PerformanceCounterReporterFactoryCreatesInstances()
        {
            try
            {
                PerformanceCounterRegistrar.EnsureExist();

                PerformanceCounterReporter reporter =
                    PerformanceCounterReporterFactory.CreateReporter(PerformanceCounterReporterType.CreateFeature);
                Assert.IsNotNull(reporter);

                reporter =
                    PerformanceCounterReporterFactory.CreateReporter(PerformanceCounterReporterType.CheckFeatureState);
                Assert.IsNotNull(reporter);

                reporter =
                    PerformanceCounterReporterFactory.CreateReporter(PerformanceCounterReporterType.UpdateFeatureState);
                Assert.IsNotNull(reporter);
            }
            finally
            {
                PerformanceCounterRegistrar.Remove();
            }
        }
    }
}