/*****************************************************************
 * Copyright 2008 Microsoft Corporation
 * File    : ChartColumn.cs
 * Created : 2008-06-19
 * Author  : danieln
 * Purpose : Data definition for chart, based on column of DataTable
 * 
 * Change History :
 *  Date        Author      Change
 *  -------------------------------
 *  2008-06-19  danieln     Created
 *  2008-06-20  danieln     Function complete
 * 
 * Notes :
 *****************************************************************/
using System;
using System.Collections;
using System.Text;

namespace ChartUtility
{
    public class ChartColumnCollection : CollectionBase
    {
        public ChartColumn this[int index]
        {
            get { return (ChartColumn)this.List[index]; }
            set { this.List[index] = value; }
        }

        public int Add(ChartColumn column) { return this.List.Add(column); }
        public void Insert(int index, ChartColumn column) { this.List.Insert(index, column); }
        public void Remove(ChartColumn column) { this.List.Remove(column); }
        public int IndexOf(ChartColumn column) { return this.List.IndexOf(column); }
        public bool Contains(ChartColumn column) { return this.List.Contains(column); }
    }
    public class ChartColumn
    {
        private float[] gScale = new float[] { 0.0010f, 0.0020f, 0.0025f, 0.0050f };
        public string ColumnName;
        protected Range _Range;
        public Range Range { get { return _Range; } set { _Range = value; } }

        public string LineColor;

        public ChartColumn(string name, TypeCode type)
        {
            if (type == TypeCode.DateTime)
                _Range = new TimeRange();
            else
                _Range = new NumberRange();
            ColumnName = name;
        }
/**
        public TimeScale BestFitTimeSpan()
        {
            if (DataType != TypeCode.DateTime)
                throw new Exception("Cannot generate time scale from a non DateTime field");
            TimeSpan timeSpan = End.Subtract(Start);
            if (timeSpan.Days > 60)
            {
                return new TimeScale(DateInterval.Monthly, Math.Max(1, ((30 + timeSpan.Days) / 30) / 4));
            }
            else if (timeSpan.Days > 10)
            {
                return new TimeScale(DateInterval.Weekly, 7);
            }
            else if (timeSpan.Days > 2)
            {
                return new TimeScale(DateInterval.Daily, 1);
            }
            else
            {
                return new TimeScale(DateInterval.Hourly, Math.Max(1, (timeSpan.Hours) / 10));
            }
        }
/**/
    }
}
