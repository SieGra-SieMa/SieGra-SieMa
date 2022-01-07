using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        Task<bool> SendAsync(string to, string subject, string html);
    }

    public class EmailService : IEmailService
    {
        //https://ethereal.email/messages
        public IConfiguration _Configuration { get; set; }
        private readonly ILogger<EmailService> _logger;


        private readonly string smtpServer, username, password, from;
        private readonly int port;


        public EmailService(IConfiguration Configuration, ILogger<EmailService> logger)
        {
            _Configuration = Configuration;
            _logger = logger;

            smtpServer = _Configuration.GetValue<string>("EmailService:SmtpServer");
            username = _Configuration.GetValue<string>("EmailService:Username");
            password = _Configuration.GetValue<string>("EmailService:Password");
            from = _Configuration.GetValue<string>("EmailService:From");
            port = _Configuration.GetValue<int>("EmailService:Port");

        }

        public async Task<bool> SendAsync(string to, string subject, string html)
        {
            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            using (var smtp = new SmtpClient())
            {
                try
                {
                    await smtp.ConnectAsync(smtpServer, port, SecureSocketOptions.StartTls);
                    await smtp.AuthenticateAsync(username, password);
                    await smtp.SendAsync(email);
                    await smtp.DisconnectAsync(true);
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return false;
                }
            }

        }
    }
}
