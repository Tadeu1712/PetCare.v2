using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetCareFinalVersion.Models;
using Microsoft.AspNetCore.Authorization;
using PetCareFinalVersion.Patterns.FactoryPost;
using PetCareFinalVersion.Data;
using System.IO;
using PetCareFinalVersion.Patterns.TemplateMethod;


namespace PetCareFinalVersion.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly AbstractPostsFactory event_factory = EventFactory.Instance;
        protected AbstractTemplate ok = new ConcreteOk();
        protected AbstractTemplate notFound = new ConcreteNotFound();

        public EventsController(AppDbContext aContext)
        {
            _context = aContext;
        }

        [Produces("application/json")]
        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<IActionResult> getAllEvents()
        {
            try
            {
                var eventsList = await _context.Events.OrderBy(b => b.DateInit).ToListAsync();
                if (!eventsList.Any())
                {
                   
                    return NotFound(notFound.TemplateResponse("Não existem posts registados"));
                }

                object response = new { success = true, data = eventsList };
                return Ok(response);
            }
            catch
            {
                return BadRequest();
            }

        }

        [Produces("application/json")]
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> getEvents(int id)
        {
            try
            {
                Event singleEvent = await _context.Events.FindAsync(id);
                singleEvent.Association = await _context.Associations.FindAsync(singleEvent.Association_id);

                object response = new { success = true, data = singleEvent };
                return Ok(response);
            }
            catch
            {
                return NotFound(notFound.TemplateResponse($"Nao existe o post com o id {id}"));
            }
        }

        [Produces("application/json")]
        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> CreateEvent(Event aEvent)
        {
            var currentUser = HttpContext.User;
            try
            {
                if (currentUser.HasClaim(c => c.Type == "id"))
                {
                    int id = int.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "id").Value);
                    Association association = _context.Associations.Single(assoc => assoc.User_id == id);
                    var cEvent = (Event)event_factory.CreatePostFromPostFactory(aEvent);
                    cEvent.Association_id = association.Id;
                    cEvent.Location = aEvent.Location;
                    cEvent.DateInit = aEvent.DateInit;
                    cEvent.DateEnd = aEvent.DateEnd;
                    cEvent.Price = aEvent.Price;
                    cEvent.Type = aEvent.Type;
                    cEvent.Image = aEvent.Image;
                    await _context.Events.AddAsync(cEvent);
                    await _context.SaveChangesAsync();


                    object response = new {sucess = true, data = cEvent};
                    return Ok(response);
                }
               
                return NotFound(notFound.TemplateResponse("Utilizador não se encontra autenticado"));
            }
            catch
            {
                return BadRequest(notFound.TemplateResponse("Não foi possivel realizar a sua ação"));
            }
        }

        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePost(int id)
        {
            var currentUser = HttpContext.User;
            try
            {
                if (currentUser.HasClaim(c => c.Type == "id"))
                {
                    var aEvent = await _context.Events.FindAsync(id);
                    var id_log = int.Parse(currentUser.Claims.First(c => c.Type == "id").Value);
                    var association = _context.Associations.Where(assoc => assoc.User_id == id_log).Single();
                    if (aEvent.Association_id == association.Id)
                    {
                        _context.Events.Remove(aEvent);
                        await _context.SaveChangesAsync();

                      
                        return Ok(ok.TemplateResponse($"O evento com o id:{id} foi apagado"));
                    }
                    else
                    {
                        return Ok(notFound.TemplateResponse($"O Evento que tentou apagar não pertence à sua associação"));
                    }
                }
                return NotFound(notFound.TemplateResponse("Utilizador não se encontra autenticado"));
            }
            catch
            {
                return NotFound(notFound.TemplateResponse($"Nao existe o evento com o id {id}"));
            }
        }


        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPut("update")]
        [Authorize]
        public async Task<IActionResult> UpdateAssociation([FromBody]Event aEvent)
        {
            var currentUser = HttpContext.User;
            try
            {
                if (currentUser.HasClaim(c => c.Type == "id"))
                {
                    _context.Events.Update(aEvent);
                    await _context.SaveChangesAsync();

                    object response = new { success = true, data = aEvent };
                    return Ok(response);
                }
                return NotFound(notFound.TemplateResponse("Utilizador não se encontra autenticado"));
            }
            catch
            {
                return NotFound(notFound.TemplateResponse($"Nao foi possivel atualizar o post com o id {aEvent.Id}"));
            }
        }
        
    }
}