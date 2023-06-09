using EndAuth.Application.Common.Exceptions;
using EndAuth.Application.Common.Interfaces;
using EndAuth.Application.Common.Interfaces.Factories;
using EndAuth.Application.Identities.Commands.ForgotPassword;
using EndAuth.Application.Identities.Commands.Login;
using EndAuth.Application.Identities.Commands.ResetPassword;
using EndAuth.Domain.Entities;
using EndAuth.Shared.Identities.Commands.ForgotPassword;
using EndAuth.Shared.Identities.Commands.Login;
using EndAuth.Shared.Identities.Commands.ResetPassword;
using FluentAssertions;
using FluentEmail.Core.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net.Mail;
using UnitTests.Application.Common;

namespace UnitTests.Application.Tests;
public class ResetPasswordCommandHandlerTests : CommandTestBase
{
    private readonly ResetPasswordCommandHandler _handler;
    private readonly ResetPasswordCommandValidator _validator;
    private readonly ApplicationUser _appUser = new ()
    {
        UserName = "Default",
        NormalizedUserName = "DEFAULT",
        Email = "Default@example.com",
        NormalizedEmail = "DEFAULT@EXAMPLE.COM"
    };
    public ResetPasswordCommandHandlerTests()
    {
        var fakeUserManager = new FakeUserManagerBuilder()
            .With(m => m.Setup(x => x.FindByEmailAsync("Default@example.com"))
            .ReturnsAsync(_appUser))
            .With(m => m.Setup(x => x.ResetPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success))
        .Build();
        _handler = new(fakeUserManager.Object);
        _validator = new();
    }

    [Fact]
    public async Task ValidEmail_ShouldBeValid()
    {
        ResetPasswordCommand command = new("Default@example.com", "Pass123$", "resettoken");
        Exception ex = Record.Exception(() => ValidateRequest(command));
        ex.Should().BeNull();
        Exception ex1 = await Record.ExceptionAsync(() => _handler.Handle(command, CancellationToken.None));
        ex1.Should().BeNull();
    }

    [Fact]
    public async Task ValidEmail_ShouldBeNotFound()
    {
        ResetPasswordCommand command = new("x@example.com", "Pass123$", "resettoken");
        Exception ex = Record.Exception(() => ValidateRequest(command));
        ex.Should().BeNull();
        Exception ex1 = await Record.ExceptionAsync(() => _handler.Handle(command, CancellationToken.None));
        ex1.Should().BeOfType<ResourceNotFoundException>();
    }

    [Theory]
    [InlineData("@x.pl")]
    [InlineData("@x.")]
    [InlineData("@.pl")]
    [InlineData("x@.pl")]
    [InlineData("x@x.")]
    [InlineData("x@x")]
    [InlineData("xxxxxx@.")]
    public Task InvalidEmail_ShouldBeValidationException(string email)
    {
        ResetPasswordCommand command = new(email, "Pass123$", "resettoken");
        Exception ex = Record.Exception(() => ValidateRequest(command));
        ex.Should().BeOfType<ValidationException>();
        return Task.CompletedTask;
    }

    [Theory]
    [InlineData("default12")]
    [InlineData("Default")]
    [InlineData("default")]
    [InlineData("Defau1")]
    public Task InvalidPassword_ShouldBeValidationException(string pass)
    {
        ResetPasswordCommand command = new("Default@example.com", pass, "resettoken");
        Exception ex = Record.Exception(() => ValidateRequest(command));
        ex.Should().BeOfType<ValidationException>();
        return Task.CompletedTask;
    }

    private void ValidateRequest(ResetPasswordCommand command)
    {
        var validationResult = _validator.Validate(command);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
    }
}