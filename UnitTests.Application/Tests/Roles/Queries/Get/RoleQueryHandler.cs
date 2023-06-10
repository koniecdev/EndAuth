using EndAuth.Shared.Dtos.Roles;
using EndAuth.Shared.Roles.Queries.Get;

namespace EndAuth.Application.Roles.Queries.Get;

public class RoleQueryHandler : IRequestHandler<RoleQuery, RoleResponse>
{
    public Task<RoleResponse> Handle(RoleQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
