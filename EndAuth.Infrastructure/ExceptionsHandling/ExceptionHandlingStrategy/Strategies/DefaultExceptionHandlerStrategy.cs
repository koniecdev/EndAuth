using System.Net;

namespace EndAuth.Infrastructure.ExceptionsHandling.ExceptionHandlingStrategy;

internal sealed class DefaultExceptionHandlerStrategy : IExceptionHandlerStrategy
{
    public int Handle()
    {
        return (int)HttpStatusCode.BadRequest;
    }
}
