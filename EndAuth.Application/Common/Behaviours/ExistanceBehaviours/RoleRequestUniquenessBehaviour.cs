using EndAuth.Shared.Interfaces.Markers;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace EndAuth.Application.Common.Behaviours.ExistanceBehaviours;

public class RoleRequestUniquenessBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : IRoleManagerPossibleDuplicateRequest
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleRequestUniquenessBehaviour(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }
    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        if (await _roleManager.RoleExistsAsync(request.Name))
        {
            throw new ResourceAlreadyExistsException(nameof(IdentityRole), request.Name);
        }
    }
}
