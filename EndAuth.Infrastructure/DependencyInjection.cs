using EndAuth.Application.Common.Interfaces;
using EndAuth.Application.Common.Interfaces.Factories;
using EndAuth.Infrastructure.Factories;
using EndAuth.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EndAuth.Shared;

public static class DependencyInjection{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IDateTimeService, DateTimeService>();
        services.AddScoped<ISmtpClientGmailFactory, SmtpClientGmailFactory>();
        services.AddScoped<IEmailService, EmailService>();
        return services;
    }
}