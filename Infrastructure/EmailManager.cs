using LudyCakeShop.Domain;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

namespace LudyCakeShop.Infrastructure
{
    public class EmailManager : IEmailManager
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailManager(EmailConfiguration emailConfig)
        {
            this._emailConfig = emailConfig;
        }

        public void SendEmail(EmailMessage message)
        {
            MimeMessage emailMessage = ComposeEmail(message);
            SendEmail(emailMessage);
        }

        private MimeMessage ComposeEmail(EmailMessage message)
        {
            MimeMessage emailMessage = new();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.FromDisplay, _emailConfig.From));
            emailMessage.To.Add(new MailboxAddress(_emailConfig.To, _emailConfig.To));
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(TextFormat.Text) { Text = message.Content };

            return emailMessage;
        }

        private void SendEmail(MimeMessage mailMessage)
        {
            using SmtpClient client = new();
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
