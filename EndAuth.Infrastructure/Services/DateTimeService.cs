using EndAuth.Application.Common.Interfaces;

namespace EndAuth.Infrastructure.Services;

public class DateTimeService : IDateTimeService
{
    public DateTimeOffset Now => DateTimeOffset.Now;
}
