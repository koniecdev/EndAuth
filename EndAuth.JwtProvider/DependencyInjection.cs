using Microsoft.Extensions.DependencyInjection;
using EndAuth.Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Http;
using EndAuth.JwtProvider.Services;
using EndAuth.JwtProvider.TokenParameterFactory;
using EndAuth.JwtProvider.AccessTokenServices;

namespace EndAuth.JwtProvider;

public static class DependencyInjection
{
    public static IServiceCollection AddJwtProvider(this IServiceCollection services)
    {
        services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.TryAddScoped<ITokenParametersFactory, TokenParametersFactory>();
        services.TryAddScoped(typeof(IAccessTokenService<>), typeof(AccessTokenService<>));
        services.TryAddScoped(typeof(ITokensService<>), typeof(TokensService<>));
        return services;
    }
}