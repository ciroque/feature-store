// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Defines the Program type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FeatureStorePerfMonExerciser
{
    using System;
    using System.Globalization;
    using System.Threading;
    using Ciroque.Foundations.FeatureStore.Instrumentation;
    using Ciroque.Foundations.FeatureStore.Mutual;
    using Ciroque.Foundations.FeatureStore.Service;
    using Ciroque.Foundations.FeatureStore.ServiceProxy;

    /// <summary>
    /// Exercises the the Performance Counters allowing a user to verify the counters using PerfMon.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// <see cref="Random"/> instance used to generate data to be displayed in the performance counters.
        /// </summary>
        private readonly Random m_Random = new Random();

        /// <summary>
        /// List of Features over which performance counters will be generated.
        /// </summary>
        private readonly Feature[] m_StandardFeatures = new[]
            {
                Feature.Create(1, Guid.NewGuid(), Guid.NewGuid(), "Standard Feature 1"),
                Feature.Create(2, Guid.NewGuid(), Guid.NewGuid(), "Standard Feature 2"),
                Feature.Create(3, Guid.NewGuid(), Guid.NewGuid(), "Standard Feature 3"),
                Feature.Create(4, Guid.NewGuid(), Guid.NewGuid(), "Standard Feature 4")
            };

        /// <summary>
        /// Thread synchronization primitive used to stop processing on the thread.
        /// </summary>
        private readonly ManualResetEvent m_Stopper = new ManualResetEvent(false);

        /// <summary>
        /// Instance of the FeatureStoreService.
        /// </summary>
        private WcfService<FeatureStoreService> m_Service;

        /// <summary>
        /// Entry point for the FeatureStorePerfMonExerciser application.
        /// </summary>
        private static void Main()
        {
            Program program = new Program();
            program.ConfigurePerfCounters();
            program.StartService();
            program.SeedStore();
            DumpStoredFeatures();
            program.Start();
            Console.ReadLine();
            program.QuitExercising();
            program.StopService();
            program.CleanUpPerfCounters();
        }

        /// <summary>
        /// Writes all the features that have been defined in the service to the Console.
        /// </summary>
        private static void DumpStoredFeatures()
        {
            IFeatureStoreServiceProxy proxy = new FeatureStoreServiceProxy();
            RetrieveDefinedFeaturesResponse response =
                proxy.RetrieveDefinedFeatures(
                    RetrieveDefinedFeaturesRequest.Create(
                        "DumpStoredFeatures", FeatureScope.Create(Guid.Empty, Guid.Empty)));
            foreach (Feature feature in response.Result)
            {
                Console.WriteLine(
                    string.Format(
                        CultureInfo.CurrentUICulture,
                        "Id [{0}], OwnerOd [{1}], Space [{2}], Name [{3}], Enabled [{4}]",
                        feature.Id,
                        feature.OwnerId,
                        feature.Space,
                        feature.Name,
                        feature.Enabled));
            }
        }

        /// <summary>
        /// Stops the WCF service.
        /// </summary>
        private void StopService()
        {
            m_Service.Stop();
        }

        /// <summary>
        /// Initializes the feature store service with the seed data features.
        /// </summary>
        private void SeedStore()
        {
            IFeatureStoreServiceProxy proxy = new FeatureStoreServiceProxy();
            foreach (Feature standardFeature in m_StandardFeatures)
            {
                proxy.CreateFeature(CreateFeatureRequest.Create(standardFeature.Name, standardFeature));
            }
        }

        /// <summary>
        /// Starts the WCF service.
        /// </summary>
        private void StartService()
        {
            m_Service = new WcfService<FeatureStoreService>();
            m_Service.Start();
        }

        /// <summary>
        /// Sets the <see cref="ManualResetEvent"/> effectively killing the thread that generates the performance counter data.
        /// </summary>
        private void QuitExercising()
        {
            m_Stopper.Set();
        }

        /// <summary>
        /// Removes the performance counters that have been created (Leave No Trace).
        /// </summary>
        private void CleanUpPerfCounters()
        {
            PerformanceCounterRegistrar.Remove();
        }

        /// <summary>
        /// Starts the thread that generates the performance counter data.
        /// </summary>
        private void Start()
        {
            Thread thread = new Thread(Runner);
            thread.Start(m_Stopper);
            Console.WriteLine(@"Press the Enter Key to exit...");
        }

        /// <summary>
        /// Ensures that the performance counters exist on the host machine.
        /// </summary>
        private void ConfigurePerfCounters()
        {
            PerformanceCounterRegistrar.EnsureExist();
        }

        /// <summary>
        /// Thread routine that calls the various service methods thus generating the performance counter data.
        /// </summary>
        /// <param name="stopper">
        /// The stopper.
        /// </param>
        private void Runner(object stopper)
        {
            ManualResetEvent stopEvent = (ManualResetEvent)stopper;
            IFeatureStoreServiceProxy proxy = new FeatureStoreServiceProxy();
            Action<int>[] calls = new Action<int>[]
                                      {
                                          v => proxy.CreateFeature(CreateFeatureRequest.Create(
                                              "CreateFeature lambda",
                                              Feature.Create(1, Guid.NewGuid(), Guid.NewGuid(), Thread.CurrentThread.ManagedThreadId.ToString()))),
                                          v =>
                                          proxy.UpdateFeatureState(
                                              UpdateFeatureStateRequest.Create(
                                                  "UpdateFeatureState lambda",
                                                  CreateFeatureKey(v),
                                                  !m_StandardFeatures[v].Enabled)),
                                          v =>
                                          proxy.CheckFeatureState(
                                              CheckFeatureStateRequest.Create(
                                                  "CheckFeatureState lambda", CreateFeatureKey(v))), v =>
                                                                                                     proxy.RetrieveDefinedFeatures(
                                                                                                             RetrieveDefinedFeaturesRequest.Create(
                                                                                                                 "RetrieveDefinedFeatures lambda",
                                                                                                                 FeatureScope.Create(m_StandardFeatures[v].OwnerId, m_StandardFeatures[v].Space)))
                                      };

            while (true)
            {
                int index = m_Random.Next(0, calls.Length);

                Console.WriteLine(@"Invoking lambda {0}", index);
                calls[index].Invoke(index);

                if (stopEvent.WaitOne(20))
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Generates a <see cref="FeatureKey"/> from the <see cref="Feature"/> at the given index.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <returns>
        /// An initialized <see cref="FeatureKey"/>.
        /// </returns>
        private FeatureKey CreateFeatureKey(int index)
        {
            Feature feature = m_StandardFeatures[index];
            return FeatureKey.Create(feature.Id, feature.OwnerId, feature.Space);
        }
    }
}