using EndAuth.Application.Common.Interfaces;
using EndAuth.Domain.Entities;
using EndAuth.Shared.Dtos;
using EndAuth.Shared.Identities.Commands.Refresh;

namespace EndAuth.Application.Identities.Commands.Login;
public class RefreshTokensCommandHandler : IRequestHandler<RefreshTokensCommand, Result<AuthSuccessResponse>>
{
    private readonly IJwtService<ApplicationUser> _jwtService;

    public RefreshTokensCommandHandler(IJwtService<ApplicationUser> jwtService)
    {
        _jwtService = jwtService;
    }
    public async Task<Result<AuthSuccessResponse>> Handle(RefreshTokensCommand request, CancellationToken cancellationToken)
    {
        (string jwt, RefreshToken refreshToken) = await _jwtService.RefreshTokensAsync(request.AccessToken, request.RefreshToken, cancellationToken);
        return new AuthSuccessResponse(jwt, refreshToken.Token);
    }
}
