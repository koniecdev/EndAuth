using EndAuth.Shared.Roles.Commands.Update;

namespace EndAuth.Application.Roles.Commands.Update;
public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
{
    public UpdateRoleCommandValidator()
    {
        RuleFor(m => m.Id).NotEmpty();
        RuleFor(m => m.Name).NotEmpty();
    }
}
