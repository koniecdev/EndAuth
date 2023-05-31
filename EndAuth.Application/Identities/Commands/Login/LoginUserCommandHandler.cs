using EndAuth.Application.Common.Exceptions;
using EndAuth.Domain;
using EndAuth.Shared.Identities.Commands.Login;
using Microsoft.AspNetCore.Identity;

namespace EndAuth.Application.Identities.Commands.Login;
public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<string>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public LoginUserCommandHandler(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    public async Task<Result<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        ApplicationUser? userFromDb = await _userManager.FindByEmailAsync(request.Email);
        if(userFromDb is null)
        {
            return new Result<string>(new ResourceNotFoundException(nameof(ApplicationUser), request.Email));
        }
        SignInResult loginAttemptResults = await _signInManager.PasswordSignInAsync(userFromDb, request.Password, false, false);
        if (loginAttemptResults.Succeeded)
        {
            //GENERATE JWT
            return "quitepoorlyjwt";
        }
        return new Result<string>(new InvalidCredentialsException(request.Email));
    }
}
