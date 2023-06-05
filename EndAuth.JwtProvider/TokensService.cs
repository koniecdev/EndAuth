using EndAuth.Application.Common.Exceptions;
using EndAuth.Application.Common.Interfaces;
using EndAuth.Domain.Entities;
using EndAuth.JwtProvider.AccessTokenServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EndAuth.JwtProvider.Services;
public class TokensService<TUser> : ITokensService<TUser> where TUser : IdentityUser
{
    private readonly UserManager<TUser> _userManager;
    private readonly IIdentityContext _db;
    private readonly IAccessTokenService<TUser> _accessTokenService;
    private readonly IRefreshTokenService<TUser> _refreshTokenService;
    
    public TokensService(
        UserManager<TUser> userManager,
        IIdentityContext context,
        IAccessTokenService<TUser> accessTokenService,
        IRefreshTokenService<TUser> refreshTokenService)
    {
        _userManager = userManager;
        _db = context;
        _accessTokenService = accessTokenService;
        _refreshTokenService = refreshTokenService;
    }

    public async Task<(AccessToken, RefreshToken)> CreateTokensAsync(string email, CancellationToken cancellationToken)
    {
        SigningCredentials signingCredentials = _accessTokenService.GetSigningCredentials();
        ICollection<Claim> claims = await _accessTokenService.GetClaimsAsync(email);
        JwtSecurityToken tokenOptions = _accessTokenService.GenerateTokenOptions(signingCredentials, claims);
        string newJwt = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        AccessToken newAccessToken = new()
        {
            Token = newJwt,
            Expires = tokenOptions.ValidTo
        };
        RefreshToken newRefreshToken = await _refreshTokenService.CreateRefreshTokenAsync(email, claims.Single(m=>m.Type == JwtRegisteredClaimNames.Jti).Value, cancellationToken);
        return (newAccessToken, newRefreshToken);
    }

    public async Task<(AccessToken, RefreshToken)> RefreshTokensAsync(string jwt, string refreshToken, CancellationToken cancellationToken)
    {
        string userId = _accessTokenService.ValidateJwt(jwt, out ClaimsPrincipal principal);

        var user = await _userManager.FindByIdAsync(userId);
        var refreshTokenFromDb = await _db.RefreshTokens.SingleOrDefaultAsync(m => m.Token.Equals(refreshToken), cancellationToken)
            ?? throw new InvalidRefreshTokenException(refreshToken);

        await _refreshTokenService.ValidateRefreshToken(jwt, principal, refreshTokenFromDb, cancellationToken);

        //At this point both Refresh token is valid, and token is valid, so we can refresh both

        return await CreateTokensAsync(user.Email, cancellationToken);
    }
}
