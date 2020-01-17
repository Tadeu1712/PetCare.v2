using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PetCareFinalVersion.Models;
using System.Text.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using PetCareFinalVersion.Data;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using BCrypt;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using PetCareFinalVersion.Patterns.FactoryAssoc;

namespace PetCareFinalVersion.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;
        private IConfiguration _config;
        private readonly AbstractAssocFactory assoc_factory = AssociationFactory.Instance;


        // UserController Constructor
        public AuthController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [Produces("application/json")]
        [Consumes("application/json")]
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(Login aLogin)
        {
            try
            {
                IActionResult response = Unauthorized();
                var user = Auth.Login(aLogin.email, aLogin.pass, _context);
                if (user != null)
                {
                    var tokenString = GenerateJSONWebToken(aLogin);
                    _context.SaveChanges();
                    response = Ok(user);
                    return response;
                }
                else
                {
                    return BadRequest("Wrong Password");
                }

            }
            catch
            {
                return BadRequest("Wrong email");
            }
        }

        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAssociation([FromBody]Association aAssociation)
        {
            var newUser = (Association)assoc_factory.CreateAssociationFromAssocFactory(aAssociation);

            try
            {
              
              _context.Associations.Add(newUser);
                _context.SaveChanges();

                newUser.User.Password = null;
                return Ok(newUser);
            }
            catch
            {
                return BadRequest();
            }
        }

        private string GenerateJSONWebToken(Login aLogin)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                null,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}