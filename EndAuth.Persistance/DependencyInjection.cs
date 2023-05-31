using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EndAuth.Domain;
using EndAuth.Persistance.Contexts.IdentityDb;
using EndAuth.Application.Common.Interfaces;

namespace EndAuth.Persistance;

public static class DependencyInjection{
    public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IdentityContext>(o => o.UseSqlServer(configuration.GetConnectionString("DefaultDatabase")));
        services.AddDefaultIdentity<ApplicationUser>(o =>
        {
            o.User.RequireUniqueEmail = true;
        }).AddEntityFrameworkStores<IdentityContext>();
        services.AddScoped<IIdentityContext, IdentityContext>(); 
        return services;
    }
}