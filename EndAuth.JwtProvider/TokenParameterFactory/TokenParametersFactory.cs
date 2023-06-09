using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EndAuth.JwtProvider.TokenParameterFactory;

public static class TokenParametersFactory
{
    public static TokenValidationParameters Create(IConfiguration configuration)
    {
        return new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JwtSettings:Key"]!)),
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            ValidIssuer = configuration["JwtSettings:Issuer"],
            ValidAudience = configuration["JwtSettings:Audience"],
        };
    }

    public static TokenValidationParameters CreateWithoutLifetimeValidation(IConfiguration configuration)
    {
        TokenValidationParameters parameters = Create(configuration);
        parameters.ValidateLifetime = false;
        return parameters;
    }
}
