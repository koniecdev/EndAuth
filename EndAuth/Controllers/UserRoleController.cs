using Microsoft.AspNetCore.Mvc;
using EndAuth.API.Controllers.Common;
using Microsoft.AspNetCore.Authorization;
using EndAuth.Shared.Users.Commands.Update;
using EndAuth.Shared.Users.Commands.Delete;

namespace EndAuth.API.Controllers;

[Authorize(Roles = $"{SD.SuperAdminRole}")]
[Route("/api/user-roles")]
public class UserRoleController : BaseController
{
    [HttpPost("promote")]
    public async Task<IActionResult> PromoteToRole([FromBody] PromoteToRoleCommand promoteToRoleCommand)
    {
        return NoContent();
    }

    [HttpPost("demote")]
    public async Task<IActionResult> DemoteFromRole([FromBody] DemoteFromRoleCommand demoteFromRoleCommand)
    {
        return NoContent();
    }
}