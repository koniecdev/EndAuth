using EndAuth.Shared.Identities.Commands.Login;
using FluentValidation;

namespace EndAuth.Application.Identities.Commands.Login;
public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(m => m.Email).EmailAddress();
        RuleFor(m => m.Password).NotEmpty();
    }
}
