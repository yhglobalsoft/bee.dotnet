﻿namespace Bee.Timing;

/// <summary>
/// 表示一个时间范围
/// </summary>
[Serializable]
public class DateTimeRange
{
    /// <summary>
    /// 初始化一个<see cref="DateTimeRange"/>类型的新实例
    /// </summary>
    public DateTimeRange()
        : this(DateTime.MinValue, DateTime.MaxValue)
    { }

    /// <summary>
    /// 初始化一个<see cref="DateTimeRange"/>类型的新实例
    /// </summary>
    public DateTimeRange(DateTime startTime, DateTime endTime)
    {
        StartTime = startTime;
        EndTime = endTime;
    }

    /// <summary>
    /// 获取或设置 起始时间
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// 获取或设置 结束时间
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    /// 获取 昨天的时间范围
    /// </summary>
    public static DateTimeRange Yesterday
    {
        get
        {
            var now = DateTime.Now;
            return new DateTimeRange(now.Date.AddDays(-1), now.Date.AddMilliseconds(-1));
        }
    }

    /// <summary>
    /// 获取 今天的时间范围
    /// </summary>
    public static DateTimeRange Today
    {
        get
        {
            var now = DateTime.Now;
            return new DateTimeRange(now.Date.Date, now.Date.AddDays(1).AddMilliseconds(-1));
        }
    }

    /// <summary>
    /// 获取 明天的时间范围
    /// </summary>
    public static DateTimeRange Tomorrow
    {
        get
        {
            var now = DateTime.Now;
            return new DateTimeRange(now.Date.AddDays(1), now.Date.AddDays(2).AddMilliseconds(-1));
        }
    }

    /// <summary>
    /// 获取 上周的时间范围
    /// </summary>
    public static DateTimeRange LastWeek
    {
        get
        {
            var now = DateTime.Now;
            DayOfWeek[] weeks =
            {
                DayOfWeek.Sunday,
                DayOfWeek.Monday,
                DayOfWeek.Tuesday,
                DayOfWeek.Wednesday,
                DayOfWeek.Thursday,
                DayOfWeek.Friday,
                DayOfWeek.Saturday
            };
            var index = Array.IndexOf(weeks, now.DayOfWeek);
            return new DateTimeRange(now.Date.AddDays(-index - 7), now.Date.AddDays(-index).AddMilliseconds(-1));
        }
    }

    /// <summary>
    /// 获取 本周的时间范围
    /// </summary>
    public static DateTimeRange ThisWeek
    {
        get
        {
            var now = DateTime.Now;
            DayOfWeek[] weeks =
            {
                DayOfWeek.Sunday,
                DayOfWeek.Monday,
                DayOfWeek.Tuesday,
                DayOfWeek.Wednesday,
                DayOfWeek.Thursday,
                DayOfWeek.Friday,
                DayOfWeek.Saturday
            };
            var index = Array.IndexOf(weeks, now.DayOfWeek);
            return new DateTimeRange(now.Date.AddDays(-index), now.Date.AddDays(7 - index).AddMilliseconds(-1));
        }
    }

    /// <summary>
    /// 获取 下周的时间范围
    /// </summary>
    public static DateTimeRange NextWeek
    {
        get
        {
            var now = DateTime.Now;
            DayOfWeek[] weeks =
            {
                DayOfWeek.Sunday,
                DayOfWeek.Monday,
                DayOfWeek.Tuesday,
                DayOfWeek.Wednesday,
                DayOfWeek.Thursday,
                DayOfWeek.Friday,
                DayOfWeek.Saturday
            };
            var index = Array.IndexOf(weeks, now.DayOfWeek);
            return new DateTimeRange(now.Date.AddDays(-index + 7), now.Date.AddDays(14 - index).AddMilliseconds(-1));
        }
    }

    /// <summary>
    /// 获取 上个月的时间范围
    /// </summary>
    public static DateTimeRange LastMonth
    {
        get
        {
            var now = DateTime.Now;
            var startTime = now.Date.AddDays(-now.Day + 1).AddMonths(-1);
            var endTime = startTime.AddMonths(1).AddMilliseconds(-1);
            return new DateTimeRange(startTime, endTime);
        }
    }

    /// <summary>
    /// 获取 本月的时间范围
    /// </summary>
    public static DateTimeRange ThisMonth
    {
        get
        {
            var now = DateTime.Now;
            var startTime = now.Date.AddDays(-now.Day + 1);
            var endTime = startTime.AddMonths(1).AddMilliseconds(-1);
            return new DateTimeRange(startTime, endTime);
        }
    }

    /// <summary>
    /// 获取 下个月的时间范围
    /// </summary>
    public static DateTimeRange NextMonth
    {
        get
        {
            var now = DateTime.Now;
            var startTime = now.Date.AddDays(-now.Day + 1).AddMonths(1);
            var endTime = startTime.AddMonths(1).AddMilliseconds(-1);
            return new DateTimeRange(startTime, endTime);
        }
    }

    /// <summary>
    /// 获取 上一年的时间范围
    /// </summary>
    public static DateTimeRange LastYear
    {
        get
        {
            var now = DateTime.Now;
            return new DateTimeRange(new DateTime(now.Year - 1, 1, 1), new DateTime(now.Year, 1, 1).AddMilliseconds(-1));
        }
    }

    /// <summary>
    /// 获取 本年的时间范围
    /// </summary>
    public static DateTimeRange ThisYear
    {
        get
        {
            var now = DateTime.Now;
            return new DateTimeRange(new DateTime(now.Year, 1, 1), new DateTime(now.Year + 1, 1, 1).AddMilliseconds(-1));
        }
    }

    /// <summary>
    /// 获取 下一年的时间范围
    /// </summary>
    public static DateTimeRange NextYear
    {
        get
        {
            var now = DateTime.Now;
            return new DateTimeRange(new DateTime(now.Year + 1, 1, 1), new DateTime(now.Year + 2, 1, 1).AddMilliseconds(-1));
        }
    }

    /// <summary>
    /// 获取 相对当前时间过去30天的时间范围
    /// </summary>
    public static DateTimeRange Last30Days
    {
        get
        {
            var now = DateTime.Now;
            return new DateTimeRange(now.AddDays(-30), now);
        }
    }

    /// <summary>
    /// 获取 截止到昨天的最近30天的天数范围
    /// </summary>
    public static DateTimeRange Last30DaysExceptToday
    {
        get
        {
            var now = DateTime.Now;
            return new DateTimeRange(now.Date.AddDays(-30), now.Date.AddMilliseconds(-1));
        }
    }

    /// <summary>
    /// 获取 相对当前时间过去7天的时间范围
    /// </summary>
    public static DateTimeRange Last7Days
    {
        get
        {
            var now = DateTime.Now;
            return new DateTimeRange(now.AddDays(-7), now);
        }
    }

    /// <summary>
    /// 获取 截止到昨天的最近7天的天数范围
    /// </summary>
    public static DateTimeRange Last7DaysExceptToday
    {
        get
        {
            var now = DateTime.Now;
            return new DateTimeRange(now.Date.AddDays(-7), now.Date.AddMilliseconds(-1));
        }
    }

    /// <summary>
    /// 返回表示当前 <see cref="T:System.Object"/> 的 <see cref="T:System.String"/>。
    /// </summary>
    /// <returns>
    /// <see cref="T:System.String"/>，表示当前的 <see cref="T:System.Object"/>。
    /// </returns>
    public override string ToString()
    {
        return $"[{StartTime} - {EndTime}]";
    }
}