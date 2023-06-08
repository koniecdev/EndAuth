using FluentEmail.Core.Models;

namespace EndAuth.Application.Common.Interfaces;

public interface IEmailService
{
    Task<SendResponse> SendEmailAsync(string fromMailAddress, string toEmail, string toName, string emailSubject, string emailMessage, CancellationToken cancellationToken);
}