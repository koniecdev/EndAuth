using EndAuth.Domain;
using Microsoft.EntityFrameworkCore;

namespace EndAuth.Application.Common.Interfaces;

public interface IIdentityContext
{
    DbSet<ApplicationUser> ApplicationUsers { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}