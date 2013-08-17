namespace Ciroque.Foundations.FeatureStore.Instrumentation.Tests
{
    using System.Diagnostics;

    public class PerformanceCounterCategoryTestManager
    {
        public const string SampleCounterCategoryName = "PerformanceCounterReporterTests";
        public const string SampleOperationCounterName = "OperationCounter";
        public const string SampleExecutionTimeCounterName = "ExecutionTimeCounter";

        public static void Initialize()
        {
            if (!PerformanceCounterCategory.Exists(SampleCounterCategoryName))
            {
                CounterCreationData opCounter = new CounterCreationData(SampleOperationCounterName, string.Empty,
                                                                        PerformanceCounterType.NumberOfItems64);
                CounterCreationData execCounter = new CounterCreationData(SampleExecutionTimeCounterName, string.Empty,
                                                                          PerformanceCounterType.NumberOfItems64);

                CounterCreationDataCollection counters =
                    new CounterCreationDataCollection(new[] {opCounter, execCounter});

                PerformanceCounterCategory.Create(
                    SampleCounterCategoryName,
                    string.Empty,
                    PerformanceCounterCategoryType.SingleInstance,
                    counters);
            }
        }

        public static void CleanUp()
        {
            if (PerformanceCounterCategory.Exists(SampleCounterCategoryName))
            {
                PerformanceCounterCategory.Delete(SampleCounterCategoryName);
            }
        }
    }
}