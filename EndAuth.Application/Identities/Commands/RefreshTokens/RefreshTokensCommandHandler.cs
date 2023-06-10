using EndAuth.Domain.Entities;
using EndAuth.Shared.Dtos.Identites;
using EndAuth.Shared.Identities.Commands.Refresh;

namespace EndAuth.Application.Identities.Commands.RefreshTokens;
public class RefreshTokensCommandHandler : IRequestHandler<RefreshTokensCommand, AuthSuccessResponse>
{
    private readonly ITokensService<ApplicationUser> _jwtService;

    public RefreshTokensCommandHandler(ITokensService<ApplicationUser> jwtService)
    {
        _jwtService = jwtService;
    }
    public async Task<AuthSuccessResponse> Handle(RefreshTokensCommand request, CancellationToken cancellationToken)
    {
        (AccessToken accessToken, RefreshToken refreshToken) = await _jwtService.RefreshTokensAsync(request.AccessToken, request.RefreshToken, cancellationToken);
        return new AuthSuccessResponse(accessToken.Token, accessToken.Expires, refreshToken.Token, refreshToken.Expires);
    }
}
