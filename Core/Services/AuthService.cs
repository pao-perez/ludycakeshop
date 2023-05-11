using LudyCakeShop.Core.Domain.Data;
using LudyCakeShop.Core.Infrastructure;

namespace LudyCakeShop.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthManager _authManager;

        public AuthService(IAuthManager authManager)
        {
            this._authManager = authManager;
        }

        public UserAccount GetAuth(string username)
        {
            return _authManager.GetAuth(username);
        }
    }
}
