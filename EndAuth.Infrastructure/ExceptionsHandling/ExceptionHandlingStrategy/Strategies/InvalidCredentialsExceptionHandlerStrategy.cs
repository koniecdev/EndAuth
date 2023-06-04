using System.Net;

namespace EndAuth.Infrastructure.ExceptionsHandling.ExceptionHandlingStrategy;

public class InvalidCredentialsExceptionHandlerStrategy : IExceptionHandlerStrategy
{
    public int Handle()
    {
        return (int)HttpStatusCode.Unauthorized;
    }
}
