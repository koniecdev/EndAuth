using EndAuth.Application.Common.Exceptions;
using EndAuth.Domain;
using EndAuth.Domain.Entities;
using EndAuth.Shared.Identities.Commands.Register;
using Microsoft.AspNetCore.Identity;

namespace EndAuth.Application.Identities.Commands.Register;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<string>>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public RegisterUserCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        ApplicationUser applicationUser = new()
        {
            Email = request.Email,
            UserName = request.Username
        };
        if (await _userManager.FindByNameAsync(applicationUser.UserName) is not null
            || await _userManager.FindByEmailAsync(applicationUser.Email) is not null)
        {
            return new(new ResourceAlreadyExistsException(nameof(ApplicationUser), applicationUser.UserName));
        }
        IdentityResult result = await _userManager.CreateAsync(applicationUser, request.Password);
        return result.Succeeded ? applicationUser.Id : new Result<string>(new IdentityResultFailedException(result.Errors));
    }
}
