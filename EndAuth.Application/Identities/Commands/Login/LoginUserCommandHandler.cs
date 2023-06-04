using EndAuth.Application.Common.Exceptions;
using EndAuth.Application.Common.Interfaces;
using EndAuth.Domain.Entities;
using EndAuth.Shared.Dtos;
using EndAuth.Shared.Identities.Commands.Login;
using Microsoft.AspNetCore.Identity;

namespace EndAuth.Application.Identities.Commands.Login;
public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthSuccessResponse>
{
    private readonly ITokensService<ApplicationUser> _jwtService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public LoginUserCommandHandler(ITokensService<ApplicationUser> jwtService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _jwtService = jwtService;
        _userManager = userManager;
        _signInManager = signInManager;
    }
    public async Task<AuthSuccessResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        ApplicationUser? userFromDb = await _userManager.FindByEmailAsync(request.Email)
            ?? throw new ResourceNotFoundException(nameof(ApplicationUser), request.Email);
        SignInResult loginAttemptResults = await _signInManager.PasswordSignInAsync(userFromDb, request.Password, false, false);
        if (loginAttemptResults.Succeeded)
        {
            (string jwt, RefreshToken refreshToken) = await _jwtService.CreateTokensAsync(request.Email, cancellationToken);
            AuthSuccessResponse response = new(jwt, refreshToken.Token);
            return response;
        }
        throw new InvalidCredentialsException(request.Email);
    }
}
