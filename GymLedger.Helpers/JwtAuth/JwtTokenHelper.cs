using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using GymLedger.Models.Models;
using Microsoft.IdentityModel.Tokens;

namespace GymLedger.Helpers.JwtAuth
{
    public static class JwtTokenHelper
    {
        private static readonly string SecretKey = "SuperSecureLongKeyForJwtAuthentication!123123432424332"; // Replace with a secure key.

        public static string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static User GetUserFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecretKey); // Your secret key
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            try
            {
                var principal = handler.ValidateToken(token, validationParameters, out var validatedToken);
                var username = principal.Identity.Name;

                // Now fetch the user by username
                using (var db = new Data.DataContext())
                {
                    var user = db.Users.SingleOrDefault(u => u.Username == username);
                    if (user == null) throw new Exception("User not found.");
                    return user;
                }
            }
            catch (Exception)
            {
                throw new UnauthorizedAccessException("Invalid token.");
            }
        }
    }
}
