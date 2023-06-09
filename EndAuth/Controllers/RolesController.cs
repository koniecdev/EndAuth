using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using EndAuth.API.Controllers.Common;

namespace EndAuth.Controllers;

[ApiController]
[EnableCors("AllowedPolicies")]
[Route("/api/roles")]
public class RolesController : BaseController
{
}