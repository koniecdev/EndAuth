using EndAuth.Domain.Entities;

namespace EndAuth.Application.Common.Interfaces;

public interface ITokensService<TUser>
{
    Task<(string, RefreshToken)> CreateTokensAsync(string email, CancellationToken cancellationToken);
    Task<(string, RefreshToken)> RefreshTokensAsync(string jwt, string refreshToken, CancellationToken cancellationToken);
}