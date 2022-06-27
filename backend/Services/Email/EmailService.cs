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

namespace SieGraSieMa.Services
{
    public interface IEmailService
    {
        Task<bool> SendAsync(string to, string subject, string html);
    }

    public class EmailService : IEmailService
    {
        //https://ethereal.email/messages
        public IConfiguration Configuration { get; set; }
        private readonly ILogger<EmailService> _logger;


        private readonly string smtpServer, username, password, from;
        private readonly int port;


        public EmailService(IConfiguration Configuration, ILogger<EmailService> logger)
        {
            this.Configuration = Configuration;
            _logger = logger;

            smtpServer = Configuration.GetConnectionString("emailSmtpServer"); 
            username = Configuration.GetConnectionString("emailUsername"); 
            password = Configuration.GetConnectionString("emailPassword"); 
            from = Configuration.GetConnectionString("emailFrom"); 
            port = int.Parse(Configuration.GetConnectionString("emailPort")); 

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
