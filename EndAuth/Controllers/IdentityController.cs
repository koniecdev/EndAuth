using EndAuth.Shared.Identities.Commands.Register;
using Microsoft.AspNetCore.Mvc;
using EndAuth.Shared.Identities.Commands.Refresh;
using EndAuth.Shared.Identities.Commands.Login;
using EndAuth.API.Controllers.Common;
using EndAuth.Shared.Identities.Commands.ForgotPassword;
using EndAuth.Shared.Identities.Commands.ResetPassword;

namespace EndAuth.API.Controllers;

[Route("/api/identities")]
public class IdentityController : BaseController
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand registerDto)
    {
        await Mediator.Send(registerDto);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand loginDto)
    {
        var result = await Mediator.Send(loginDto);
        return Ok(result);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokensCommand refreshTokensDto)
    {
        var result = await Mediator.Send(refreshTokensDto);
        return Ok(result);
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand forgotPasswordDto)
    {
        await Mediator.Send(forgotPasswordDto);
        return Ok();
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand resetPasswordDto)
    {
        await Mediator.Send(resetPasswordDto);
        return Ok();
    }
}