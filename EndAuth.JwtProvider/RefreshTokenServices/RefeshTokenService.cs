﻿using EndAuth.Application.Common.Exceptions;
using EndAuth.Application.Common.Interfaces;
using EndAuth.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EndAuth.JwtProvider.AccessTokenServices;
public class RefreshTokenService<TUser> : IRefreshTokenService<TUser> where TUser : IdentityUser
{
    private readonly UserManager<TUser> _userManager;
    private readonly IIdentityContext _db;

    public RefreshTokenService(
        UserManager<TUser> userManager,
        IIdentityContext context)
    {
        _userManager = userManager;
        _db = context;
    }
    public async Task<RefreshToken> CreateRefreshTokenAsync(string email, string jwtId, CancellationToken cancellationToken)
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
            Token = newRefreshTokenId,
            JwtId = jwtId
        };
        _db.RefreshTokens.Add(token);
        await _db.SaveChangesAsync(cancellationToken);
        return token;
    }

    public async Task ValidateRefreshToken(string jwt, ClaimsPrincipal principal, RefreshToken refreshToken, CancellationToken cancellationToken)
    {
        if (principal.FindFirst(JwtRegisteredClaimNames.Jti)?.Value != refreshToken.JwtId)
        {
            throw new NotMatchingTokensException(jwt, refreshToken.Token);
        }

        if (refreshToken.Expires < DateTime.Now || refreshToken.Invalidated || refreshToken.Used)
        {
            //Refresh token has expired, or has already been used, so user have to login again.
            throw new SecurityTokenExpiredException("Please log in again");
        }

        refreshToken.Used = true;
        await _db.SaveChangesAsync(cancellationToken);
    }
}