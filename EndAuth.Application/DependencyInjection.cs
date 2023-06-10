using EndAuth.Application.Common.Behaviours;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EndAuth.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
        .AddOpenBehavior(typeof(PerformanceBehaviour<,>))
        .AddOpenBehavior(typeof(ValidationBehaviour<,>)));
        return services;
    }
}