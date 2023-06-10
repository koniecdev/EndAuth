using Microsoft.AspNetCore.Mvc;
using EndAuth.API.Controllers.Common;
using EndAuth.Shared.Dtos.Users;
using EndAuth.Shared.Users.Commands.Update;

namespace EndAuth.Controllers;

[Route("/api/users")]
public class UserController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        IEnumerable<UserResponse> results = null;
        return Ok(results);
    }

    [HttpGet("{idOrEmail}")]
    public async Task<IActionResult> Get([FromRoute] string idOrEmail)
    {
        UserResponse results = null;
        return Ok(results);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateUserCommand updateUserCommand)
    {
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] string id)
    {
        return NoContent();
    }
}