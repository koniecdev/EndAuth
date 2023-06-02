using EndAuth.Application.Common.Exceptions;
using EndAuth.Application.Common.Interfaces;
using EndAuth.Domain;
using EndAuth.Shared.Identities.Commands.Login;
using Microsoft.AspNetCore.Identity;

namespace EndAuth.Application.Identities.Commands.Login;
public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<string>>
{
    private readonly IJwtService<ApplicationUser> _jwtService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public LoginUserCommandHandler(IJwtService<ApplicationUser> jwtService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _jwtService = jwtService;
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
            return await _jwtService.CreateTokenAsync(request.Email);
        }
        return new Result<string>(new InvalidCredentialsException(request.Email));
    }
}
