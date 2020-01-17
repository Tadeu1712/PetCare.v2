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
using PetCareFinalVersion.Patterns;


namespace PetCareFinalVersion.Controllers
{

    [Route("api/animal")]
    [ApiController]
    public class AnimalController : Controller
    {
        private readonly AppDbContext _context;

        public AnimalController(AppDbContext context)
        {
            _context = context;
        }


        // CREATE NEW ANIMAL
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Animal aAnimal)
        {
            
            AbstractAnimalFactory facto = AnimalFactory.Instance;
            var x  =  (Animal)facto.CreateAnimalFromAnimalFactory( aAnimal.Name, aAnimal.Age, aAnimal.Weight, aAnimal.Type, aAnimal.Breed, aAnimal.Description, aAnimal.Association_id);
            
              try
              {
               // x.Status = x.StartToAdoption();
                 _context.Animals.Add(x);
                 _context.SaveChanges();
                  return Ok(x);
              }
              catch
              {
                  return BadRequest();
              }
        }


        // GET ALL ANIMALS 
        [Produces("application/json")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllAnimals()
        {
            try
            {
                var animals = _context.Animals.ToList();
                if (animals == null) return NotFound();
                else return Ok(animals);
            }
            catch
            {
                return BadRequest();
            }

        }

        [Produces("application/json")]
        [HttpGet("find/{id}")]
        public async Task<IActionResult> GetAnimal(int id)
        {
            try
            {
                Animal animal = _context.Animals.Find(id);
                animal.Association = _context.Associations.Find(animal.Association_id);
                return Ok(animal);
            }
            catch
            {
                return NotFound();
            }
        }

        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAnimal(int id)
        {
            try
            {
                var animal = _context.Animals.Find(id);
                _context.Animals.Remove(animal);
                _context.SaveChanges();
                return Ok("Deleted");
            }
            catch
            {
                return NotFound("Animal not found!");
            }
        }

        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateAssociation([FromBody]Animal aAnimal)
        {
            try
            {
                // VER ESTA PARTE
                //aAnimal.Status = aAnimal.StartAdopted();
                _context.Animals.Update(aAnimal);
                _context.SaveChanges();
                return Ok(aAnimal);
            }
            catch
            {
                return NotFound("Not Found!!!");
            }
        }
    }
}