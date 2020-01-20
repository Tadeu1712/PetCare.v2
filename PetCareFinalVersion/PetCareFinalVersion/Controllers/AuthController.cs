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
using Newtonsoft.Json;
using PetCareFinalVersion.Patterns.FactoryAssoc;
using System.Text.Json.Serialization;

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

        //LOGIN
        [Produces("application/json")]
        [Consumes("application/json")]
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(Login aLogin)
        {
            try
            {
               
                var user = Auth.Login(aLogin.email, aLogin.password, _context);
                if (user != null)
                {
        
                    var tokenString = Auth.GenerateJSONWebToken(user, _config);
                    var obj = new {data = user, token = tokenString};

                    return  Ok(obj);
                }
                else
                {
                    var rs = new { success = false, message = "Wrong password" };
                    return NotFound(rs);
                }

            }
            catch
            {
                var rs = new { success = false, message = "Wrong email" };
                return NotFound(rs);
            }
        }

        //DEVOLVE O ATUAL -> UTILIZANDO A TOKEN
        [HttpGet("actual")]
        [Authorize]
        public async Task<IActionResult> GetActual()
        {
            var currentUser = HttpContext.User;
           
            int id;
          
            if (currentUser.HasClaim(c => c.Type == "id"))
            {
                id = int.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "id").Value);

                Association association = _context.Associations.Single(assoc => assoc.User_id == id);
                association.User = await _context.Users.FindAsync(id);
                association.User.Password = null;
                association.Posts = _context.Posts.Where(post => post.Association_id == association.Id).ToList();
                association.Animals = _context.Animals.Where(animal => animal.Association_id == association.Id).ToList();
                association.Events = _context.Events.Where(e => e.Association_id == association.Id).ToList();

                return Ok(association);

            }

            var rs = new { success = false, message = "Nao foi possivel encontrar o utilizador atual" };
            return NotFound(rs);
        }

        //REGISTO
        [Produces("application/json")]
        [Consumes("application/json")]
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAssociation([FromBody]Association aAssociation)
        {
            var newUser = (Association)assoc_factory.CreateAssociationFromAssocFactory(aAssociation);

            try
            {
              
              await _context.Associations.AddAsync(newUser);
               await _context.SaveChangesAsync();

                newUser.User.Password = null;
                return Ok(newUser);
               
            }
            catch
            {
                var rs = new { success = false, message = "Nao foi possivel criar um novo registo" };
                return NotFound(rs);
            }
            
        }


        // VAI SER APAGADO
        //TESTE
        [HttpGet("values")]
        [Authorize]
        public ActionResult<IEnumerable<string>> Get()
        {
            var currentUser = HttpContext.User;
            bool admin;
            int id;
            IActionResult response = Unauthorized();
            if (currentUser.HasClaim(c => c.Type == "id") && currentUser.HasClaim(c => c.Type == "admin"))
            {
                id = int.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "id").Value);
                admin = bool.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "admin").Value);

                return Ok(id);

            }

            return BadRequest();
        }
    }
}
