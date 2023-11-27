using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Project.COMMON.Tools
{
    public static class MailService
    {
        public static void Send(string receiver, string password = "rwaodeuxordmtedw", string body = "Test mesajıdır", string subject = "Email Testi", string sender = "yzl3169test@gmail.com")
        {
            MailAddress senderEmail = new MailAddress(sender);
            MailAddress receiverEmail = new MailAddress(receiver);

            //Bizim Email işlemlerimiz SMTP'ye göre yapılır...
            //Kullandıgınız gmail hesabının baska uygulamalar tarafından mesaj gönderme özelligini acmalısınız...

            SmtpClient smtp = new()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail.Address, password)
            };



            //Birazdan kullanacagımız ifade ozel bir scope acarak bir sublocal olusturup orada da garbage collector'in calısmasını saglamak icin verilecektir...

            using (MailMessage message = new(senderEmail, receiverEmail)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }


            //    MailMessage message = new(senderEmail, receiverEmail);
            //message.Subject = subject;
            //message.Body = body;

            //smtp.Send(message);
        }
    }
}
