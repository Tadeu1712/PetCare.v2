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
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly AppDbContext _context;
        private IConfiguration _config;

        // UserController Constructor
        public UserController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        //Api que devolve todos os users em formato JSON
        [Produces("application/json")]
        [HttpGet("all")]
        public async Task<IActionResult> AllUsers()
        {
            try
            {
                var json = _context.Users.ToList();
                if (json == null)
                {

                }
                return Ok(json);
            }
            catch
            {
                return BadRequest();
            }

        }

        [Produces("application/json")]
        [HttpGet("find/{id}")]
        public async Task<IActionResult> User(int id)
        {
            try
            {
                User query = _context.Users.Find(id);

                return Ok(query);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]User aUser)
        {
            try
            {

                var queryUser = new User()
                {
                    Name = aUser.Name,
                    Email = aUser.Email,
                    Password = BCrypt.Net.BCrypt.EnhancedHashPassword(aUser.Password),
                    Admin = false,
                };

                _context.Users.Add(queryUser);
                _context.SaveChanges();

                return Ok(queryUser);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var user = _context.Users.Find(id);
                _context.Users.Remove(user);
                _context.SaveChanges();
                return Ok("Deleted " + id);
            }
            catch
            {
                return NotFound("User " + id + " not found!");
            }
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
                    user.Token = tokenString;
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
