using EndAuth.Domain.Entities;
using EndAuth.Shared.Identities.Commands.Register;
using Microsoft.AspNetCore.Identity;

namespace EndAuth.Application.Identities.Commands.Register;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public RegisterUserCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        ApplicationUser applicationUser = new()
        {
            Email = request.Email,
            UserName = request.Username
        };
        if (await _userManager.FindByNameAsync(applicationUser.UserName) is not null
            || await _userManager.FindByEmailAsync(applicationUser.Email) is not null)
        {
            throw new ResourceAlreadyExistsException(nameof(ApplicationUser), applicationUser.UserName);
        }
        IdentityResult result = await _userManager.CreateAsync(applicationUser, request.Password);
        if (!result.Succeeded)
        {
            throw new IdentityResultFailedException(result.Errors);
        }
    }
}
