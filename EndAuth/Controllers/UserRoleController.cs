using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using EndAuth.API.Controllers.Common;
using Microsoft.AspNetCore.Authorization;
using EndAuth.Shared.Dtos.Users;
using EndAuth.Shared.Dtos.Roles;

namespace EndAuth.Controllers;

[Authorize(Roles = $"{SD.SuperAdminRole}")]
[Route("/api/user-roles")]
public class UserRoleController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> AddUserToRole()
    {
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUserFromRole()
    {
        return NoContent();
    }
}