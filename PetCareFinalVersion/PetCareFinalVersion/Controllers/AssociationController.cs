using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetCareFinalVersion.Models;
using Microsoft.AspNetCore.Authorization;
using PetCareFinalVersion.Patterns.TemplateMethod;


namespace PetCareFinalVersion.Controllers
{
    [Route("api/association")]
    [ApiController]
    public class AssociationController : Controller
    {
        private readonly AppDbContext _context;
        protected AbstractTemplate ok = new ConcreteOk();
        protected AbstractTemplate notFound = new ConcreteNotFound();

        public AssociationController(AppDbContext context)
        {
            _context = context;
        }

        //DEVOLVER TODAS AS ASSOCIAÇÔES
        [Produces("application/json")]
        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<IActionResult> AllAssociations()
          {
              
                try
                {
                    var associationsList = await _context.Associations.ToListAsync();
                    foreach (Association assoc in associationsList)
                    {
                        assoc.User = _context.Users.Find(assoc.User_id);
                        assoc.User.Password = "";
                    }
                    if (!associationsList.Any())
                    {
                        return NotFound(notFound.TemplateResponse("Não tem associações registados"));
                    }

                    object response = new {success = true, data = associationsList};
                    return Ok(response);
                }
                catch
                {
                    return BadRequest();
                }
          }

        //DEVOLVER UMA ASSOCIAÇÂO
        [Produces("application/json")]
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Association(int id)
        {
            try
            {
                Association assoc = await _context.Associations.FindAsync(id);
                assoc.User = await _context.Users.FindAsync(assoc.User_id);
                assoc.User.Password = null;
                assoc.Posts = _context.Posts.Where(post => post.Association_id == id).ToList();
                assoc.Animals = _context.Animals.Where(animal => animal.Association_id == id).ToList();
                assoc.Events = _context.Events.Where(e => e.Association_id == id).ToList();

                object response = new {success = true, data = assoc};
                return Ok(response);
            }
            catch
            {
                return NotFound(notFound.TemplateResponse($"Nao foi possivel encontrar a associação com o id {id}"));
            }
        }

 
        //ELIMINAR UMA ASSOCIAÇÂO
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpDelete("{id}")] 
        [Authorize]
        public async Task<IActionResult> DeleteAssociation(int id)
        {

            var currentUser = HttpContext.User;
            try
            {
                if (currentUser.HasClaim(c => c.Type == "id") && bool.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "admin").Value))
                {

                    var association = await _context.Associations.FindAsync(id);
                    _context.Users.Remove(association.User);
                    _context.Associations.Remove(association);
                    await _context.SaveChangesAsync();
                   
                    return Ok(ok.TemplateResponse($"Associação com o id {id} foi eliminada "));
                }
                
                return NotFound(notFound.TemplateResponse("Utilizador não se encontra autenticado"));
            }
            catch
            {
                return NotFound(notFound.TemplateResponse($"Nao foi possivel encontrar a associação com o id {id}"));
            }
        }

        //ATUALIZAR UMA ASSOCIAÇÂO
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPut("update")]
        [Authorize]
        public async Task<IActionResult> UpdateAssociation([FromBody]Association aAssociation)
        {

            var currentUser = HttpContext.User;
            try
            {
                if (currentUser.HasClaim(c => c.Type == "id"))
                {

                    _context.Associations.Update(aAssociation);
                    await _context.SaveChangesAsync();
                    object response = new {success = true, data = aAssociation};
                    return Ok(response);
                }
                return NotFound(notFound.TemplateResponse("Utilizador não se encontra autenticado"));
                
            }
            catch
            {
                return NotFound(notFound.TemplateResponse($"Nao foi possivel encontrar a associação com o id {aAssociation.Id}"));
            }
        }
    }
}
