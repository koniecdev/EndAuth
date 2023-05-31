using EndAuth.Shared.Identities.Commands.Login;

namespace EndAuth.Application.Identities.Commands.Login;
public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<string>>
{
    public LoginUserCommandHandler()
    {

    }
    public Task<Result<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
