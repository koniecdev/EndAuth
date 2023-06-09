using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EndAuth.Domain.Entities;
using EndAuth.Persistance.Contexts.IdentityDb;
using EndAuth.Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace EndAuth.Persistance;

public static class DependencyInjection{
    public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IdentityContext>(o => o.UseSqlServer(configuration.GetConnectionString("DefaultDatabase")));
        services.AddDefaultIdentity<ApplicationUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();
        services.AddScoped<IIdentityContext, IdentityContext>(); 
        return services;
    }
}