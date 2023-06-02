using EndAuth.Application.Common.Interfaces;
using EndAuth.Domain.Entities;
using LanguageExt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EndAuth.JwtProvider.Services;
public class JwtService<TUser> : IJwtService<TUser> where TUser : IdentityUser
{
    private readonly UserManager<TUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITokenParametersFactory _tokenParametersFactory;
    private readonly IIdentityContext _db;
    private readonly TokenValidationParameters _tokenValidationParameters;

    public JwtService(
        UserManager<TUser> userManager,
        IConfiguration configuration,
        IHttpContextAccessor httpContextAccessor,
        ITokenParametersFactory tokenParametersFactory,
        IIdentityContext context)
    {
        _userManager = userManager;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
        _tokenParametersFactory = tokenParametersFactory;
        _db = context;
        _tokenValidationParameters = _tokenParametersFactory.Create();
    }

    public async Task<string> CreateTokenAsync(string email)
    {
        var signingCredentials = GetSigningCredentials();
        var claims = await GetClaims(email);
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    private SigningCredentials GetSigningCredentials()
    {
        var jwtConfig = _configuration.GetSection("JwtSettings");
        var key = Encoding.UTF8.GetBytes(jwtConfig["Key"]);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private async Task<List<Claim>> GetClaims(string email)
    {
        if (_httpContextAccessor.HttpContext == null)
        {
            throw new Exception();
        }
        if (!string.IsNullOrWhiteSpace(email))
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
        return new();
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var tokenOptions = new JwtSecurityToken
        (
        issuer: jwtSettings["Issuer"],
        audience: jwtSettings["Audience"],
        claims: claims,
        expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpiresIn"])),
        signingCredentials: signingCredentials
        );
        return tokenOptions;
    }


    public async Task<RefreshToken> CreateRefreshTokenAsync(string email)
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
        await _db.SaveChangesAsync();
        return token;
    }

    public async Task<(string, RefreshToken)> RefreshTokensAsync(string jwt, string refreshToken)
    {
        var refreshTokenFromDb = await _db.RefreshTokens.SingleAsync(m => m.Token.Equals(refreshToken));
        if(refreshTokenFromDb.Expires < DateTime.Now || refreshTokenFromDb.Invalidated)
        {
            //Refresh token has expired, user have to login again.
            refreshTokenFromDb.Invalidated = true;
            await _db.SaveChangesAsync();
            throw new Exception("Please log in again");
        }
        //Refresh token is still valid, we can validate jwt.

        JwtSecurityTokenHandler tokenHandler = new();
        ClaimsPrincipal? principal = null;
        SecurityToken? securityToken;
        try
        {
             principal = tokenHandler.ValidateToken(jwt, _tokenValidationParameters, out securityToken);

        }
        catch(SecurityTokenExpiredException ex)
        {
            throw new SecurityTokenException("Please log in again!!");
        }
        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
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

        //At this point both Refresh token is valid, and token is valid, so we can refresh both

        var newJwt = await CreateTokenAsync(user.Email);
        var newRefresh = await CreateRefreshTokenAsync(user.Email);

        return (newJwt, newRefresh);
    }
}
