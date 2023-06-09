using EndAuth.Application.Common.Exceptions;
using EndAuth.Infrastructure.ExceptionsHandling.ExceptionHandlingStrategy;
using Microsoft.AspNetCore.Http;

namespace EndAuth.Infrastructure.ExceptionsHandling;

internal sealed class ExceptionsHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionsHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(Exception ex)
        {
            await context.Response.WriteAsJsonAsync(HandleException(ex));
        }
    }

    private static FailureResponse HandleException(Exception ex)
    {
        IExceptionHandlerStrategy exceptionStrategy = ex switch
        {
            InvalidCredentialsException => new InvalidCredentialsExceptionHandlerStrategy(),
            ResourceAlreadyExistsException => new ResourceAlreadyExistsExceptionHandlerStrategy(),
            ResourceNotFoundException => new ResourceNotFoundExceptionHandlerStrategy(),
            _ => new DefaultExceptionHandlerStrategy()
        };
        ExceptionHandler exceptionHandler = new(exceptionStrategy);
        return new(exceptionHandler.Handle(), ex.Message);
    }
}
