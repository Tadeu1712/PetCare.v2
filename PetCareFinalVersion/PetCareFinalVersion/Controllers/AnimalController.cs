using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetCareFinalVersion.Models;
using Microsoft.AspNetCore.Authorization;
using PetCareFinalVersion.Patterns;
using PetCareFinalVersion.Data;
using PetCareFinalVersion.Patterns.StateMachine;
using PetCareFinalVersion.Patterns.Observer;

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
            int id;
            object response;
            var currentUser = HttpContext.User;
            try
            {
                if (currentUser.HasClaim(c => c.Type == "id"))
                {
                  id = int.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "id").Value);
                  Association association = _context.Associations.Single(assoc => assoc.User_id == id);
                    
                  var animal  = (Animal)animal_factory.CreateAnimalFromAnimalFactory(data, association.Id);
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
                    var id_log = int.Parse(currentUser.Claims.First(c => c.Type == "id").Value);
                    if (animal.Association_id == id_log) {
                        _context.Animals.Remove(animal);
                        await _context.SaveChangesAsync();
                        response = new { success = true, message = $"Animal com o id {id} foi eliminado!" };
                        return Ok(response);
                    }
                    else
                    {
                        response = new { success = false, message = "Este animal não pertence à sua associação" };
                        return BadRequest(response);
                    }
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
                    AbstractStatus state= GetState(aAnimal.Id);
                    //////VER ESTADO DO OBJETO
                    //SetAnimalStatus(aAnimal);

                    if (aAnimal.Status == "Adotado")
                    {
                        aAnimal.Status = aAnimal.StartAdopted(state);
                    }
                    else if (aAnimal.Status == "Perdido")
                    {
                        aAnimal.Status = aAnimal.StartLosted(state);
                    }
                    else if(aAnimal.Status == "Adoção")
                    {
                        aAnimal.Status = aAnimal.StartToAdoption(state);
                    }
                    else
                    {
                        response = new { sucess = false, data = "Valor para o Status inválido" };
                        return BadRequest(response);
                    }
                    _context.Entry(await _context.Animals.FirstOrDefaultAsync(x => x.Id == aAnimal.Id)).CurrentValues.SetValues(aAnimal);
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
                var animalList = await _context.Animals.Where(animal => animal.Status == state).ToListAsync();
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
                var animalList = await _context.Animals.Where(animal => animal.Status == state).Where(animals => animals.Association_id == id).ToListAsync();
                response = new { success = true, data = animalList };
                return Ok(response);
            }
            catch
            {
                response = new { success = false, message = $"Nao tem animais com o estado {state}!" };
                return NotFound(response);
            }
        }

        [Produces("application/json")]
        [HttpGet("babies")]
        [AllowAnonymous]
        public async Task<IActionResult> GetBabies()
        {
            
            var endDate = DateTime.Now.AddYears(-2);
          
            object response;
            try
            {
                var animals = await _context.Animals.Where(obj => obj.Age >= endDate).OrderByDescending(b => b.Age).ToListAsync();

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

        public AbstractStatus GetState(int aAnimalId)
        {
            var previus_animal =  _context.Animals.Find(aAnimalId);
            AbstractStatus status;
            if(previus_animal.Status== "Perdido")
            {
                status = new Lost();
            }
            else if(previus_animal.Status == "Adotado")
            {
                status = new Adopted();
            }
            else
            {
                status = new Adoption();
            }

            return status;
        }
    }


}
