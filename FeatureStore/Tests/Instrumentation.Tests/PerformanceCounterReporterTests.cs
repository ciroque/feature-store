namespace Ciroque.Foundations.FeatureStore.Instrumentation.Tests
{
    using System.Diagnostics;
    using System.Threading;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PerformanceCounterReporterTests
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            PerformanceCounterCategoryTestManager.Initialize();
            Debug.WriteLine("ClassInitialize after creating categories.");
        }

        [ClassCleanup]
        public static void ClassCleanUp()
        {
            PerformanceCounterCategoryTestManager.CleanUp();
            Debug.WriteLine("ClassCleanup after removing categories.");
        }

        [TestMethod]
        public void PerformanceCounterReporterReportsCountAndElapsedTime()
        {
            using (PerformanceCounterReporterFactory.CreateReporter(
                PerformanceCounterCategoryTestManager.SampleCounterCategoryName,
                PerformanceCounterCategoryTestManager.SampleOperationCounterName,
                PerformanceCounterCategoryTestManager.SampleExecutionTimeCounterName))
            {
                Thread.Sleep(250);
            }

            float operationCount =
                new PerformanceCounter(
                    PerformanceCounterCategoryTestManager.SampleCounterCategoryName,
                    PerformanceCounterCategoryTestManager.SampleOperationCounterName,
                    true).NextValue();

            Assert.AreEqual(1.0, operationCount);

            float executionTime =
                new PerformanceCounter(
                    PerformanceCounterCategoryTestManager.SampleCounterCategoryName,
                    PerformanceCounterCategoryTestManager.SampleExecutionTimeCounterName,
                    true).NextValue();
            Assert.IsTrue(executionTime > 0);
        }
    }
}