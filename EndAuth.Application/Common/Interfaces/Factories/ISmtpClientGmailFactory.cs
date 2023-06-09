using System.Net.Mail;

namespace EndAuth.Application.Common.Interfaces.Factories;

public interface ISmtpClientGmailFactory
{
    SmtpClient Create(string mailAddress, string password);
}
