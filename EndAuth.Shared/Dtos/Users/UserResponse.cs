using EndAuth.Shared.Dtos.Roles;

namespace EndAuth.Shared.Dtos.Users;

public record UserResponse(string Id, string Email, string Username, IEnumerable<RoleResponse> Roles);