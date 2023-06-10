using EndAuth.Shared.Users.Commands.Delete;

namespace EndAuth.Application.Users.Commands.Delete;
public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    public Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
