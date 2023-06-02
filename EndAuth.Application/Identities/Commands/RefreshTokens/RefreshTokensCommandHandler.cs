using EndAuth.Application.Common.Interfaces;
using EndAuth.Domain.Entities;
using EndAuth.Shared.Dtos;
using EndAuth.Shared.Identities.Commands.Refresh;

namespace EndAuth.Application.Identities.Commands.Login;
public class RefreshTokensCommandHandler : IRequestHandler<RefreshTokensCommand, Result<TokensResponse>>
{
    private readonly IJwtService<ApplicationUser> _jwtService;

    public RefreshTokensCommandHandler(IJwtService<ApplicationUser> jwtService)
    {
        _jwtService = jwtService;
    }
    public async Task<Result<TokensResponse>> Handle(RefreshTokensCommand request, CancellationToken cancellationToken)
    {
        (string jwt, RefreshToken refreshToken) = await _jwtService.RefreshTokensAsync(request.Jwt, request.RefreshToken);
        return new TokensResponse(jwt, refreshToken.Token, refreshToken.Expires);
    }
}
