using EndAuth.Application.Common.Exceptions;
using EndAuth.Application.Identities.Commands.Register;
using EndAuth.Domain.Entities;
using EndAuth.Shared.Identities.Commands.Register;
using FluentAssertions;
using FluentValidation;
using LanguageExt.Common;
using Microsoft.AspNetCore.Identity;
using Moq;
using UnitTests.Application.Common;

namespace UnitTests.Application.Tests;
public class RegisterUserCommandHandlerTest : CommandTestBase
{
    private readonly RegisterUserCommandHandler _handler;
    private readonly RegisterUserCommandValidator _validator;
    public RegisterUserCommandHandlerTest()
    {
        var fakeUserManager = new FakeUserManagerBuilder()
        .With(m=>m.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
        .ReturnsAsync(IdentityResult.Success))
        .With(m => m.Setup(x => x.FindByNameAsync("Default"))
        .ReturnsAsync(new ApplicationUser()))
        .With(m => m.Setup(x => x.FindByEmailAsync("Default@example.com"))
        .ReturnsAsync(new ApplicationUser()))
        .Build();
        _handler = new(fakeUserManager.Object);
        _validator = new();
    }

    [Fact]
    public async Task Register_ValidData_ShouldBeValid()
    {
        RegisterUserCommand command = new("Default@xd.pl", "Default1", "Default123");
        ValidateRequest(command);
        var result = await _handler.Handle(command, CancellationToken.None);
        result.IsSuccess.Should().BeTrue();
    }

    [Theory]
    [InlineData("Default1")]
    [InlineData("defauLt123")]
    [InlineData("$defauLt12")]
    public async Task Register_ValidPassword_ShouldBeValid(string pass)
    {
        RegisterUserCommand command = new("Default@xd.pl", "Default1", pass);
        ValidateRequest(command);
        var result = await _handler.Handle(command, CancellationToken.None);
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public Task Register_InvalidEmail_ShouldBeInvalid()
    {
        RegisterUserCommand command = new("Default@", "Default", "Default123$");
        try
        {
            ValidateRequest(command);
            throw new Exception();
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<ValidationException>();
            return Task.CompletedTask;
        }
    }

    [Theory]
    [InlineData("Default@example.com", "freeusername")]
    [InlineData("Default@wow.com", "Default")]
    public async Task Register_DuplicateEmailOrUsername_ShouldBeInvalid(string email, string username)
    {
        RegisterUserCommand command = new(email, username, "Default123$");
        ValidateRequest(command);
        var result = await _handler.Handle(command, CancellationToken.None);
        result.IsSuccess.Should().BeFalse();
    }

    [Theory]
    [InlineData("default12")]
    [InlineData("Default")]
    [InlineData("default")]
    [InlineData("Defau1")]
    public Task Register_InvalidPassword_ShouldBeInvalid(string pass)
    {
        RegisterUserCommand command = new("Default1@koniec.dev", "Default1", pass);
        try
        {
            ValidateRequest(command);
            throw new Exception();
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<ValidationException>();
            return Task.CompletedTask;
        }
    }


    private void ValidateRequest(RegisterUserCommand command)
    {
        var validationResult = _validator.Validate(command);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
    }
}