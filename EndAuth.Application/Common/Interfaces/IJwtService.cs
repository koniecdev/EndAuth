using EndAuth.Domain.Entities;

namespace EndAuth.Application.Common.Interfaces;

public interface IJwtService<TUser>
{
    Task<string> CreateTokenAsync(string email);
    Task<RefreshToken> CreateRefreshTokenAsync(string email);
    Task<(string, RefreshToken)> RefreshTokensAsync(string jwt, string refreshToken);
}