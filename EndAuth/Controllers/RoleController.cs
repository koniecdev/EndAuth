using Microsoft.AspNetCore.Mvc;
using EndAuth.API.Controllers.Common;
using Microsoft.AspNetCore.Authorization;
using EndAuth.Shared.Dtos.Roles;
using EndAuth.Shared.Roles.Commands.Create;
using EndAuth.Shared.Roles.Commands.Update;

namespace EndAuth.Controllers;

[Authorize(Roles = $"{SD.SuperAdminRole}")]
[Route("/api/roles")]
public class RoleController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        IEnumerable<RoleResponse> results = null;
        return Ok(results);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] string id)
    {
        RoleResponse result = null;
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRoleCommand createRoleCommand)
    {
        string id = null;
        return Ok(id);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateRoleCommand updateRoleCommand)
    {
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] string id)
    {
        return NoContent();
    }
}