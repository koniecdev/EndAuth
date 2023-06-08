using EndAuth.Application.Common.Interfaces.Factories;
using System.Net;
using System.Net.Mail;

namespace EndAuth.Infrastructure.Factories;

internal sealed class SmtpClientGmailFactory : ISmtpClientGmailFactory
{
    public SmtpClient Create(string mailAddress, string password)
    {
        SmtpClient smtpClient = new()
        {
            DeliveryMethod = SmtpDeliveryMethod.Network,
            Host = "smtp.gmail.com",
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(mailAddress, password),
            Port = 587
        };
        return smtpClient;
    }
}
