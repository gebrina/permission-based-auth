using System.Net;
using System.Net.Mail;
using Auth.Api.Settings;
using Auth.Application.Services;
using Microsoft.Extensions.Options;

namespace Auth.Api.Services;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;

    public EmailService(IOptions<EmailSettings> options)
    {
        _emailSettings = options.Value;
    }

    public async Task<bool> SendEmailAsync(string to, string subject, string body, bool isBodyHtml)
    {
        try
        {
            string from = _emailSettings.EmailAddress;
            int port = Convert.ToInt32(_emailSettings.Port);
            string password = _emailSettings.Password;
            string server = _emailSettings.Server;

            var credentails = new NetworkCredential(from, password);
            var smtpClient = new SmtpClient(server, port)
            {
                Credentials = credentails,
                EnableSsl = true
            };

            var mailMessage = new MailMessage(from, to, subject, body)
            {
                IsBodyHtml = isBodyHtml
            };
            await smtpClient.SendMailAsync(mailMessage);
            return true;
        }
        catch (Exception)
        {
            throw;
        }

    }
}