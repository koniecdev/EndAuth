using Microsoft.Extensions.DependencyInjection;

namespace EndAuth.Shared;

public static class DependencyInjection{
    public static IServiceCollection AddShared(this IServiceCollection services)
    {
        return services;
    }
}