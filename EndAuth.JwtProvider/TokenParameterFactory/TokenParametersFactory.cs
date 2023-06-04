using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EndAuth.JwtProvider.TokenParameterFactory;

public class TokenParametersFactory : ITokenParametersFactory
{
    private readonly IConfiguration _configuration;

    public TokenParametersFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public TokenValidationParameters Create()
    {
        return new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JwtSettings:Key"]!)),
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            ValidIssuer = _configuration["JwtSettings:Issuer"],
            ValidAudience = _configuration["JwtSettings:Audience"],
        };
    }
}
