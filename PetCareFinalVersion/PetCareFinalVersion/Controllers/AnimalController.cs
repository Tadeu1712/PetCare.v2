using System;
using System.Collections.Generic;
using System.IO;
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
using PetCareFinalVersion.Patterns;
using PetCareFinalVersion.Data;

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
        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var files = Request.Form.Files;
            var data = Request.Form;
            object response;
            var currentUser = HttpContext.User;
            var animal  = (Animal)animal_factory.CreateAnimalFromAnimalFactory(data);
            
              try
              {
                if (currentUser.HasClaim(c => c.Type == "id"))
                {
                      animal.Image = ImageSave.SaveImage(files, "animal");
                      await _context.Animals.AddAsync(animal);
                      await _context.SaveChangesAsync();
                      response = new {success = true, data = animal};
                      return Ok(response);
                }
              response = new { success = false, message = "Utilizador não se encontra autenticado" };
              return NotFound(response);
              }
              catch
              {
                  return BadRequest();
              }
        }


        // GET ALL ANIMALS 
        [Produces("application/json")]
        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAnimals()
        {
            object response;
            try
            {
                var animals = await _context.Animals.OrderByDescending(b =>b.Id).ToListAsync();
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
        [AllowAnonymous]
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
          [Authorize]
        public async Task<IActionResult> DeleteAnimal(int id)
        {
            object response;
            var currentUser = HttpContext.User;

            try
            {
                if (currentUser.HasClaim(c => c.Type == "id"))
                {
                    var animal = await _context.Animals.FindAsync(id);
                    _context.Animals.Remove(animal);
                    await _context.SaveChangesAsync();
                    response = new {success = true, message = $"Animal com o id {id} foi eliminado!"};
                    return Ok(response);
                }
                response = new { success = false, message = "Utilizador não se encontra autenticado" };
                return NotFound(response);
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
        [Authorize]
        public async Task<IActionResult> UpdateAssociation([FromBody]Animal aAnimal)
        {
            object response;
            var currentUser = HttpContext.User;

            try
            {
                if (currentUser.HasClaim(c => c.Type == "id"))
                {
                    //VER ESTADO DO OBJETO
                    if (aAnimal.Status == "Adotado")
                    {
                        aAnimal.Status = aAnimal.StartAdopted();
                    }
                    else if (aAnimal.Status == "Perdido")
                    {
                        aAnimal.Status = aAnimal.StartLosted();
                    }
                    else
                    {
                        aAnimal.Status = aAnimal.StartToAdoption();
                    }

                    _context.Animals.Update(aAnimal);
                    await _context.SaveChangesAsync();
                    response = new {success = true, data = aAnimal};

                    return Ok(response);
                }
                response = new { success = false, message = "Utilizador não se encontra autenticado" };
                return NotFound(response);
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
        [AllowAnonymous]
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

        //IGUAL MAS APENAS PARA UMA ASSOCIAÇAo
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpGet("state/{state}/association/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetStateAnimalsByAssoc(string state, int id)
        {
            object response;
            try
            {
                var animalList = _context.Animals.Where(animal => animal.Status == state).Where(animals => animals.Association_id == id).ToList();
                response = new { success = true, data = animalList };
                return Ok(response);
            }
            catch
            {
                response = new { success = false, message = $"Nao tem animais com o estado {state}!" };
                return NotFound(response);
            }
        }

        //ACABAR ISTO

        [Produces("application/json")]
        [HttpGet("babies")]
        [AllowAnonymous]
        public async Task<IActionResult> GetBabies()
        {
            object response;
            try
            {
                var animals = await _context.Animals.OrderByDescending(b => b.Age).ToListAsync();
                var dataNow = DateTime.Now;
                foreach(var value in animals)
                {
                    
                    int result = DateTime.Compare(dataNow , value.Age);
                   

                    if (result < 0)
                    {
                        //is earlier than
                    }else if(result == 0){
                        //is the same time as
                    }
                    else
                    {
                        //is later than
                    }
                }
               
                if (!animals.Any())
                {
                    response = new { success = false, message = "Não tem animais registados" };
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
        // ROUTE GET POST IMAGES 
        [HttpGet("img/{imgName}")]
        [AllowAnonymous]

        public async Task<IActionResult> GetImgAsync(string imgName)
        {
            var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Resources/images/animal", imgName);
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
