using EndAuth.Shared.Identities.Commands.ResetPassword;

namespace EndAuth.Application.Identities.Commands.ResetPassword;
public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(m => m.Email).NotEmpty().EmailAddress().Matches(@"^[\w\.-]+@[\w\.-]+\.\w+$").WithMessage("Invalid email address.");
        RuleFor(m => m.ResetToken).NotEmpty();
        RuleFor(m => m.NewPassword).NotEmpty().WithMessage("Your password cannot be empty")
                    .MinimumLength(8).WithMessage("Your password length must be at least 8.")
                    .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                    .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                    .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.");
    }
}
