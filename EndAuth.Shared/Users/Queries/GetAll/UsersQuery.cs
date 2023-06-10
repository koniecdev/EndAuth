using EndAuth.Shared.Dtos.Users;

namespace EndAuth.Shared.Users.Queries.GetAll;

public record UsersQuery : IRequest<IEnumerable<UserResponse>>;
