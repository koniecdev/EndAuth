using Microsoft.AspNetCore.Mvc;
using EndAuth.API.Controllers.Common;
using Microsoft.AspNetCore.Authorization;
using EndAuth.Shared.Dtos.Roles;
using EndAuth.Shared.Roles.Commands.Create;
using EndAuth.Shared.Roles.Commands.Update;
using EndAuth.Shared.Roles.Queries.GetAll;
using EndAuth.Shared.Roles.Commands.Delete;

namespace EndAuth.API.Controllers;

//[Authorize(Roles = $"{SD.SuperAdminRole}")]
[Route("/api/roles")]
public class RoleController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await Mediator.Send(new RolesQuery()));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] string roleName)
    {
        RoleResponse result = null;
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRoleCommand createRoleCommand)
    {
        await Mediator.Send(createRoleCommand);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateRoleCommand updateRoleCommand)
    {
        await Mediator.Send(updateRoleCommand);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] string id)
    {
        await Mediator.Send(new DeleteRoleCommand(id));
        return NoContent();
    }
}