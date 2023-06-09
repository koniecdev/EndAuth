using EndAuth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EndAuth.Application.Common.Interfaces;

public interface IIdentityContext
{
    DbSet<ApplicationUser> ApplicationUsers { get; }
    DbSet<RefreshToken> RefreshTokens { get; }
    DbSet<PasswordResetToken> PasswordResetTokens { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}