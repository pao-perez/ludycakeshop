using LudyCakeShop.Domain;

namespace LudyCakeShop.Infrastructure
{
    public interface IEmailManager
    {
        public void SendEmail(EmailMessage message);
    }
}
