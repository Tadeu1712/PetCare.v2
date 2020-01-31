using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PetCareFinalVersion.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using PetCareFinalVersion.Data;
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

        //LOGIN
        [Produces("application/json")]
        [Consumes("application/json")]
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(Login aLogin)
        {
            object response;
            try
            {
               
                var user = Auth.Login(aLogin.email, aLogin.password, _context);
                if (user != null)
                {
        
                    var tokenString = Auth.GenerateJSONWebToken(user, _config);
                    response = new {data = user, token = tokenString};

                    return  Ok(response);
                }
                else
                {
                    response = new { success = false, message = "Wrong password" };
                    return NotFound(response);
                }

            }
            catch
            {
                response = new { success = false, message = "Wrong email" };
                return NotFound(response);
            }
        }

        //DEVOLVE O ATUAL -> UTILIZANDO A TOKEN
        [HttpGet("actual")]
        [Authorize]
        public async Task<IActionResult> GetActual()
        {
            var currentUser = HttpContext.User;
            object response;
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

                response = new {success = true, data = association};
                return Ok(response);

            }

            response = new { success = false, message = "Nao foi possivel encontrar o utilizador atual" };
            return NotFound(response);
        }

        //REGISTO
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAssociation([FromBody]Association aAssociation)
        {
            object response;
            var currentUser = HttpContext.User;

            var newUser = (Association)assoc_factory.CreateAssociationFromAssocFactory(aAssociation);

            try
            {
                if (currentUser.HasClaim(c => c.Type == "id") && bool.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "admin").Value))
                {
                    await _context.Associations.AddAsync(newUser);
                    await _context.SaveChangesAsync();

                    newUser.User.Password = null;
                    response = new {success = true, data = newUser};
                    return Ok(response);
                }
                
                response = new { success = false, message = "Utilizador não se encontra autenticado" };
                return NotFound(response);
            }
            catch
            {
                response = new { success = false, message = "Nao foi possivel criar um novo registo" };
                return NotFound(response);
            }
            
        }
    }
}
