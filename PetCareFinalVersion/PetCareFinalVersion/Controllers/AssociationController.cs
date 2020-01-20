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
            try
            {
                var associationsList = _context.Associations.ToList();
                if (!associationsList.Any())
                {
                    var rs = new { success = false, message = "Não tem associações registados" };
                    return NotFound(rs);

                }
                return Ok(associationsList);
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
            try
            {
                Association query = _context.Associations.Find(id);
                query.User = _context.Users.Find(query.User_id);
                query.User.Password = null;
                query.Posts = _context.Posts.Where(post => post.Association_id == id).ToList();
                query.Animals = _context.Animals.Where(animal => animal.Association_id == id).ToList();
                query.Events = _context.Events.Where(e => e.Association_id == id).ToList();

                return Ok(query);
            }
            catch
            {
                var rs = new { success = false, message = $"Nao foi possivel encontrar a associação com o id {id}" };
                return NotFound(rs);

            }
        }

 
        //ELIMINAR UMA ASSOCIAÇÂO
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpDelete("delete/{id}")]
        //  [Authorize]
        public async Task<IActionResult> DeleteAssociation(int id)
        {
            try
            {
                var association = _context.Associations.Find(id);
                _context.Users.Remove(association.User);
                _context.Associations.Remove(association);
                _context.SaveChanges();
                return Ok($"Associação com o id {id} foi eliminada ");
            }
            catch
            {
                var rs = new { success = false, message = $"Nao foi possivel encontrar a associação com o id {id}" };
                return NotFound(rs);
            }
        }

        //ATUALIZAR UMA ASSOCIAÇÂO
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPut("update")]
        //  [Authorize]
        public async Task<IActionResult> UpdateAssociation([FromBody]Association aAssociation)
        {
            try
            {
                _context.Associations.Update(aAssociation);
                _context.SaveChanges();
                return Ok(aAssociation);
            }
            catch
            {
                var rs = new { success = false, message = $"Nao foi possivel encontrar a associação com o id {aAssociation.Id}" };
                return NotFound(rs);
            }
        }
    }
}
