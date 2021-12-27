using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.Services.Email
{
    public interface IEmailService
    {
        void Send(string from, string to, string subject, string html);
    }

    public class EmailService : IEmailService
    {
        public IConfiguration _Configuration { get; set; }

        public EmailService(IConfiguration Configuration)
        {
            _Configuration = Configuration;
        }

        public void Send(string from, string to, string subject, string html)
        {
            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            using var smtp = new SmtpClient();
            //TODO Setup account on ethereal email
            //smtp.Connect("mail.smtp.port", 8025, SecureSocketOptions.StartTls); smtp.ethereal.email
            //smtp.Authenticate("jennyfer.erdman1@ethereal.email", "Nwsv1ycWtFBTSnQGGM");
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
