using EndAuth.Application.Common.Helpers;
using EndAuth.Shared.Roles.Commands.Delete;
using Microsoft.AspNetCore.Identity;

namespace EndAuth.Application.Roles.Commands.Delete;
public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand>
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public DeleteRoleCommandHandler(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }
    public async Task Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        IdentityRole identityRole = await _roleManager.FindByIdAsync(request.Id);
        IdentityResult results = await _roleManager.DeleteAsync(identityRole);
        SuccessfullIdentityResultHelper.CheckForErrors(results);
    }
}
