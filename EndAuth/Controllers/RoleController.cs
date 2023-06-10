using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using EndAuth.API.Controllers.Common;
using Microsoft.AspNetCore.Authorization;
using EndAuth.Shared.Dtos.Users;
using EndAuth.Shared.Dtos.Roles;

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
    public async Task<IActionResult> Get(int id)
    {
        RoleResponse result = null;
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create()
    {
        string id = null;
        return Ok(id);
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