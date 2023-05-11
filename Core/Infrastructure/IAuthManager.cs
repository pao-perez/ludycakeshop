using LudyCakeShop.Core.Domain.Data;

namespace LudyCakeShop.Core.Infrastructure
{
    public interface IAuthManager
    {
        public UserAccount GetAuth(string username);
    }
}
