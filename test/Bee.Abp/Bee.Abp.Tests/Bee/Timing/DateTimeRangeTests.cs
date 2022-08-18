namespace Bee.Timing;

public class DateTimeRangeTests
{
    [Fact]
    public void DateTimeRangeTest_Ctor()
    {
        var range = new DateTimeRange();
        range.StartTime.ShouldBe(DateTime.MinValue);
        range.EndTime.ShouldBe(DateTime.MaxValue);

        var now = new DateTime(2021, 4, 22, 14, 8, 22);
        range = new DateTimeRange(now.Date.AddDays(-1), now.Date.AddDays(2));
        range.StartTime.Day.ShouldBe(21);
        range.EndTime.Day.ShouldBe(24);
    }

    [Fact]
    public void DateTimeRangeTest_Properties()
    {
        DateTimeRange.Today.StartTime.ShouldBeGreaterThan(DateTimeRange.Yesterday.EndTime);
        DateTimeRange.Today.EndTime.ShouldBeLessThan(DateTimeRange.Tomorrow.StartTime);

        DateTimeRange.ThisMonth.StartTime.Day.ShouldBe(1);
        DateTimeRange.LastMonth.StartTime.Day.ShouldBe(1);
        DateTimeRange.NextMonth.StartTime.Day.ShouldBe(1);

        DateTimeRange.ThisMonth.StartTime.ShouldBeGreaterThan(DateTimeRange.LastMonth.EndTime);
        DateTimeRange.ThisMonth.EndTime.ShouldBeLessThan(DateTimeRange.NextMonth.EndTime);

        DateTimeRange.ThisYear.StartTime.Month.ShouldBe(1);
        DateTimeRange.ThisYear.StartTime.Day.ShouldBe(1);

        DateTimeRange.ThisYear.StartTime.ShouldBeGreaterThan(DateTimeRange.LastYear.EndTime);
        DateTimeRange.ThisYear.EndTime.ShouldBeLessThan(DateTimeRange.NextYear.StartTime);

        DateTimeRange.Last7DaysExceptToday.EndTime.ShouldBeLessThan(DateTimeRange.Today.StartTime);
        DateTimeRange.Last30DaysExceptToday.EndTime.ShouldBeLessThan(DateTimeRange.Today.StartTime);
    }

    [Fact]
    public void DateTimeRangeTest_ToString()
    {
        var range = new DateTimeRange();
        range.ToString().ShouldBe($"[{DateTime.MinValue} - {DateTime.MaxValue}]");
    }
}