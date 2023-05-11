using LudyCakeShop.Core.Domain.Data;

namespace LudyCakeShop.Core.Services
{
    public interface IAuthService
    {
        public UserAccount GetAuth(string username);
    }
}
