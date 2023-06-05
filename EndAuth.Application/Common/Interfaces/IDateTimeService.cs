namespace EndAuth.Application.Common.Interfaces;

public interface IDateTimeService
{
    DateTimeOffset Now { get; }
}