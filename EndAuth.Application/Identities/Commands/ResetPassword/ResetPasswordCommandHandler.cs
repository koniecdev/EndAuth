using EndAuth.Shared.Identities.Commands.ResetPassword;

namespace EndAuth.Application.Identities.Commands.ResetPassword;
public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand>
{
    public ResetPasswordCommandHandler()
    {

    }
    public Task Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
