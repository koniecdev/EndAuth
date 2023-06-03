using EndAuth.Application.Common.Exceptions;
using EndAuth.Application.Common.Interfaces;
using EndAuth.Domain.Entities;
using EndAuth.Shared.Dtos;
using EndAuth.Shared.Identities.Commands.Login;
using Microsoft.AspNetCore.Identity;

namespace EndAuth.Application.Identities.Commands.Login;
public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<AuthSuccessResponse>>
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
    public async Task<Result<AuthSuccessResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        ApplicationUser? userFromDb = await _userManager.FindByEmailAsync(request.Email);
        if(userFromDb is null)
        {
            return new Result<AuthSuccessResponse>(new ResourceNotFoundException(nameof(ApplicationUser), request.Email));
        }

        SignInResult loginAttemptResults = await _signInManager.PasswordSignInAsync(userFromDb, request.Password, false, false);
        if (loginAttemptResults.Succeeded)
        {
            string accessToken = await _jwtService.CreateTokenAsync(request.Email);
            RefreshToken refreshToken = await _jwtService.CreateRefreshTokenAsync(request.Email, cancellationToken);
            AuthSuccessResponse response = new(accessToken, refreshToken.Token);
            return response;
        }
        return new Result<AuthSuccessResponse>(new InvalidCredentialsException(request.Email));
    }
}
