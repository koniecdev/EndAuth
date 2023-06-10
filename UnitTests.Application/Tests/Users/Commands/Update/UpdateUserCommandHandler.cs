using EndAuth.Shared.Users.Commands.Update;

namespace EndAuth.Application.Users.Commands.Update;
public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    public Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
