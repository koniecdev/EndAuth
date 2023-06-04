using EndAuth.Application.Common.Interfaces;

namespace EndAuth.Infrastructure.Services;

public class DateTimeService : IDateTimeService
{
    public DateTime Now => DateTime.Now;
}
