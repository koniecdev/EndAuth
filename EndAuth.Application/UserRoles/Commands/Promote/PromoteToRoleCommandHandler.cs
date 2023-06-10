using EndAuth.Shared.Users.Commands.Update;

namespace EndAuth.Application.UserRoles.Commands.Promote;
public class PromoteToRoleCommandHandler : IRequestHandler<PromoteToRoleCommand>
{
    public Task Handle(PromoteToRoleCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
