using EndAuth.Infrastructure.ExceptionsHandling;
using Microsoft.AspNetCore.Builder;

namespace EndAuth.Infrastructure.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionsHandling(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionsHandlingMiddleware>();
    }
}
