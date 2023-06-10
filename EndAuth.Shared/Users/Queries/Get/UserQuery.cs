using EndAuth.Shared.Dtos.Users;

namespace EndAuth.Shared.Users.Queries.Get;

public record UserQuery(string IdOrEmail) : IRequest<UserResponse>;
