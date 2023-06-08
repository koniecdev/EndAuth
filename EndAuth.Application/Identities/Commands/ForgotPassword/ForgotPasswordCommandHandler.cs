using EndAuth.Application.Common.Exceptions;
using EndAuth.Application.Common.Interfaces;
using EndAuth.Application.Common.Interfaces.Factories;
using EndAuth.Domain.Entities;
using EndAuth.Shared.Identities.Commands.ForgotPassword;
using FluentEmail.Core;
using FluentEmail.Core.Models;
using FluentEmail.Smtp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EndAuth.Application.Identities.Commands.ForgotPassword;

public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand>
{
    private readonly IIdentityContext _db;
    private readonly IDateTimeService _dateTimeService;
    private readonly ILogger<ForgotPasswordCommandHandler> _logger;
    private readonly IConfiguration _configuration;
    private readonly ISmtpClientGmailFactory _smtpClientGmailFactory;
    private readonly IEmailService _emailService;

    public ForgotPasswordCommandHandler(
        IIdentityContext db,
        IDateTimeService dateTimeService,
        ILogger<ForgotPasswordCommandHandler> logger,
        IConfiguration configuration,
        ISmtpClientGmailFactory smtpClientGmailFactory,
        IEmailService emailService
        )
    {
        _db = db;
        _dateTimeService = dateTimeService;
        _logger = logger;
        _configuration = configuration;
        _smtpClientGmailFactory = smtpClientGmailFactory;
        _emailService = emailService;
    }
    public async Task Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        ApplicationUser userFromDb = await _db.ApplicationUsers.Include(m=>m.PasswordResetTokens)
            .SingleOrDefaultAsync(m => m.NormalizedEmail == request.Email.ToUpperInvariant(), cancellationToken)
            ?? throw new ResourceNotFoundException(nameof(ApplicationUser), request.Email);

        InvalidateExistingTokens(userFromDb.PasswordResetTokens.Where(m => m.Invalidated == false));
        
        PasswordResetToken passwordResetToken = GeneratePasswordResetToken(userFromDb.Id);

        string mailAddress = _configuration["Secrets:GmailLogin"];
        string mailAddressPassword = _configuration["Secrets:GmailPassword"];

        SmtpSender sender = new(() => _smtpClientGmailFactory.Create(mailAddress, mailAddressPassword));

        Email.DefaultSender = sender;

        SendResponse emailResponse = await _emailService
            .SendEmailAsync(mailAddress,
            userFromDb.Email,
            userFromDb.UserName,
            "Password reset request",
            $"Your password reset link: <a href=\"https://localhost:7207/api/identities/reset-password?token={passwordResetToken.Token}\">Click here</a> or paste url: https://localhost:7207/api/identities/reset-password?token={passwordResetToken.Token}",
            cancellationToken);

        if (!emailResponse.Successful)
        {
            throw new EmailException(userFromDb.Email);
        }

        _db.PasswordResetTokens.Add(passwordResetToken);
        await _db.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Email has been sent to {email}", userFromDb.Email);
    }

    private PasswordResetToken GeneratePasswordResetToken(string userId)
    {
        return new()
        {
            ApplicationUserId = userId,
            Token = Guid.NewGuid().ToString(),
            ValidUntil = _dateTimeService.Now.AddDays(1)
        };
    }

    private static void InvalidateExistingTokens(IEnumerable<PasswordResetToken> passwordResetTokens)
    {
        if (passwordResetTokens.Any())
        {
            foreach (PasswordResetToken? item in passwordResetTokens)
            {
                item.Invalidated = true;
            }
        }
    }
}
