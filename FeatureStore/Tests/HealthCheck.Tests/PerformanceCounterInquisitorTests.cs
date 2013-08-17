namespace Ciroque.Foundations.HealthCheck.Tests
{
    using System;
    using System.Diagnostics;
    using FeatureStore.HealthChecks;
    using FeatureStore.Instrumentation;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PerformanceCounterInquisitorTests
    {
        [TestMethod]
        public void CountersCreatedAndCleanedUp()
        {
            IPerformanceCounterInquisitor inquisitor = PerformanceCounterInquisitor.Create();

            using (PerformanceCounterRegistrarDisposable.Create())
            {
                Assert.IsTrue(inquisitor.CheckCategoryExists(PerformanceCounterRegistrar.CategoryName));
                foreach (CounterCreationData counterCreationData in PerformanceCounterRegistrar.CounterDefinitions)
                {
                    Assert.IsTrue(inquisitor.CheckCounterExists(counterCreationData.CounterName));
                }
            }

            Assert.IsFalse(inquisitor.CheckCategoryExists(PerformanceCounterRegistrar.CategoryName));

            // The category contains the counters so when the category does not exist you can be assured the categories do not exist.
        }

        #region Nested type: PerformanceCounterRegistrarDisposable

        private class PerformanceCounterRegistrarDisposable : IDisposable
        {
            #region IDisposable Members

            public void Dispose()
            {
                PerformanceCounterRegistrar.Remove();
            }

            #endregion

            public static IDisposable Create()
            {
                PerformanceCounterRegistrar.EnsureExist();
                return new PerformanceCounterRegistrarDisposable();
            }
        }

        #endregion
    }
}