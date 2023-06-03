using EndAuth.Application.Common.Exceptions;
using EndAuth.Application.Common.Interfaces;
using EndAuth.Domain.Entities;
using EndAuth.JwtProvider.AccessTokenServices;
using EndAuth.JwtProvider.TokenParameterFactory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EndAuth.JwtProvider.Services;
public class TokensService<TUser> : ITokensService<TUser> where TUser : IdentityUser
{
    private readonly UserManager<TUser> _userManager;
    private readonly ITokenParametersFactory _tokenParametersFactory;
    private readonly IIdentityContext _db;
    private readonly IAccessTokenService<TUser> _accessTokenService;
    private readonly TokenValidationParameters _tokenValidationParameters;

    public TokensService(
        UserManager<TUser> userManager,
        ITokenParametersFactory tokenParametersFactory,
        IIdentityContext context,
        IAccessTokenService<TUser> accessTokenService)
    {
        _userManager = userManager;
        _tokenParametersFactory = tokenParametersFactory;
        _db = context;
        _accessTokenService = accessTokenService;
        _tokenValidationParameters = _tokenParametersFactory.Create();
    }

    public async Task<string> CreateTokenAsync(string email)
    {
        SigningCredentials signingCredentials = _accessTokenService.GetSigningCredentials();
        ICollection<Claim> claims = await _accessTokenService.GetClaimsAsync(email);
        JwtSecurityToken tokenOptions = _accessTokenService.GenerateTokenOptions(signingCredentials, claims);
        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    public async Task<RefreshToken> CreateRefreshTokenAsync(string email, CancellationToken cancellationToken)
    {
        TUser user = await _userManager.FindByEmailAsync(email);
        string newRefreshTokenId = Guid.NewGuid().ToString();
        RefreshToken token = new()
        {
            CreationDate = DateTime.Now,
            Expires = DateTime.Now.AddMinutes(5),
            ApplicationUserId = user.Id,
            Invalidated = false,
            Used = false,
            Token = newRefreshTokenId
        };
        _db.RefreshTokens.Add(token);
        await _db.SaveChangesAsync(cancellationToken);
        return token;
    }

    public async Task<(string, RefreshToken)> RefreshTokensAsync(string jwt, string refreshToken, CancellationToken cancellationToken)
    {
        //First of all, lets validate JWT

        JwtSecurityTokenHandler tokenHandler = new();
        ClaimsPrincipal? principal = null;
        //bool isJwtExpired = false;
        SecurityToken? securityToken;
        try
        {
            _tokenValidationParameters.ValidateLifetime = false;
            principal = tokenHandler.ValidateToken(jwt, _tokenValidationParameters, out securityToken);
            _tokenValidationParameters.ValidateLifetime = true;
        }
        catch (SecurityTokenExpiredException)
        {
            //Our jwt is expired
            throw new SecurityTokenException("Please log in again!!");
        }
        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token!");
        }
        string? userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            throw new SecurityTokenException($"Missing claim: {ClaimTypes.Name}!");
        }

        var user = await _userManager.FindByIdAsync(userId);
        //var jwtExpirationDate = new DateTime(1970, 1, 1, 0, 0, 0)
        //    .AddTicks(long.Parse(principal.Claims.Single(m => m.Type == JwtRegisteredClaimNames.Exp)!.Value));

        var refreshTokenFromDb = await _db.RefreshTokens.SingleOrDefaultAsync(m => m.Token.Equals(refreshToken), cancellationToken)
            ?? throw new InvalidRefreshTokenException(refreshToken);
        if (refreshTokenFromDb.Expires < DateTime.Now || refreshTokenFromDb.Invalidated)
        {
            //Refresh token has expired, or has already been used, so user have to login again.
            throw new Exception("Please log in again");
        }

        //At this point both Refresh token is valid, and token is valid, so we can refresh both

        var newJwt = await CreateTokenAsync(user.Email);
        var newRefresh = await CreateRefreshTokenAsync(user.Email, cancellationToken);

        return (newJwt, newRefresh);
    }
}
