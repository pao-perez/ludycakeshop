using LudyCakeShop.Domain;

namespace LudyCakeShop.Services
{
    public interface IAuthService
    {
        public UserAccount GetAuth(string username);
    }
}
