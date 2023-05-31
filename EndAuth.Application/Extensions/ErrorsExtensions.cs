using EndAuth.Application.Common.Exceptions;
using EndAuth.Shared.Dtos;
using System.Net;

namespace EndAuth.Application.Extensions;
public static class ErrorsExtensions
{
    public static ErrorResponse MapToResponse(this Exception exception)
    {
        int code = exception switch
        {
            ResourceNotFoundException => (int)HttpStatusCode.NotFound,
            _ => (int)HttpStatusCode.BadRequest
        };
        return new ErrorResponse(code, exception.Message.Replace("Severity: Error", ""));
    }
}
