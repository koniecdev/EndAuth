using EndAuth.Shared.Users.Commands.Delete;

namespace EndAuth.Application.UserRoles.Commands.Demote;
public class DemoteFromRoleCommandHandler : IRequestHandler<DemoteFromRoleCommand>
{
    public Task Handle(DemoteFromRoleCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
