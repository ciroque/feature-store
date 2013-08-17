// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Ciroque Enterprises, Inc">
//   Copyright 2011 by Ciroque Enterprises, Inc. All Rights Reserved.
// </copyright>
// <summary>
//   Defines the Program type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.ChartStorePerformance
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Windows.Forms.DataVisualization.Charting;
    using Data;
    using Mutual;

    /// <summary>
    /// The entry point and primary logic of the ChartStorePerformance application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Represents the number of features to write to the storage file.
        /// </summary>
        private const int FeatureCount = 50;

        /// <summary>
        /// Represents the output filename to accept the CSV-formatted data.
        /// </summary>
        private const string CsvFilename = @"feature-storage-time-graph.csv";

        /// <summary>
        /// Represents name of the column containing the elapsed milliseconds.
        /// </summary>
        private const string MillsecondsColumnName = "Milliseconds";

        /// <summary>
        /// Represents name of the column containing the feature count written to storage.
        /// </summary>
        private const string FeatureCountColumnName = "FeatureCount";

        /// <summary>
        /// The entry point and primary logic of the ChartStorePerformance application.
        /// </summary>
        private static void Main()
        {
            Program program = new Program();
            program.WriteFeaturesToStorage();
            program.DrawChart();
        }

        /// <summary>
        /// Handles the actual drawing of the chart.
        /// </summary>
        private void DrawChart()
        {
            // CSV csv = new CSV(CsvFilename, new[] {','});
// DataTable dataTable = csv.GetDataTable();
// 
// var m = dataTable.Compute("MIN([FeatureCount])", string.Empty);
            new Chart();

            // chart.Data = dataTable;
// chart.EnableMultipleScales = true;
// chart.DrawLegend = true;
// chart.DrawScale = true;
// chart.Type = Chart.ChartType.Bar;
// chart.Height = 768;
// chart.Width = 1280;
// 
// ChartColumn xAxis  = new ChartColumn(FeatureCountColumnName, TypeCode.Int16);
// 
// xAxis.Range.Min = FeatureCount - 1;
// xAxis.Range.Max = 0;
// 
// chart.DependentColumns.Add(new ChartColumn(MillsecondsColumnName, TypeCode.Int32));
// chart.IndependentColumns.Add(xAxis);
// 
// System.Drawing.Imaging.ImageCodecInfo[] Info = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();
// System.Drawing.Imaging.EncoderParameters Params = new System.Drawing.Imaging.EncoderParameters(1);
// Params.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
// 
// Bitmap bitmap = chart.GetBitmap();
// 
// object o = xAxis.Range.GetStep(1);
// 
// using(FileStream writer = File.Create(@"StorePerformanceGraph.bmp"))
// {
// bitmap.Save(writer, Info[1], Params);
// }
        }

        /// <summary>
        /// writes the features out to a storage file.
        /// </summary>
        private void WriteFeaturesToStorage()
        {
            CacheSwappingStorageContainer onlineStorageContainer = new CacheSwappingStorageContainer(@"features.dat");
            using (StreamWriter writer = File.CreateText(CsvFilename))
            {
                writer.WriteLine("{0},{1}", FeatureCountColumnName, MillsecondsColumnName);
                Stopwatch stopwatch = new Stopwatch();
                for (int index = 0; index < FeatureCount; index++)
                {
                    string featureName = "Feature " + index;

                    stopwatch.Reset();
                    stopwatch.Start();
                    Feature feature =
                        onlineStorageContainer.Store(Feature.Create(index, Guid.NewGuid(), Guid.NewGuid(), featureName));
                    stopwatch.Stop();
                    Console.WriteLine(@"{0} {1} {2} {3}", feature.Id, feature.Space, feature.OwnerId, feature.Name);
                    writer.WriteLine("{0},{1}", index, stopwatch.ElapsedMilliseconds);
                    writer.Flush();
                }
            }
        }
    }
}