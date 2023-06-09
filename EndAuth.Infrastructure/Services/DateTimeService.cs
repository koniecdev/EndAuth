using EndAuth.Application.Common.Interfaces;

namespace EndAuth.Infrastructure.Services;

internal sealed class DateTimeService : IDateTimeService
{
    public DateTimeOffset Now => DateTimeOffset.Now;
}
