// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Defines the Program type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GenerateMassiveStorageFile
{
    using System;
    using System.Collections.Concurrent;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Formatters.Binary;
    using Ciroque.Foundations.FeatureStore.Data;
    using Ciroque.Foundations.FeatureStore.Mutual;

    internal class Program
    {
        private readonly int m_FeatureCount;
        private ConcurrentDictionary<FeatureKey, Feature> m_Dictionary;
        private string m_SampleKeysFilename = @"SampleFeatureKeys.txt";
        private CacheSwappingStorageContainer m_StorageContainer;
        private string m_StorageContainerFilename = @".\MassiveFeatureStore.dat";

        private Program(int featureCount)
        {
            m_FeatureCount = featureCount;
        }

        private static void Main(string[] args)
        {
            int featureCount = 100000;
            if (args.Length == 1)
            {
                int temp;
                if (int.TryParse(args[0], out temp))
                {
                    featureCount = temp;
                }
            }

            Program program = new Program(featureCount);
            program.BuildFeatureStore();
            program.WriteStoreToFile();
            program.WriteKeySampleFile();
            program.ClearFeatureStore();
            program.LoadStorageContainer();
            program.ProfileRetrieve();

            Console.WriteLine("Press Enter key to exit.");
            Console.ReadLine();
        }

        private void ProfileRetrieve()
        {
            Stopwatch stopwatch = new Stopwatch();
            long acc = 0;
            long min = 0;
            long max = 0;
            long lineNumber = 0;

            using (new InternalStopWatch("ProfileRetrieve"))
            {
                using (StreamReader streamReader = File.OpenText(m_SampleKeysFilename))
                {
                    string line = streamReader.ReadLine();
                    while (!string.IsNullOrEmpty(line))
                    {
                        lineNumber++;

                        FeatureKey key = BuildFeatureKey(line);

                        stopwatch.Start();
                        Feature feature = m_StorageContainer.Retrieve(key);
                        stopwatch.Stop();

                        Debug.Assert(feature != null);

                        long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

                        min = lineNumber == 1 ? elapsedMilliseconds : Math.Min(min, elapsedMilliseconds);
                        max = Math.Max(max, elapsedMilliseconds);
                        acc += elapsedMilliseconds;

                        line = streamReader.ReadLine();
                    }
                }

                Console.WriteLine(@">> Retrieve performance: Min: {0}, Max {1}, Avg {2}", min, max, (acc/lineNumber));
            }
        }

        private static FeatureKey BuildFeatureKey(string line)
        {
            string[] parts = line.Split(' ');
            return FeatureKey.Create(
                long.Parse(parts[0]),
                new Guid(parts[2]), new Guid(parts[1]));
        }

        private void ClearFeatureStore()
        {
            using (new InternalStopWatch("ClearFeatureStore"))
            {
                m_Dictionary.Clear();
                m_Dictionary = null;
            }
        }

        private void LoadStorageContainer()
        {
            using (new InternalStopWatch("LoadStorageContainer"))
            {
                m_StorageContainer = new CacheSwappingStorageContainer(m_StorageContainerFilename);
            }
        }

        private void WriteKeySampleFile()
        {
            using (new InternalStopWatch("WriteKeySampleFile"))
            {
                var q = from k in m_Dictionary.Keys where k.Id%7 == 0 select k;
                m_SampleKeysFilename = @"SampleFeatureKeys.txt";
                using (StreamWriter streamWriter = File.CreateText(m_SampleKeysFilename))
                {
                    foreach (FeatureKey featureKey in q)
                    {
                        streamWriter.WriteLine("{0} {1} {2}", featureKey.Id, featureKey.Space, featureKey.OwnerId);
                    }
                    streamWriter.Flush();
                }
            }
        }

        private void WriteStoreToFile()
        {
            using (new InternalStopWatch("WriteStoreToFile"))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                m_StorageContainerFilename = @".\MassiveFeatureStore.dat";
                using (Stream streamWriter = File.OpenWrite(m_StorageContainerFilename))
                {
                    formatter.Serialize(streamWriter, m_Dictionary);
                    streamWriter.Flush();
                }
            }
        }

        private void BuildFeatureStore()
        {
            Console.WriteLine(string.Format(CultureInfo.CurrentUICulture, @">. Building {0} Features.", m_FeatureCount));
            using (new InternalStopWatch("BuildFeatureStore"))
            {
                m_Dictionary = new ConcurrentDictionary<FeatureKey, Feature>();
                for (int index = 0; index < m_FeatureCount; index++)
                {
                    Guid space = Guid.NewGuid();
                    Guid owner = Guid.NewGuid();
                    m_Dictionary.AddOrUpdate(FeatureKey.Create(index, owner, space),
                                             Feature.Create(index, owner, space, "Feature " + index),
                                             (k, f) => f);
                }
            }
        }

        #region Nested type: InternalStopWatch

        private class InternalStopWatch : IDisposable
        {
            private readonly string m_Description;
            private readonly Stopwatch m_Stopwatch = new Stopwatch();

            public InternalStopWatch(string description)
            {
                m_Description = description;
                m_Stopwatch.Start();
            }

            #region IDisposable Members

            public void Dispose()
            {
                m_Stopwatch.Stop();
                Console.WriteLine(string.Format(CultureInfo.CurrentUICulture, @">> {0} elapsed milliseconds: {1}",
                                                m_Description, m_Stopwatch.ElapsedMilliseconds));
            }

            #endregion
        }

        #endregion
    }
}