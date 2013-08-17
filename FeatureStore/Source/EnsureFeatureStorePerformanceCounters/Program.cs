// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Defines the Program type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace EnsureFeatureStorePerformanceCounters
{
    using System;
    using Ciroque.Foundations.FeatureStore.Instrumentation;

    /// <summary>
    /// Utilizes the <see cref="PerformanceCounterRegistrar"/> class to ensure the Performance Counters have been registered on a system.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Entry point for the 
        /// </summary>
        private static void Main()
        {
            try
            {
                PerformanceCounterRegistrar.EnsureExist();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}