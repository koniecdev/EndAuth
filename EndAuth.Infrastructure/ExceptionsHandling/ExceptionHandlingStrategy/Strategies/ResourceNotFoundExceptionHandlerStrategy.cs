using System.Net;

namespace EndAuth.Infrastructure.ExceptionsHandling.ExceptionHandlingStrategy;

public class ResourceNotFoundExceptionHandlerStrategy : IExceptionHandlerStrategy
{
    public int Handle()
    {
        return (int)HttpStatusCode.NotFound;
    }
}
