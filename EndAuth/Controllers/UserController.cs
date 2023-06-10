using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using EndAuth.API.Controllers.Common;
using Microsoft.AspNetCore.Authorization;
using System.Runtime.CompilerServices;
using EndAuth.Shared.Dtos.Users;

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
    public async Task<IActionResult> Get(string idOrEmail)
    {
        UserResponse results = null;
        return Ok(results);
    }

    [HttpPut]
    public async Task<IActionResult> Update()
    {
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return NoContent();
    }
}