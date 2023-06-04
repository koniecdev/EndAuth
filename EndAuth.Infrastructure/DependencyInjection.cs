using EndAuth.Application.Common.Interfaces;
using EndAuth.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EndAuth.Shared;

public static class DependencyInjection{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IDateTimeService, DateTimeService>();
        return services;
    }
}