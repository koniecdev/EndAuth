using EndAuth.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EndAuth.Persistance.Contexts.IdentityDb;

public static class SeedAdmin
{
    public static void Initialize(ModelBuilder builder)
    {
        var superAdminRoleId = "978e4744-ffe0-4b3a-bdcd-e805ecb64359";
        builder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Name = "SuperAdmin",
            NormalizedName = "SUPERADMIN",
            Id = superAdminRoleId,
            ConcurrencyStamp = superAdminRoleId
        });

        var appUserId = "1362245b-0494-47ea-abae-912890c0cb46";
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