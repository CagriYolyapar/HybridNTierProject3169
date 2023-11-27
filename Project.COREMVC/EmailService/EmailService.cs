using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Project.COREMVC.Models;
using System.Net;


namespace Project.COREMVC.EmailService
{
    public class EmailService : IEmailService
    {
        readonly EmailSetting _emailSetting;

        public EmailService(IOptions<EmailSetting> options)
        {
            _emailSetting = options.Value;
        }
        public async Task SendEmailAsync(MailRequest email)
        {
            MimeMessage mimeMessage = new MimeMessage();
            mimeMessage.Sender = MailboxAddress.Parse(_emailSetting.Email);
            mimeMessage.To.Add(MailboxAddress.Parse(email.ToEmail));
            BodyBuilder builder = new BodyBuilder();
            builder.HtmlBody = email.Body;
            mimeMessage.Body = builder.ToMessageBody();

            using  SmtpClient smtp = new SmtpClient();
            //smtp.ServerCertificateValidationCallback = (sender, certificate, certChainType, errors) => true;
            //smtp.AuthenticationMechanisms.Remove("XOAUTH2");
           
            smtp.Connect(_emailSetting.Host,_emailSetting.Port,SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(new NetworkCredential(_emailSetting.Email, _emailSetting.Password));
            await smtp.SendAsync(mimeMessage);
            smtp.Disconnect(true);
        }
    }
}
