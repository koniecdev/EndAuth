using EndAuth.Shared.Roles.Commands.Delete;

namespace EndAuth.Application.Roles.Commands.Delete;
public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand>
{
    public Task Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
