using System.Net;

namespace EndAuth.Infrastructure.ExceptionsHandling.ExceptionHandlingStrategy;

public class ResourceAlreadyExistsExceptionHandlerStrategy : IExceptionHandlerStrategy
{
    public int Handle()
    {
        return (int)HttpStatusCode.Conflict;
    }
}
