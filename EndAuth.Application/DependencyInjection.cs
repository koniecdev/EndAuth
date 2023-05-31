using EndAuth.Application.Common.Behaviours;
using EndAuth.Application.Extensions;
using EndAuth.Shared.Identities.Commands.Login;
using EndAuth.Shared.Identities.Commands.Register;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EndAuth.Shared;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
        .AddOpenBehavior(typeof(PerformanceBehaviour<,>))
        .AddValidation<RegisterUserCommand, string>()
        .AddValidation<LoginUserCommand, string>());
        return services;
    }
}