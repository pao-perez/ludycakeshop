﻿using LudyCakeShop.Domain;
using LudyCakeShop.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Text;

namespace LudyCakeShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly LCS _requestDirector;
        private readonly AuthConfiguration _authConfig;

        public AuthController(AuthConfiguration authConfig)
        {
            _requestDirector = new();
            _authConfig = authConfig;
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
            try
            {
                storedUserAccount = _requestDirector.GetAuth(userAccount.Username);

                if (storedUserAccount == null)
                {
                    return StatusCode(404, "Username not found.");
                }

                if (userAccount.Username.Equals(storedUserAccount.Username) && userAccount.Password.Equals(storedUserAccount.Password))
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authConfig.SigningKeySecret));
                    var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                    var tokenOptions = new JwtSecurityToken(
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
                } else
                {
                    return StatusCode(401, "Incorrect username or password.");
                }
            }
            catch (Exception)
            {
                // TODO: log exception
                return StatusCode(500, "Server Error. The server is unable to fulfill your request at this time.");
            }
        }
    }
}
