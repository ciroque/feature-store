/*****************************************************************
 * Copyright 2008 Microsoft Corporation
 * File    : TimeUtil.cs
 * Created : 2008-06-19
 * Author  : danieln
 * Purpose : Date utilities
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

namespace ChartUtility
{
    public enum DateInterval
    {
        Monthly = 0,
        Weekly,
        Daily,
        Hourly
    };
    public class TimeScale
    {
        public DateInterval Interval;
        public int Count;
        public TimeScale(DateInterval interval, int n)
        {
            Interval = interval; Count = n;
        }
        public TimeScale(DateTime start, DateTime end)
        {
            TimeSpan timeSpan = end.Subtract(start);
            if (timeSpan.Days > 60)
            {
                Interval = DateInterval.Monthly;
                Count = Math.Max(1, ((30 + timeSpan.Days) / 30) / 4);
            }
            else if (timeSpan.Days > 10)
            {
                Interval = DateInterval.Weekly;
                Count = 1;
            }
            else if (timeSpan.Days > 2)
            {
                Interval = DateInterval.Daily;
                Count = 1;
            }
            else
            {
                Interval = DateInterval.Hourly;
                Count = Math.Max(1, (timeSpan.Hours) / 10);
            }
        }
        public int IntervalsInRange(DateTime start, DateTime end)
        {
            int cnt = 0;
            DateTime time = start;
            while (time.CompareTo(end) <= 0)
            {
                cnt++;
                time = IncrementByInterval(time);
            }
            return cnt;
        }
        public DateTime GetInterval(DateTime start, int n)
        {
            DateTime time = start;
            for (int i = 0; i < n; i++)
                time = IncrementByInterval(time);
            return time;
        }
        public DateTime GetIntervalFromRecent(DateTime end, int n)
        {
            DateTime time = end;
            for (int i = 0; i < n; i++)
                time = DecrementByInterval(time);
            return time;
        }
        public DateTime IncrementByInterval(DateTime time)
        {
            switch (Interval)
            {
                case DateInterval.Monthly:
                    return time.AddMonths(Count);
                case DateInterval.Weekly:
                    return time.AddDays(Count * 7);
                case DateInterval.Daily:
                    return time.AddDays(Count);
                case DateInterval.Hourly:
                default:
                    return time.AddHours(Count);
            }
        }
        public DateTime DecrementByInterval(DateTime time)
        {
            switch (Interval)
            {
                case DateInterval.Monthly:
                    return time.AddMonths(-Count);
                case DateInterval.Weekly:
                    return time.AddDays(-Count * 7);
                case DateInterval.Daily:
                    return time.AddDays(-Count);
                case DateInterval.Hourly:
                default:
                    return time.AddHours(-Count);
            }
        }
    }
}
