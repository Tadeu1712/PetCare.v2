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

namespace PetCareFinalVersion.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;
        private IConfiguration _config;

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
                    var tokenString = Auth.GenerateJSONWebToken(user, _config);
                    var createToken = new { token = tokenString };

               
                    return  Ok(createToken);
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

        [HttpGet("values")]
        [Authorize]

        public ActionResult<IEnumerable<string>> Get()
        {
            var currentUser = HttpContext.User;
            string email;
            bool admin;
            int id;

            if (currentUser.HasClaim(c => c.Type == "id") && currentUser.HasClaim(c => c.Type == "admin"))
            {
                 id = int.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "id").Value);
                 admin = bool.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "admin").Value);

                return Ok(admin);

            }

            return BadRequest();
        }




        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAssociation([FromBody]Association aAssociation)
        {
            try
            {
                var queryUser = new User()
                {
                    Name = aAssociation.User.Name,
                    Email = aAssociation.User.Email,
                    Password = BCrypt.Net.BCrypt.EnhancedHashPassword(aAssociation.User.Password),
                    Admin = false,
                };


                var queryAssociation = new Association()
                {
                    Iban = aAssociation.Iban,
                    Adress = aAssociation.Adress,
                    PhoneNumber = aAssociation.PhoneNumber,
                    Description = aAssociation.Description,
                    FoundationDate = aAssociation.FoundationDate,
                    User = queryUser,
                };

                _context.Associations.Add(queryAssociation);
                _context.SaveChanges();

                return Ok(queryAssociation);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
