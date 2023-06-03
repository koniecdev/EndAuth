using EndAuth.Application.Common.Interfaces;
using EndAuth.Application.Identities.Commands.Login;
using EndAuth.Domain.Entities;
using EndAuth.JwtProvider.Services;
using EndAuth.Shared.Identities.Commands.Login;
using FluentAssertions;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Moq;
using UnitTests.Application.Common;

namespace UnitTests.Application.Tests;
public class LoginUserCommandHandlerTest : CommandTestBase
{
    private readonly LoginUserCommandHandler _handler;
    private readonly LoginUserCommandValidator _validator;
    public LoginUserCommandHandlerTest()
    {
        var appUser = new ApplicationUser
        {
            UserName = "Default",
            NormalizedUserName = "DEFAULT",
            Email = "Default@example.com",
            NormalizedEmail = "DEFAULT@EXAMPLE.COM"
        };
        var fakeUserManager = new FakeUserManagerBuilder()
        .With(m => m.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
        .ReturnsAsync(IdentityResult.Success))
        .With(m => m.Setup(x => x.FindByEmailAsync("Default@example.com"))
        .ReturnsAsync(appUser))
        .Build();
        var fakeSignInManager = new FakeSignInManagerBuilder()
        .With(x => x.Setup(sm => sm.PasswordSignInAsync(It.IsAny<ApplicationUser>(),
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<bool>()))
        .ReturnsAsync(SignInResult.Failed))
        .With(x => x.Setup(sm => sm.PasswordSignInAsync(
                It.Is<ApplicationUser>(m=>m.Equals(appUser)),
                It.Is<string>(xd => xd == "Default123$"),
                It.IsAny<bool>(),
                It.IsAny<bool>()))
        .ReturnsAsync(SignInResult.Success))
        .Build();
        //var jwtServiceMock = new Mock<ITokensService<ApplicationUser>>();
        //jwtServiceMock.Setup(m => m.CreateTokensAsync("Default@example.com", CancellationToken.None)).ReturnsAsync(new ("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiRGVmYXVsdCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6IkRlZmF1bHRAZXhhbXBsZS5jb20iLCJleHAiOjE2ODU2OTE1MDgsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcyMDciLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo3MTcxIn0.mVGcL8gpdg_w_yO3Prcl6f2LqQ8JpeWIddZoRa-azlY", Guid.NewGuid().ToString()));
        //_handler = new(new TokensService<ApplicationUser>(fakeUserManager.Object), fakeUserManager.Object, fakeSignInManager.Object);
        //_validator = new();
    }

    [Fact]
    public async Task Login_ValidData_ShouldBeValid()
    {
        LoginUserCommand command = new("Default@example.com", "Default123$");
        ValidateRequest(command);
        var result = await _handler.Handle(command, CancellationToken.None);
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Login_InvalidPassword_ShouldBeInvalid()
    {
        LoginUserCommand command = new("Default@example.com", "wffqwfqwfwqfqf");
        ValidateRequest(command);
        var result = await _handler.Handle(command, CancellationToken.None);
        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public async Task Login_InvalidEmail_ShouldBeInvalid()
    {
        LoginUserCommand command = new("qwfwqf@koniec.dev", "wffqwfqwfwqfqf");
        ValidateRequest(command);
        var result = await _handler.Handle(command, CancellationToken.None);
        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public async Task Login_InvalidEmail_ShouldBeValidationError()
    {
        LoginUserCommand command = new("qwfwqf@koniec.dev", "wffqwfqwfwqfqf");
        ValidateRequest(command);
        var result = await _handler.Handle(command, CancellationToken.None);
        result.IsSuccess.Should().BeFalse();
    }

    private void ValidateRequest(LoginUserCommand command)
    {
        var validationResult = _validator.Validate(command);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
    }
}