using EndAuth.Persistance.Contexts.IdentityDb;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace UnitTests.Application.Common;

public class IdentityContextMockFactory : IDbContextMockFactory<IdentityContext>
{
    public Mock<IdentityContext> Create()
    {
        var options = new DbContextOptionsBuilder<IdentityContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

        var mock = new Mock<IdentityContext>(options) { CallBase = true };

        var context = mock.Object;

        context.Database.EnsureCreated();

        //Remember - Seed of database are also applied - keep tracking of int IDs

        var passwordHasher = new PasswordHasher<IdentityUser>();

        var user = new IdentityUser
        {
            UserName = "Default",
            NormalizedUserName = "DEFAULT",
            Email = "Default@example.com",
            NormalizedEmail = "DEFAULT@EXAMPLE.COM"
        };

        user.PasswordHash = passwordHasher.HashPassword(user, "Default123$");

        context.Users.Add(user);

        context.SaveChanges();

        return mock is null ? throw new Exception("Could not create db mock") : mock;
    }

    public void Destroy(IdentityContext context)
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }
}