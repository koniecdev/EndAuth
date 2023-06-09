using EndAuth.Domain.Entities;
using EndAuth.Shared.Identities.Commands.ResetPassword;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EndAuth.Application.Identities.Commands.ResetPassword;
public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ResetPasswordCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    public async Task Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var userFromDb = await _userManager.FindByEmailAsync(request.Email)
            ?? throw new ResourceNotFoundException(nameof(ApplicationUser), request.Email);
        var result = await _userManager.ResetPasswordAsync(userFromDb, request.ResetToken, request.NewPassword);
        if (!result.Succeeded)
        {
            throw new IdentityResultFailedException(result.Errors);
        }
    }
}
