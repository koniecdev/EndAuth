﻿using Microsoft.Extensions.DependencyInjection;
using EndAuth.Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Http;
using EndAuth.JwtProvider.Services;

namespace EndAuth.Shared;

public static class DependencyInjection
{
    public static IServiceCollection AddJwtProvider(this IServiceCollection services)
    {
        services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IJwtService, JwtService>();
        return services;
    }
}