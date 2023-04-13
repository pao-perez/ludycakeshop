using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System.IO;

namespace LudyCakeShop.Domain
{
    public class EmailSender
    {
        private readonly EmailConfiguration _emailConfig;
        private readonly string settingFile = "appsettings.json";

        public EmailSender()
        {
            _emailConfig = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile(settingFile)
                            .Build()
                            .GetSection("EmailConfiguration")
                            .Get<EmailConfiguration>();
        }

        public void SendEmail(EmailMessage message)
        {
            var emailMessage = ComposeEmail(message);
            SendEmail(emailMessage);
        }

        private MimeMessage ComposeEmail(EmailMessage message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.FromDisplay, _emailConfig.From));
            emailMessage.To.Add(new MailboxAddress(_emailConfig.To, _emailConfig.To));
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
            return emailMessage;
        }

        private void SendEmail(MimeMessage mailMessage)
        {
            using var client = new SmtpClient();
            try
            {
                client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailConfig.UserName, _emailConfig.Password);
                client.Send(mailMessage);
            } // ditch catch here to propagate error
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}
