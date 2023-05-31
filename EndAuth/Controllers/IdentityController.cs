using EndAuth.Shared.Identities.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using LanguageExt;
using LanguageExt.Common;
using EndAuth.Application.Common.Mappings.Errors;
using EndAuth.Shared.Identities.Commands.Refresh;
using EndAuth.Shared.Dtos;
using EndAuth.Shared.Identities.Commands.Login;

namespace EndAuth.Controllers;

[ApiController]
[Route("/api/identities")]
public class IdentityController : ControllerBase
{
    private readonly IMediator _mediator;

    public IdentityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody]RegisterUserCommand registerDto)
    {
        var result = await _mediator.Send(registerDto);
        return result.Match<IActionResult>(m => Ok(m), e => BadRequest(e.MapToResponse()));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]LoginUserCommand loginDto)
    {
        var result = await _mediator.Send(loginDto);
        return result.Match<IActionResult>(m => Ok(m), e => BadRequest(e.MapToResponse()));
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody]RefreshTokensCommand refreshTokensDto)
    {
        var result = await _mediator.Send(refreshTokensDto);
        return result.Match<IActionResult>(m => Ok(m), e => BadRequest(e.MapToResponse()));
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