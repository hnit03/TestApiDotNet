using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Task3.IServices;
using Task3.Models;

namespace Task3.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private IConfiguration config;
        private readonly ILogger<AuthenticationService> logger;

        public AuthenticationService(IConfiguration config, ILogger<AuthenticationService> logger)
        {
            this.config = config;
            this.logger = logger;
        }

        public string GenerateJSONWebToken(Member member)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, "ThisIsJwtSubject"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("Username", member.Username),
                    new Claim("Fullname", member.Fullname)
                };

                var token = new JwtSecurityToken(config["Jwt:Issuer"],
                                                config["Jwt:Issuer"],
                                                claims,
                                                expires: DateTime.Now.AddMinutes(120),
                                                signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                logger.LogError("AuthenticationService (GenerateJSONWebToken): " + ex.Message);
                return null;
            }
        }
    }
}
