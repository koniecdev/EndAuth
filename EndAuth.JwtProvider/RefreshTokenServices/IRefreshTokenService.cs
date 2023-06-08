using EndAuth.Domain.Entities;
using System.Security.Claims;

namespace EndAuth.JwtProvider.AccessTokenServices;
public interface IRefreshTokenService<TUser>
{
    Task<RefreshToken> CreateRefreshTokenAsync(string email, string jwtId, CancellationToken cancellationToken);
    Task ValidateRefreshToken(string jwt, ClaimsPrincipal principal, RefreshToken refreshToken, CancellationToken cancellationToken);
}