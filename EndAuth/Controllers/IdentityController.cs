using EndAuth.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace EndAuth.Controllers;

[ApiController]
[Route("/api/identities")]
public class IdentityController : ControllerBase
{
    public IdentityController()
    {
        
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody]RegisterDto registerDto, CancellationToken ct = default)
    {
        return Ok("");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]LoginDto registerDto, CancellationToken ct = default)
    {
        return Ok("");
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokensDto refreshTokensDto, CancellationToken ct = default)
    {
        return Ok("");
    }

    //private const string TokenSecret = "ForTheLoveOfGodStoreAndLoadThisSecurely";
    //private static readonly TimeSpan TokenLifeTime = TimeSpan.FromMinutes(1);

    //[HttpPost("token")]
    //public IActionResult GenerateToken([FromBody]TokenGenerationRequest request)
    //{
    //    var tokenHandler = new JwtSecurityTokenHandler();
    //    var key = Encoding.UTF8.GetBytes(TokenSecret);

    //    var claims = new List<Claim>
    //    {
    //        new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    //        new(JwtRegisteredClaimNames.Sub, request.Email),
    //        new(JwtRegisteredClaimNames.Email, request.Email),
    //        new("userid", request.UserId.ToString())
    //    };

    //    foreach(var claimPair in request.CustomClaims)
    //    {
    //        var jsonElement = (JsonElement)claimPair.Value;
    //        var valueType = jsonElement.ValueKind switch
    //        {
    //            JsonValueKind.True => ClaimValueTypes.Boolean,
    //            JsonValueKind.False => ClaimValueTypes.Boolean,
    //            JsonValueKind.Number => ClaimValueTypes.Double,
    //            _ => ClaimValueTypes.String
    //        };

    //        var claim = new Claim(claimPair.Key, claimPair.Value.ToString()!, valueType);
    //        claims.Add(claim);
    //    }

    //    var tokenDescriptor = new SecurityTokenDescriptor
    //    {
    //        Subject = new ClaimsIdentity(claims),
    //        Expires = DateTime.UtcNow.Add(TokenLifeTime),
    //        Issuer = "https://localhost:7207",
    //        Audience = "",
    //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    //    };

    //    var token = tokenHandler.CreateToken(tokenDescriptor);
    //    var jwt = tokenHandler.WriteToken(token);
    //    return Ok(jwt);
    //}
}