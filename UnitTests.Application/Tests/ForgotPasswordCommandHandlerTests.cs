using EndAuth.Application.Common.Exceptions;
using EndAuth.Application.Common.Interfaces;
using EndAuth.Application.Common.Interfaces.Factories;
using EndAuth.Application.Identities.Commands.ForgotPassword;
using EndAuth.Shared.Identities.Commands.ForgotPassword;
using EndAuth.Shared.Identities.Commands.Login;
using FluentAssertions;
using FluentEmail.Core.Models;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net.Mail;
using UnitTests.Application.Common;

namespace UnitTests.Application.Tests;
public class ForgotPasswordCommandHandlerTests : CommandTestBase
{
    private readonly ForgotPasswordCommandHandler _handler;
    private readonly ForgotPasswordCommandValidator _validator;
    public ForgotPasswordCommandHandlerTests()
    {
        var dateTimeMock = new Mock<IDateTimeService>();
        dateTimeMock.Setup(m => m.Now).Returns(new DateTime(2023, 1, 1, 0, 0, 0));
        var loggerMock = new Mock<ILogger<ForgotPasswordCommandHandler>>();
        var smtpGmailSenderMock = new Mock<ISmtpClientGmailFactory>();
        smtpGmailSenderMock.Setup(m => m.Create(It.IsAny<string>(), It.IsAny<string>())).Returns(new SmtpClient());
        var emailServiceMock = new Mock<IEmailService>();
        emailServiceMock.Setup(m => m.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), CancellationToken.None))
            .ReturnsAsync(new SendResponse());
        _handler = new(_context, dateTimeMock.Object, loggerMock.Object, _configuration, smtpGmailSenderMock.Object, emailServiceMock.Object);
        _validator = new();
    }

    [Fact]
    public async Task ValidEmail_ShouldBeValid()
    {
        ForgotPasswordCommand command = new("Default@example.com");
        ValidateRequest(command);
        Exception ex = await Record.ExceptionAsync(() => _handler.Handle(command, CancellationToken.None));
        ex.Should().BeNull();
    }

    [Fact]
    public async Task ValidEmail_ShouldBeNotFound()
    {
        ForgotPasswordCommand command = new("x@example.com");
        ValidateRequest(command);
        Exception ex = await Record.ExceptionAsync(() => _handler.Handle(command, CancellationToken.None));
        ex.Should().BeOfType<ResourceNotFoundException>();
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
        ForgotPasswordCommand command = new(email);
        Exception ex = Record.Exception(() => ValidateRequest(command));
        ex.Should().BeOfType<ValidationException>();
        return Task.CompletedTask;
    }

    private void ValidateRequest(ForgotPasswordCommand command)
    {
        var validationResult = _validator.Validate(command);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
    }
}