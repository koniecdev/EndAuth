using EndAuth.Shared.Interfaces.Markers;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace EndAuth.Application.Common.Behaviours.ExistanceBehaviours;

public class RoleRequestExistanceBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : IRoleManagerRequest
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleRequestExistanceBehaviour(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }
    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        if(await _roleManager.FindByIdAsync(request.Id) is null)
        {
            throw new ResourceNotFoundException(nameof(IdentityRole), request.Id);
        }
    }
}
