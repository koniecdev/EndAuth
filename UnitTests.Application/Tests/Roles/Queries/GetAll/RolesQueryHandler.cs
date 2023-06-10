using EndAuth.Shared.Dtos.Roles;
using EndAuth.Shared.Roles.Queries.GetAll;

namespace EndAuth.Application.Roles.Queries.GetAll;

public class RolesQueryHandler : IRequestHandler<RolesQuery, IEnumerable<RoleResponse>>
{
    public Task<IEnumerable<RoleResponse>> Handle(RolesQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
