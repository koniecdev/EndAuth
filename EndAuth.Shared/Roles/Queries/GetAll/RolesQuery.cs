using EndAuth.Shared.Dtos.Roles;

namespace EndAuth.Shared.Roles.Queries.GetAll;

public record RolesQuery : IRequest<IEnumerable<RoleResponse>>;