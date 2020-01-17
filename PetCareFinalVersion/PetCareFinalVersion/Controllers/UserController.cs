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

        // UserController Constructor
        public UserController(AppDbContext context, IConfiguration config)
        {
            _context = context;
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


    }
}
