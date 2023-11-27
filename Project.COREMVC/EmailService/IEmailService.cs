using Project.COREMVC.Models;

namespace Project.COREMVC.EmailService
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest email);
    }
}
