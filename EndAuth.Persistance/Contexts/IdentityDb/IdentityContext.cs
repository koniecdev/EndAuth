using EndAuth.Application.Common.Interfaces;
using EndAuth.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EndAuth.Persistance.Contexts.IdentityDb;
public class IdentityContext : IdentityDbContext, IIdentityContext
{
    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
    {

    }

    public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }
}
