using Euphorolog.Database.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Euphorolog.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace Euphorolog.Services.Utils
{
    public class Utilities : IUtilities
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private static int TokenValidTimeSpan = 10;
        public Utilities(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration =  configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }
        public string CreateJWTToken(Users user)
        {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.userName.ToLower()),
                new Claim(ClaimTypes.Name, user.userName.ToLower().ToString()),
                new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(TokenValidTimeSpan).AddSeconds(1).ToString())
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(_configuration.GetSection("AppSettings:JWTSecretKey").Value));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(TokenValidTimeSpan).AddSeconds(5),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public bool tokenStillValid(DateTime passwordChangedAt)
        {
            var tokenExpirationString = _httpContextAccessor.HttpContext.User?.FindFirst(ClaimTypes.Expiration)?.Value;
            if (tokenExpirationString == null)
            {
                return false;
            }
            DateTime tokenExpiration = Convert.ToDateTime(tokenExpirationString);
            DateTime passwordTimeExpiration = passwordChangedAt.AddDays(TokenValidTimeSpan);
            if (DateTime.Compare(tokenExpiration, passwordTimeExpiration) < 0)
            {
                return false;
            }
            return true;
        }
    }
}
