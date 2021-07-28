using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using dotnetServer.Domain.Models;
using dotnetServer.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace dotnetServer.Services.AuthorizationServices
{
    public class TokenService
    {
        private readonly ConfigService Config;

        public TokenService([FromServices] ConfigService config)
        {
            this.Config = config;
        }

        public string GenerateToken(Profile profile)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Config.JwtSecrete);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, profile.Username.ToString()),
                    new Claim(ClaimTypes.Role, typeof(Profile).Name),
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}