﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetCareFinalVersion.Models;
using Microsoft.AspNetCore.Authorization;
using PetCareFinalVersion.Patterns.FactoryPost;
using PetCareFinalVersion.Data;
using System.IO;


namespace PetCareFinalVersion.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly AbstractPostsFactory event_factory = EventFactory.Instance;

        public EventsController(AppDbContext aContext)
        {
            _context = aContext;
        }

        [Produces("application/json")]
        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<IActionResult> getAllEvents()
        {
            object response;
            try
            {
                var eventsList = await _context.Events.OrderBy(b => b.DateInit).ToListAsync();
                if (!eventsList.Any())
                {
                    response = new { success = true, message = "Não existem posts registados" };
                    return NotFound(response);
                }

                response = new { success = true, data = eventsList };
                return Ok(response);
            }
            catch
            {
                return BadRequest();
            }

        }

        [Produces("application/json")]
        [HttpGet("find/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> getEvents(int id)
        {
            object response;
            try
            {
                Event singleEvent = await _context.Events.FindAsync(id);
                singleEvent.Association = await _context.Associations.FindAsync(singleEvent.Association_id);

                response = new { success = true, data = singleEvent };
                return Ok(response);
            }
            catch
            {
                response = new { success = false, message = $"Nao existe o post com o id {id}" };
                return NotFound(response);
            }
        }

        [Produces("application/json")]
        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> CreateEvent([FromForm] string title, [FromForm] string description, [FromForm] string location, [FromForm] string dateInit, [FromForm] string dateEnd, [FromForm] decimal price, [FromForm] string type)
        {
            object response;
            var currentUser = HttpContext.User;
            var files = Request.Form.Files;
            int id;
            try
            {
                if (currentUser.HasClaim(c => c.Type == "id"))
                {
                    id = int.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "id").Value);
                    Association association = _context.Associations.Single(assoc => assoc.User_id == id);

                    var cEvent = (Event)event_factory.CreatePostFromPostFactory(title, description);
                    cEvent.Association_id = association.Id;
                    cEvent.Image = ImageSave.SaveImage(files, "event");
                    cEvent.Location = location;
                    var initDate = DateTime.Parse(dateInit);
                    var endDate = DateTime.Parse(dateEnd);
                    cEvent.DateInit = initDate;
                    cEvent.DateEnd = endDate;
                    cEvent.Price = price;
                    cEvent.Type = "Concentração de Cães";
                    cEvent.Type = type;
                    await _context.Events.AddAsync(cEvent);
                    await _context.SaveChangesAsync();


                    response = new {sucess = true, data = cEvent};
                    return Ok(response);
                }
                response = new { success = false, message = "Utilizador não se encontra autenticado" };
                return NotFound(response);
            }
            catch
            {
                response = new { sucess = false, message = "Não foi possivel realizar a sua ação" };
                return BadRequest(response);
            }
        }

        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpDelete("delete/{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePost(int id)
        {
            object response;
            var currentUser = HttpContext.User;
           
            try
            {
                if (currentUser.HasClaim(c => c.Type == "id"))
                {
                    var dEvent = await _context.Events.FindAsync(id);
                    _context.Events.Remove(dEvent);
                    await _context.SaveChangesAsync();

                    response = new { success = true, message = $"O evento com o id:{id} foi apagado" };
                    return Ok(response);
                }

                response = new { success = false, message = "Utilizador não se encontra autenticado" };
                return NotFound(response);
            }
            catch
            {
                var rs = new { success = false, message = $"Nao existe o evento com o id {id}" };
                return NotFound(rs);

            }
        }


        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPut("update")]
        [Authorize]
        public async Task<IActionResult> UpdateAssociation([FromBody]Event aEvent)
        {
            object response;
            var currentUser = HttpContext.User;

            try
            {
                if (currentUser.HasClaim(c => c.Type == "id"))
                {
                    _context.Events.Update(aEvent);
                    await _context.SaveChangesAsync();

                    response = new { success = true, data = aEvent };
                    return Ok(response);
                }

                response = new { success = false, message = "Utilizador não se encontra autenticado" };
                return NotFound(response);
            }
            catch
            {
                var rs = new { success = false, message = $"Nao foi possivel atualizar o post com o id {aEvent.Id}" };
                return NotFound(rs);
            }
        }
        // ROUTE GET POST IMAGES 
        [HttpGet("img/{imgName}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetImgAsync(string imgName)
        {
           var path = "Resources/images/event/"+ imgName;
           var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            memory.Position = 0;
            return Ok(memory);
        }
    }
}