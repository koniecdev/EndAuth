using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EndAuth.JwtProvider.AccessTokenServices;
public interface IAccessTokenService<TUser>
{
    public SigningCredentials GetSigningCredentials();
    public Task<ICollection<Claim>> GetClaimsAsync(string email);
    public JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, ICollection<Claim> claims);
    public string ValidateJwt(string jwt, out ClaimsPrincipal claimsPrincipal);

}