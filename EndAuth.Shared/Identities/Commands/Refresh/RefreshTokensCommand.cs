using EndAuth.Shared.Dtos;

namespace EndAuth.Shared.Identities.Commands.Refresh;
public record RefreshTokensCommand(string ExpiredJWT, string RefreshToken) : IRequest<Result<TokensResponse>>;