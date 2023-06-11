using EndAuth.Shared.Dtos.Roles;
using EndAuth.Shared.Roles.Queries.GetAll;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EndAuth.Application.Roles.Queries.GetAll;

public class RolesQueryHandler : IRequestHandler<RolesQuery, IEnumerable<RoleResponse>>
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RolesQueryHandler(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<IEnumerable<RoleResponse>> Handle(RolesQuery request, CancellationToken cancellationToken)
    {
        var results = await _roleManager.Roles.Select(m => new RoleResponse(m.Id, m.Name)).ToListAsync(cancellationToken);
        return results;
    }
}
