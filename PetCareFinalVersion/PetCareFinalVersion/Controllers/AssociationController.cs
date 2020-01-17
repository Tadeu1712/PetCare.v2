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

        [Produces("application/json")]
        [HttpGet("all")]
        public async Task<IActionResult> AllAssociations()
        {
            try
            {
                var json = _context.Associations.ToList();
                if (json == null)
                {

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
        public async Task<IActionResult> Association(int id)
        {
            try
            {
                Association query = _context.Associations.Find(id);
                query.User = _context.Users.Find(query.User_id);
                try
                {
                    query.Posts = _context.Posts.Where(post => post.Association_id == id).ToList();
                }
                catch
                {
                    query.Posts = new Post[0];
                }
                try
                {
                    query.Animals = _context.Animals.Where(animal => animal.Association_id == id).ToList();
                }
                catch
                {
                    query.Animals = new Animal[0];
                }
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
        public async Task<IActionResult> DeleteAssociation(int id)
        {
            try
            {
                var association = _context.Associations.Find(id);
                _context.Users.Remove(association.User);
                _context.Associations.Remove(association);
                _context.SaveChanges();
                return Ok("Deleted "+id);
            }
            catch
            {
                return NotFound("Association "+id+" not found!");
            }
        }

        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPut("update")]
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
                return NotFound("Not Found!!!");
            }
        }
    }
}
