using Microsoft.Extensions.DependencyInjection;
using EndAuth.Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Http;
using EndAuth.JwtProvider.Services;

namespace EndAuth.JwtProvider;

public static class DependencyInjection
{
    public static IServiceCollection AddJwtProvider(this IServiceCollection services)
    {
        services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.TryAddScoped<ITokenParametersFactory, TokenParametersFactory>();
        services.TryAddScoped(typeof(IJwtService<>), typeof(JwtService<>));
        return services;
    }
}