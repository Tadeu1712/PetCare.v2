using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PetCareFinalVersion.Models;
using System.Text.Json;
using System.Collections.Generic;

namespace PetCareFinalVersion.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("all")]
        public string allUsers()
        {
            var json = _context.Users.ToList();
            if (json == null){
               
            }
            return JsonSerializer.Serialize(json);
        }

        [HttpGet("{id}")]
        public string allUsers(int id)
        {
            var json = _context.Users.Find(id);

            if(json == null)
            {
                
            }

            return JsonSerializer.Serialize(json);
        }

        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost("create")]
        public User CreateUser([FromBody] User obj)
        {
              var queryUser = new User()
            {
                Name = obj.Name,
                Email = obj.Email,
                Password = obj.Password,
                Admin = true,
            };
            
            _context.Users.Add(queryUser);
            _context.SaveChanges();
            return queryUser;
        }

    }
}
