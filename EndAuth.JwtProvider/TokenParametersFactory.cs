﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EndAuth.JwtProvider;

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
            ValidIssuer = _configuration["JwtSettings:Issuer"],
            ValidAudience = _configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]!)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };
    }
}
