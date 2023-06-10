using EndAuth.Shared.Roles.Commands.Create;

namespace EndAuth.Application.Roles.Commands.Create;
public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, int>
{
    public Task<int> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}