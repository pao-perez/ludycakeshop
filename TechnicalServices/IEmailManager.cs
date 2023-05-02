using LudyCakeShop.Domain;

namespace LudyCakeShop.TechnicalServices
{
    public interface IEmailManager
    {
        public void SendEmail(EmailMessage message);
    }
}
