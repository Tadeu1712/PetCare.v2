using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetCareFinalVersion.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;


namespace PetCareFinalVersion.Controllers
{
    [Route("api/association")]
    [ApiController]
    public class AssociationController : Controller
    {
        private readonly AppDbContext _context;

        public AssociationController(AppDbContext context)
        {
            _context = context;
        }

        //DEVOLVER TODAS AS ASSOCIAÇÔES
        [Produces("application/json")]
        [HttpGet("all")]
      //  [Authorize]
      public async Task<IActionResult> AllAssociations()
      {
          object response;
            try
            {
                var associationsList = await _context.Associations.ToListAsync();
                if (!associationsList.Any())
                {
                    response = new { success = false, message = "Não tem associações registados" };
                    return NotFound(response);

                }

                response = new {success = true, data = associationsList};
                return Ok(response);
            }
            catch
            {
                return BadRequest();
            }

        }

        //DEVOLVER UMA ASSOCIAÇÂO
        [Produces("application/json")]
        [HttpGet("find/{id}")]
        //  [Authorize]
        public async Task<IActionResult> Association(int id)
        {
            object response;
            try
            {
                Association query = await _context.Associations.FindAsync(id);
                query.User = await _context.Users.FindAsync(query.User_id);
                query.User.Password = null;
                query.Posts = _context.Posts.Where(post => post.Association_id == id).ToList();
                query.Animals = _context.Animals.Where(animal => animal.Association_id == id).ToList();
                query.Events = _context.Events.Where(e => e.Association_id == id).ToList();

                response = new {success = true, data = query};
                return Ok(response);
            }
            catch
            {
                response = new { success = false, message = $"Nao foi possivel encontrar a associação com o id {id}" };
                return NotFound(response);

            }
        }

 
        //ELIMINAR UMA ASSOCIAÇÂO
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpDelete("delete/{id}")]
        //  [Authorize]
        public async Task<IActionResult> DeleteAssociation(int id)
        {
            object response;
            try
            {
                var association = await _context.Associations.FindAsync(id);
                _context.Users.Remove(association.User);
                _context.Associations.Remove(association);
                await _context.SaveChangesAsync();
                response = new {success = true, message = $"Associação com o id {id} foi eliminada "};
                return Ok(response);
            }
            catch
            {
                response = new { success = false, message = $"Nao foi possivel encontrar a associação com o id {id}" };
                return NotFound(response);
            }
        }

        //ATUALIZAR UMA ASSOCIAÇÂO
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPut("update")]
        //  [Authorize]
        public async Task<IActionResult> UpdateAssociation([FromBody]Association aAssociation)
        {
            object response;
            try
            {
                _context.Associations.Update(aAssociation);
                await _context.SaveChangesAsync();
                response = new {success = true, data = aAssociation};
                return Ok(response);
            }
            catch
            {
                response = new { success = false, message = $"Nao foi possivel encontrar a associação com o id {aAssociation.Id}" };
                return NotFound(response);
            }
        }
    }
}
