using EndAuth.Shared.Identities.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using EndAuth.Shared.Identities.Commands.Refresh;
using EndAuth.Shared.Identities.Commands.Login;
using Microsoft.AspNetCore.Cors;

namespace EndAuth.Controllers;

[ApiController]
[EnableCors("AllowedPolicies")]
[Route("/api/identities")]
public class IdentityController : ControllerBase
{
    private readonly IMediator _mediator;

    public IdentityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand registerDto)
    {
        await _mediator.Send(registerDto);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand loginDto)
    {
        var result = await _mediator.Send(loginDto);
        return Ok(result);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokensCommand refreshTokensDto)
    {
        var result = await _mediator.Send(refreshTokensDto);
        return Ok(result);
    }
}