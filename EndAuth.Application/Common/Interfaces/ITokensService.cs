using EndAuth.Domain.Entities;

namespace EndAuth.Application.Common.Interfaces;

public interface ITokensService<TUser>
{
    Task<(AccessToken, RefreshToken)> CreateTokensAsync(string email, CancellationToken cancellationToken);
    Task<(AccessToken, RefreshToken)> RefreshTokensAsync(string jwt, string refreshToken, CancellationToken cancellationToken);
}