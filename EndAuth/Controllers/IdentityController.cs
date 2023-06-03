using EndAuth.Shared.Identities.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using EndAuth.Shared.Identities.Commands.Refresh;
using EndAuth.Shared.Identities.Commands.Login;
using EndAuth.Application.Extensions;
using EndAuth.Application.Common.Exceptions;
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
        var result = await _mediator.Send(registerDto);
        return result.Match<IActionResult>(m => Ok(m), e => BadRequest(e.MapToResponse()));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand loginDto)
    {
        var result = await _mediator.Send(loginDto);
        return result.Match<IActionResult>(m => Ok(m), e => e switch
        {
            ResourceNotFoundException => NotFound(e.MapToResponse()),
            _ => BadRequest(e.MapToResponse())
        });
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokensCommand refreshTokensDto)
    {
        var result = await _mediator.Send(refreshTokensDto);
        return result.Match<IActionResult>(m => Ok(m), e => BadRequest(e.MapToResponse()));
    }
}