using EndAuth.Application.Common.Helpers;
using EndAuth.Shared.Roles.Commands.Update;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace EndAuth.Application.Roles.Commands.Update;
public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand>
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public UpdateRoleCommandHandler(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }
    public async Task Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var roleFromDb = await _roleManager.FindByIdAsync(request.Id);
        roleFromDb.Name = request.Name;
        roleFromDb.NormalizedName = request.Name.ToUpper();
        IdentityResult results = await _roleManager.UpdateAsync(roleFromDb);
        SuccessfullIdentityResultHelper.CheckForErrors(results);
    }
}
