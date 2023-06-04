using EndAuth.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EndAuth.Persistance.Contexts.IdentityDb;

public static class SeedAdmin
{
    public static void Initialize(ModelBuilder builder)
    {
        var superAdminRoleId = Guid.NewGuid().ToString();
        builder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Name = "SuperAdmin",
            NormalizedName = "SUPERADMIN",
            Id = superAdminRoleId,
            ConcurrencyStamp = superAdminRoleId
        });

        var appUserId = Guid.NewGuid().ToString();
        var appUser = new ApplicationUser
        {
            Id = appUserId,
            Email = "DefaultAdmin@default.com",
            NormalizedEmail = "DEFAULTADMIN@DEFAULT.COM",
            EmailConfirmed = true,
            UserName = "DefaultAdmin",
            NormalizedUserName = "DEFAULTADMIN"
        };
        PasswordHasher<ApplicationUser> ph = new();
        appUser.PasswordHash = ph.HashPassword(appUser, "Default123$");

        builder.Entity<ApplicationUser>().HasData(appUser);

        builder.Entity<IdentityUserRole<string>>().HasData(new
        {
            RoleId = superAdminRoleId,
            UserId = appUserId
        });
    }
}