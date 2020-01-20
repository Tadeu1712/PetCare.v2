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
        private readonly AbstractAnimalFactory animal_factory = AnimalFactory.Instance;

        public AnimalController(AppDbContext context)
        {
            _context = context;
        }


        // CREATE NEW ANIMAL
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost("create")]
        //  [Authorize]
        public async Task<IActionResult> Create([FromBody] Animal aAnimal)
        {
            var animal  = (Animal)animal_factory.CreateAnimalFromAnimalFactory(aAnimal);
            
              try
              {
                  _context.Animals.Add(animal);
                 _context.SaveChanges();
                  return Ok(animal);
              }
              catch
              {
                  return BadRequest();
              }
        }


        // GET ALL ANIMALS 
        [Produces("application/json")]
        [HttpGet("all")]
        //  [Authorize]
        public async Task<IActionResult> GetAllAnimals()
        {
            try
            {
                var animals = _context.Animals.ToList();
                if (!animals.Any())
                {
                    var rs = new { success = false,message = "Não tem animais registados" }; 
                    return NotFound(rs);

                }
                return Ok(animals);
            }
            catch
            {
                return BadRequest();
            }

        }

        [Produces("application/json")]
        [HttpGet("find/{id}")]
        //  [Authorize]
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
                var rs = new { success = false, message = $"Animal com o id {id} nao encontrado!" };
                return NotFound(rs);
            }
        }

        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpDelete("delete/{id}")]
        //  [Authorize]
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
                var rs = new { success = false, message = $"Animal com o id {id} nao encontrado!" };
                return NotFound(rs);
            }
        }

        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPut("update")]
        //  [Authorize]
        public async Task<IActionResult> UpdateAssociation([FromBody]Animal aAnimal)
        {
            try
            {
                //VER ESTADO DO OBJETO
                //aAnimal.Status = aAnimal.StartAdopted();
                _context.Animals.Update(aAnimal);
                _context.SaveChanges();
                return Ok(aAnimal);
            }
            catch
            {
                var rs = new { success = false, message = $"Nao foi possivel atualizar o animal com o id {aAnimal.Id}" };
                return NotFound(rs);
            }
        }

        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpGet("state/{state}")]
        public async Task<IActionResult> GetStateAnimals(string state)
        {
            try
            {
                var animalList = _context.Animals.Where(animal => animal.Status == state).ToList();
                return Ok(animalList);
            }
            catch
            {
                var rs = new { success = false, message = $"Nao tem animais com o estado {state}!" };
                return NotFound(rs);
            }
        }

    }
}