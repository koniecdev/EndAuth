using EndAuth.Domain.Entities;

namespace EndAuth.Application.Common.Interfaces;

public interface ITokensService<TUser>
{
    Task<string> CreateTokenAsync(string email);
    Task<RefreshToken> CreateRefreshTokenAsync(string email, CancellationToken cancellationToken);
    Task<(string, RefreshToken)> RefreshTokensAsync(string jwt, string refreshToken, CancellationToken cancellationToken);
}