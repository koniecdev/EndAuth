using EndAuth.Application.Common.Exceptions;
using EndAuth.Application.Common.Interfaces;
using EndAuth.Application.Identities.Commands.Login;
using EndAuth.Domain.Entities;
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
        var jwtServiceMock = new Mock<ITokensService<ApplicationUser>>();
        string fakeJwt = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiRGVmYXVsdCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6IkRlZmF1bHRAZXhhbXBsZS5jb20iLCJleHAiOjE2ODU2OTE1MDgsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcyMDciLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo3MTcxIn0.mVGcL8gpdg_w_yO3Prcl6f2LqQ8JpeWIddZoRa-azlY";
        RefreshToken fakeRefresh = new()
        {
            Token = "FakeToken"
        };
        jwtServiceMock.Setup(m => m.CreateTokensAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(value: (fakeJwt, fakeRefresh));
        _handler = new(jwtServiceMock.Object, fakeUserManager.Object, fakeSignInManager.Object);
        _validator = new();
    }

    [Fact]
    public async Task Login_ValidData_ShouldBeValid()
    {
        LoginUserCommand command = new("Default@example.com", "Default123$");
        ValidateRequest(command);
        var result = await _handler.Handle(command, CancellationToken.None);
        result.AccessToken.Length.Should().BeGreaterThan(1);
        result.RefreshToken.Length.Should().BeGreaterThan(1);
    }

    [Fact]
    public async Task Login_InvalidPassword_ShouldBeInvalid()
    {
        LoginUserCommand command = new("Default@example.com", "wffqwfqwfwqfqf");
        ValidateRequest(command);
        Exception ex = await Record.ExceptionAsync(() => _handler.Handle(command, CancellationToken.None));
        ex.Should().BeOfType<InvalidCredentialsException>();
    }

    [Fact]
    public async Task Login_InvalidEmail_ShouldBeInvalid()
    {
        LoginUserCommand command = new("qwfwqf@koniec.dev", "wffqwfqwfwqfqf");
        ValidateRequest(command);
        Exception ex = await Record.ExceptionAsync(() => _handler.Handle(command, CancellationToken.None));
        ex.Should().BeOfType<ResourceNotFoundException>();
    }

    [Theory]
    [InlineData("Default@")]
    [InlineData("Default@.com")]
    [InlineData("Default@x.")]
    [InlineData("@x.")]
    [InlineData("@x.pl")]
    public Task Login_InvalidEmail_ShouldBeValidationError(string email)
    {
        LoginUserCommand command = new(email, "wffqwfqwfwqfqf");
        Exception ex = Record.Exception(() => ValidateRequest(command));
        ex.Should().BeOfType<ValidationException>();
        return Task.CompletedTask;
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