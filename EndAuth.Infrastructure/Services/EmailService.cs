using EndAuth.Application.Common.Interfaces;
using FluentEmail.Core;
using FluentEmail.Core.Models;

namespace EndAuth.Infrastructure.Services;

internal sealed class EmailService : IEmailService
{
    public async Task<SendResponse> SendEmailAsync(
        string fromMailAddress,
        string toEmail,
        string toName,
        string emailSubject,
        string emailMessage,
        CancellationToken cancellationToken)
    {
        return await Email
            .From(fromMailAddress)
            .To(toEmail, toName)
            .Subject(emailSubject)
            .Body(emailMessage, true)
            .SendAsync(cancellationToken);
    }
}
