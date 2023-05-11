using LudyCakeShop.Core.Domain.Config;
using LudyCakeShop.Core.Domain.Data;
using LudyCakeShop.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LudyCakeShop.Web.Controllers
{
    internal class Auth
    {
        public string Token { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly AuthConfiguration _authConfig;

        public AuthController(IAuthService authService, AuthConfiguration authConfig)
        {
            this._authService = authService;
            this._authConfig = authConfig;
        }

        [HttpPost]
        [Consumes("application/json")]
        public IActionResult Post(UserAccount userAccount)
        {
            if (userAccount == null)
            {
                return StatusCode(400, "UserAccount Invalid.");
            }

            UserAccount storedUserAccount;
            storedUserAccount = _authService.GetAuth(userAccount.Username);

            if (storedUserAccount == null)
            {
                return StatusCode(404, "Username not found.");
            }

            if (userAccount.Username.Equals(storedUserAccount.Username) && userAccount.Password.Equals(storedUserAccount.Password))
            {
                SymmetricSecurityKey secretKey = new(Encoding.UTF8.GetBytes(_authConfig.SigningKeySecret));
                SigningCredentials signingCredentials = new(secretKey, SecurityAlgorithms.HmacSha256);

                JwtSecurityToken tokenOptions = new(
                        issuer: _authConfig.ValidIssuer,
                        audience: _authConfig.ValidAudience,
                        claims: new List<Claim>(),
                        expires: DateTime.Now.AddMinutes(_authConfig.TokenExpiration),
                        signingCredentials: signingCredentials
                    );

                Auth auth = new()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(tokenOptions)
                };
                return StatusCode(200, auth);
            }
            else
            {
                return StatusCode(401, "Incorrect username or password.");
            }
        }
    }
}
