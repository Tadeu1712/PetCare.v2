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
            object response;

            var animal  = (Animal)animal_factory.CreateAnimalFromAnimalFactory(aAnimal);
            
              try
              {
                  await _context.Animals.AddAsync(animal);
                 await _context.SaveChangesAsync();
                 response = new {success = true, data = animal};
                  return Ok(response);
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
            object response;
            try
            {
                var animals = await _context.Animals.ToListAsync();
                if (!animals.Any())
                {
                    response = new { success = false,message = "Não tem animais registados" }; 
                    return NotFound(response);

                }
                response = new { success = true, data = animals };
                return Ok(response);
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
            object response;
            try
            {
                Animal animal = await _context.Animals.FindAsync(id);
                animal.Association = await _context.Associations.FindAsync(animal.Association_id);
                response = new {success = true, data = animal};
                return Ok(response);
            }
            catch
            {
                response = new { success = false, message = $"Animal com o id {id} nao encontrado!" };
                return NotFound(response);
            }
        }

        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpDelete("delete/{id}")]
        //  [Authorize]
        public async Task<IActionResult> DeleteAnimal(int id)
        {
            object response;
            try
            {
                var animal = await _context.Animals.FindAsync(id);
                _context.Animals.Remove(animal);
                await _context.SaveChangesAsync();
                response = new { success = true, message = $"Animal com o id {id} foi eliminado!" };
                return Ok(response);
            }
            catch
            {
                response = new { success = false, message = $"Animal com o id {id} nao encontrado!" };
                return NotFound(response);
            }
        }

        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPut("update")]
        //  [Authorize]
        public async Task<IActionResult> UpdateAssociation([FromBody]Animal aAnimal)
        {
            object response;
            try
            {
                //VER ESTADO DO OBJETO
                _context.Animals.Update(aAnimal);
                await _context.SaveChangesAsync();
                response = new { success = true,data = aAnimal };

                return Ok(response);
            }
            catch
            {
                response = new { success = false, message = $"Nao foi possivel atualizar o animal com o id {aAnimal.Id}" };
                return NotFound(response);
            }
        }

        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpGet("state/{state}")]
        public async Task<IActionResult> GetStateAnimals(string state)
        {
            object response;
            try
            {
                var animalList = _context.Animals.Where(animal => animal.Status == state).ToList();
                response = new { success = true, data = animalList };
                return Ok(response);
            }
            catch
            {
                response = new { success = false, message = $"Nao tem animais com o estado {state}!" };
                return NotFound(response);
            }
        }

    }
}