using LudyCakeShop.Domain;

namespace LudyCakeShop.Infrastructure
{
    public interface IAuthManager
    {
        public UserAccount GetAuth(string username);
    }
}
