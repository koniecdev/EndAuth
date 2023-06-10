using EndAuth.Application.Common.Exceptions;
using EndAuth.Application.Common.Interfaces;
using EndAuth.Application.Common.Interfaces.Factories;
using EndAuth.Domain.Entities;
using EndAuth.Shared.Identities.Commands.ForgotPassword;
using EndAuth.Shared.Users.Commands.Delete;
using FluentEmail.Core;
using FluentEmail.Core.Models;
using FluentEmail.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EndAuth.Application.Identities.Commands.ForgotPassword;

public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand>
{
    private readonly ILogger<ForgotPasswordCommandHandler> _logger;
    private readonly IConfiguration _configuration;
    private readonly ISmtpClientGmailFactory _smtpClientGmailFactory;
    private readonly IEmailService _emailService;
    private readonly UserManager<ApplicationUser> _userManager;

    public ForgotPasswordCommandHandler(
        ILogger<ForgotPasswordCommandHandler> logger,
        IConfiguration configuration,
        ISmtpClientGmailFactory smtpClientGmailFactory,
        IEmailService emailService,
        UserManager<ApplicationUser> userManager
        )
    {
        _logger = logger;
        _configuration = configuration;
        _smtpClientGmailFactory = smtpClientGmailFactory;
        _emailService = emailService;
        _userManager = userManager;
    }
    public async Task Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var userFromDb = await _userManager.FindByEmailAsync(request.Email)
            ?? throw new ResourceNotFoundException(nameof(ApplicationUser), request.Email);
        var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(userFromDb);

        if (string.IsNullOrWhiteSpace(passwordResetToken))
        {
            throw new Exception("Something went wrong with generating password token");
        }

        string mailAddress = _configuration["Secrets:GmailLogin"];
        string mailAddressPassword = _configuration["Secrets:GmailPassword"];

        SmtpSender sender = new(() => _smtpClientGmailFactory.Create(mailAddress, mailAddressPassword));

        Email.DefaultSender = sender;

        SendResponse emailResponse = await _emailService
            .SendEmailAsync(mailAddress,
            userFromDb.Email,
            userFromDb.UserName,
            "Password reset request",
            $"Your password reset link: <a href=\"https://localhost:7207/api/identities/reset-password?token={passwordResetToken}\">Click here</a> or paste url: https://localhost:7207/api/identities/reset-password?email={userFromDb.Email}&token={passwordResetToken}",
            cancellationToken);

        if (!emailResponse.Successful)
        {
            throw new EmailException(userFromDb.Email);
        }

        _logger.LogInformation("Email has been sent to {email}", userFromDb.Email);
    }
}
