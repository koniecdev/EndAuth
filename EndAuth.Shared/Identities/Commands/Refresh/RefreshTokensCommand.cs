using EndAuth.Shared.Dtos;

namespace EndAuth.Shared.Identities.Commands.Refresh;
public record RefreshTokensCommand(string AccessToken, string RefreshToken) : IRequest<Result<AuthSuccessResponse>>;