using EndAuth.Shared.Identities.Commands.ForgotPassword;

namespace EndAuth.Application.Identities.Commands.ForgotPassword;
public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
{
    public ForgotPasswordCommandValidator()
    {
        RuleFor(m => m.Email).NotEmpty().EmailAddress().Matches(@"^[\w\.-]+@[\w\.-]+\.\w+$").WithMessage("Invalid email address.");
    }
}
