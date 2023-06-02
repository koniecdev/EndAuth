using EndAuth.Shared.Dtos;

namespace EndAuth.Shared.Identities.Commands.Refresh;
public record RefreshTokensCommand(string Jwt, string RefreshToken) : IRequest<Result<TokensResponse>>;