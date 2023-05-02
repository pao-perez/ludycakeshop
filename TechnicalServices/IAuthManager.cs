using LudyCakeShop.Domain;

namespace LudyCakeShop.TechnicalServices
{
    public interface IAuthManager
    {
        public UserAccount GetAuth(string username);
    }
}
