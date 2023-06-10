using EndAuth.Shared.Dtos.Roles;

namespace EndAuth.Shared.Roles.Queries.Get;

public record RoleQuery(string IdOrName) : IRequest<RoleResponse>;
