using EndAuth.Application.Common.Helpers;
using EndAuth.Shared.Roles.Commands.Create;
using LanguageExt.Pipes;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EndAuth.Application.Roles.Commands.Create;
public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, string>
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public CreateRoleCommandHandler(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }
    public async Task<string> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        IdentityResult results = await _roleManager.CreateAsync(new(request.Name));
        SuccessfullIdentityResultHelper.CheckForErrors(results);
        IdentityRole fromDb = await _roleManager.FindByNameAsync(request.Name);
        return fromDb.Id;
    }
}