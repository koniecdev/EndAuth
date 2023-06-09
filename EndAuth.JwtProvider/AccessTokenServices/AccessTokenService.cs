using EndAuth.Application.Common.Interfaces;
using EndAuth.JwtProvider.TokenParameterFactory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EndAuth.JwtProvider.AccessTokenServices;
internal sealed class AccessTokenService<TUser> : IAccessTokenService<TUser> where TUser : IdentityUser
{
    private readonly UserManager<TUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IDateTimeService _dateTimeService;
    private readonly TokenValidationParameters _tokenValidationParameters;

    public AccessTokenService(
        UserManager<TUser> userManager,
        IConfiguration configuration,
        IHttpContextAccessor httpContextAccessor,
        IDateTimeService dateTimeService)
    {
        _userManager = userManager;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
        _dateTimeService = dateTimeService;
        _tokenValidationParameters = TokenParametersFactory.CreateWithoutLifetimeValidation(configuration);
    }

    public SigningCredentials GetSigningCredentials()
    {
        var jwtConfig = _configuration.GetSection("JwtSettings");
        var key = Encoding.ASCII.GetBytes(jwtConfig["Key"]);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256Signature);
    }

    public async Task<ICollection<Claim>> GetClaimsAsync(string email)
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
        return new List<Claim>();
    }

    public JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, ICollection<Claim> claims)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var tokenOptions = new JwtSecurityToken
        (
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: _dateTimeService.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpiresIn"])).DateTime,
            signingCredentials: signingCredentials
        );
        return tokenOptions;
    }

    public string ValidateJwt(string jwt, out ClaimsPrincipal claimsPrincipal)
    {
        JwtSecurityTokenHandler tokenHandler = new();
        SecurityToken? securityToken;
        try
        {
            _tokenValidationParameters.ValidateLifetime = false;
            claimsPrincipal = tokenHandler.ValidateToken(jwt, _tokenValidationParameters, out securityToken);
        }
        finally
        {
            _tokenValidationParameters.ValidateLifetime = true;
        }


        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("JWT is invalid");
        }

        string? userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            throw new SecurityTokenException($"Missing claim: {ClaimTypes.Name}!");
        }
        return userId;
    }
}
