using System.Net;

namespace EndAuth.Infrastructure.ExceptionsHandling.ExceptionHandlingStrategy;

public class DefaultExceptionHandlerStrategy : IExceptionHandlerStrategy
{
    public int Handle()
    {
        return (int)HttpStatusCode.BadRequest;
    }
}
