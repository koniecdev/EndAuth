using EndAuth.Shared.Dtos.Identites;

namespace EndAuth.Shared.Identities.Commands.Refresh;
public record RefreshTokensCommand(string AccessToken, string RefreshToken) : IRequest<AuthSuccessResponse>;