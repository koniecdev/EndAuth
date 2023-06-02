using Microsoft.AspNetCore.Identity;

namespace EndAuth.Domain.Entities;
public class ApplicationUser : IdentityUser
{
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = null!;
}