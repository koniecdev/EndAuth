﻿using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EndAuth.Shared;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        return services;
    }
}