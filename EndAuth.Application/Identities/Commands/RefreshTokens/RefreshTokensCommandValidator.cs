using EndAuth.Shared.Identities.Commands.Refresh;

namespace EndAuth.Application.Identities.Commands.RefreshTokens;
public class RefreshTokensCommandValidator : AbstractValidator<RefreshTokensCommand>
{
    public RefreshTokensCommandValidator()
    {
        RuleFor(m => m.AccessToken).NotEmpty();
        RuleFor(m => m.RefreshToken).NotEmpty();
    }
}
