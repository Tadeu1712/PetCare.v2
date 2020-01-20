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
using Microsoft.EntityFrameworkCore;

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
                var json = await _context.Users.ToListAsync();
                if (!json.Any())
                {
                    var rs = new { success = false, message = "Nao existe utilizadores registados" };
                    return NotFound(rs);
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
                User query = await _context.Users.FindAsync(id);

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
                var user = await _context.Users.FindAsync(id);
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return Ok("Deleted " + id);
            }
            catch
            {
                var rs = new { success = false, message = $"Nao foi possivel eliminar o utilizador com o id {id}" };
                return NotFound(rs);
           
            }
        }


    }
}
