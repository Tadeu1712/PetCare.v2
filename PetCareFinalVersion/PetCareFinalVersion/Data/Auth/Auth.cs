using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PetCareFinalVersion.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;


namespace PetCareFinalVersion.Data
{
    public static class Auth
    {
        //LOGIN
        public static User Login(string aEmail, string aPassword, AppDbContext _context)
        {

            var user = _context.Users.Where(u => u.Email == aEmail).Single();

            if (BCrypt.Net.BCrypt.EnhancedVerify(aPassword, user.Password))
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        //CRIAÇÂO DA TOKEN
        public static string GenerateJSONWebToken(User aUser, IConfiguration _config)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
            new Claim("admin", aUser.Admin.ToString()),
            new Claim("id", aUser.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
