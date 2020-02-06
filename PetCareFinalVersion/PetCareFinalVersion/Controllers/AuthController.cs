using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PetCareFinalVersion.Models;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using PetCareFinalVersion.Data;
using Microsoft.Extensions.Configuration;
using PetCareFinalVersion.Patterns.FactoryAssoc;
using PetCareFinalVersion.Patterns.TemplateMethod;

namespace PetCareFinalVersion.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;
        private IConfiguration _config;
        private readonly AbstractAssocFactory assoc_factory = AssociationFactory.Instance;
        protected AbstractTemplate ok = new ConcreteOk();
        protected AbstractTemplate notFound = new ConcreteNotFound();

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
                    object response = new {data = user, token = tokenString};
                    return  Ok(response);
                }
                else
                {
                    return NotFound(notFound.TemplateResponse("Wrong password"));
                }
            }
            catch
            {
                return NotFound(notFound.TemplateResponse("Wrong email"));
            }
        }

        //DEVOLVE O ATUAL -> UTILIZANDO A TOKEN
        [HttpGet("actual")]
        [Authorize]
        public async Task<IActionResult> GetActual()
        {
            var currentUser = HttpContext.User;
            int id;

            try
            {
                if (currentUser.HasClaim(c => c.Type == "id") && bool.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "admin").Value) == false)
                {
                    id = int.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "id").Value);

                    Association association = _context.Associations.Single(assoc => assoc.User_id == id);
                    association.User = await _context.Users.FindAsync(id);
                    association.User.Password = null;
                    association.Posts = _context.Posts.Where(post => post.Association_id == association.Id).ToList();
                    association.Animals = _context.Animals.Where(animal => animal.Association_id == association.Id).ToList();
                    association.Events = _context.Events.Where(e => e.Association_id == association.Id).ToList();

                    object response = new { success = true, data = association };
                    return Ok(response);

                }
                else
                {
                    id = int.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "id").Value);

                    User user = _context.Users.Single(users => users.Id == id);
                    user = await _context.Users.FindAsync(id);
                    user.Password = null;

                    object response = new { success = true, data = user };
                    return Ok(response);
                }
            }
            catch
            {
                return NotFound(notFound.TemplateResponse("Nao foi possivel encontrar o utilizador atual"));
            }
           


           
        }

        //REGISTO
        [Produces("application/json")]
        [Authorize]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAssociation()
        {
            var currentUser = HttpContext.User;
         //   var files = Request.Form.Files;
            var data = Request.Form;
            try
            {
                if (currentUser.HasClaim(c => c.Type == "id") && bool.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "admin").Value))
                {
                    var newUser = (Association)assoc_factory.CreateAssociationFromAssocFactory(data);

                    await _context.Associations.AddAsync(newUser);
                    await _context.SaveChangesAsync();

                    newUser.User.Password = null;
                    object response = new {success = true, data = newUser};
                    return Ok(response);
                }
                return NotFound(notFound.TemplateResponse("Utilizador não se encontra autenticado"));
            }
            catch
            {
                return NotFound(notFound.TemplateResponse("Nao foi possivel criar um novo registo"));
            }
            
        }
    }
}
