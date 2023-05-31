using EndAuth.Application.Common.Exceptions;
using EndAuth.Shared.Dtos;
using System.Net;

namespace EndAuth.Application.Common.Mappings.Errors;
public static class ErrorsExtensions
{
    public static ErrorResponse MapToResponse(this Exception exception)
    {
        int code = exception switch
        {
            IdentityResultFailedException => (int)HttpStatusCode.BadRequest,
            _ => (int)HttpStatusCode.BadRequest
        };
        return new ErrorResponse(code, exception.Message);
    }
}
