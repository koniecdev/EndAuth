using EndAuth.Shared.Identities.Commands.Refresh;
using FluentValidation;

namespace EndAuth.Application.Identities.Commands.Login;
public class RefreshTokensCommandValidator : AbstractValidator<RefreshTokensCommand>
{
    public RefreshTokensCommandValidator()
    {
        RuleFor(m => m.AccessToken).NotEmpty();
        RuleFor(m => m.RefreshToken).NotEmpty();
    }
}
