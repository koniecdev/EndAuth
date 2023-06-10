using EndAuth.Shared.Dtos.Users;
using EndAuth.Shared.Users.Queries.Get;

namespace EndAuth.Application.Users.Queries.Get;

public class UserQueryHandler : IRequestHandler<UserQuery, UserResponse>
{
    public Task<UserResponse> Handle(UserQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
