using LudyCakeShop.Core.Domain.Notification;

namespace LudyCakeShop.Core.Infrastructure
{
    public interface IEmailManager
    {
        public void SendEmail(EmailMessage message);
    }
}
