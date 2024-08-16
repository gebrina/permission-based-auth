namespace Auth.Application.Services;

public interface IEmailService
{
   Task<bool> SendEmailAsync(string to,string subject,string body,bool isBodyHtml);   
}