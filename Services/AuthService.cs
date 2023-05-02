using LudyCakeShop.Domain;
using LudyCakeShop.TechnicalServices;

namespace LudyCakeShop.Services
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
