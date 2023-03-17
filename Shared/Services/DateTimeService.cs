using Shared.Services.Interfaces;

namespace Shared.Services;

public class DateTimeService : IDateTimeService
{
    private static readonly TimeZoneInfo _timeZone = InitTimezone();

    static TimeZoneInfo InitTimezone() => TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");

    public DateTime Now => TimeZoneInfo.ConvertTime(DateTime.Now, _timeZone);
}
