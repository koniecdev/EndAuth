using EndAuth.Application.Common.Exceptions;
using EndAuth.Application.Common.Interfaces;
using EndAuth.Domain.Entities;
using EndAuth.Shared.Identities.Commands.ForgotPassword;
using FluentEmail.Core;
using FluentEmail.Smtp;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;

namespace EndAuth.Application.Identities.Commands.ForgotPassword;

public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand>
{
    private readonly IIdentityContext _db;
    private readonly IDateTimeService _dateTimeService;
    private readonly ILogger<ForgotPasswordCommandHandler> _logger;
    private readonly IConfiguration _configuration;

    public ForgotPasswordCommandHandler(IIdentityContext db, IDateTimeService dateTimeService, ILogger<ForgotPasswordCommandHandler> logger, IConfiguration configuration)
    {
        _db = db;
        _dateTimeService = dateTimeService;
        _logger = logger;
        _configuration = configuration;
    }
    public async Task Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        ApplicationUser userFromDb = await _db.ApplicationUsers.AsNoTracking().SingleOrDefaultAsync(m => m.NormalizedEmail == request.Email.ToUpperInvariant(), cancellationToken)
            ?? throw new ResourceNotFoundException(nameof(ApplicationUser), request.Email);

        PasswordResetToken passwordResetToken = new()
        {
            ApplicationUserId = userFromDb.Id,
            Token = Guid.NewGuid().ToString(),
            ValidUntil = _dateTimeService.Now.AddDays(1)
        };

        var u = _configuration["Secrets:GmailLogin"];
        var p = _configuration["Secrets:GmailPassword"];

        var sender = new SmtpSender(() => new SmtpClient("localhost")
        {
            DeliveryMethod = SmtpDeliveryMethod.Network,
            Host = "smtp.gmail.com",
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(u, p),
            Port = 587
        });

        Email.DefaultSender = sender;
        var email = await Email
            .From(u)
            .To(userFromDb.Email, userFromDb.UserName)
            .Subject("Password reset request")
            .Body($"Your password reset link: <a href=\"https://localhost:7207/api/identities/reset-password?token={passwordResetToken.Token}\">Click here</a> or paste url: https://localhost:7207/api/identities/reset-password?token={passwordResetToken.Token}", true)
            .SendAsync(cancellationToken);

        if (!email.Successful)
        {
            throw new EmailException(userFromDb.Email);
        }

        _db.PasswordResetTokens.Add(passwordResetToken);
        await _db.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Email has been sent to {email}", userFromDb.Email);
    }
}
