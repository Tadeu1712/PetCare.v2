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
using PetCareFinalVersion.Patterns.TemplateMethod;


namespace PetCareFinalVersion.Controllers
{

    [Route("api/animal")]
    [ApiController]
    public class AnimalController : Controller
    {
        private readonly AppDbContext _context;
        private readonly AbstractAnimalFactory animal_factory = AnimalFactory.Instance;

        protected AbstractTemplate ok = new ConcreteOk();
        protected AbstractTemplate notFound = new ConcreteNotFound();

        public AnimalController(AppDbContext context)
        {
            _context = context;
        }


        // CREATE NEW ANIMAL
        [Produces("application/json")]
        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> Create(Animal aAnimal)
        {
            var files = Request.Form.Files;
            object response;
            var currentUser = HttpContext.User;
            try
            {
                if (currentUser.HasClaim(c => c.Type == "id"))
                {

                    int id = int.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "id").Value);
                    Association association = _context.Associations.Single(assoc => assoc.User_id == id);
                    var animal = (Animal)animal_factory.CreateAnimalFromAnimalFactory(aAnimal);
                    animal.Image = ImageSave.SaveImage(files, "event");
                    await _context.Animals.AddAsync(animal);
                    await _context.SaveChangesAsync();
                    response = new { success = true, data = animal };
                    return Ok(response);

                }
                return NotFound(notFound.TemplateResponse("Utilizador não se encontra autenticado"));
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
            try
            {
                var animals = await _context.Animals.OrderByDescending(b => b.Id).ToListAsync();
                if (!animals.Any())
                {
                    return NotFound(notFound.TemplateResponse("Não tem animais registados"));
                }
                foreach (Animal animal in animals)
                {
                    animal.Association = await _context.Associations.FindAsync(animal.Association_id);
                    animal.Association.User = await _context.Users.FindAsync(animal.Association.User_id);
                    animal.Association.Animals = null;
                }
                object response = new { success = true, data = animals };
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
        public async Task<IActionResult> GetAnimal(int id)
        {
            try
            {
                Animal animal = await _context.Animals.FindAsync(id);
                animal.Association = await _context.Associations.FindAsync(animal.Association_id);
                animal.Association.User = await _context.Users.FindAsync(animal.Association.User_id);
                object response = new { success = true, data = animal };
                return Ok(response);
            }
            catch
            {
                return NotFound(notFound.TemplateResponse($"Animal com o id {id} nao encontrado!"));
            }
        }

        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteAnimal(int id)
        {
            var currentUser = HttpContext.User;
            try
            {
                if (currentUser.HasClaim(c => c.Type == "id"))
                {
                    var animal = await _context.Animals.FindAsync(id);
                    var id_log = int.Parse(currentUser.Claims.First(c => c.Type == "id").Value);
                    var association = _context.Associations.Where(assoc => assoc.User_id == id_log).Single();
                    if (animal.Association_id == association.Id)
                    {
                        _context.Animals.Remove(animal);
                        await _context.SaveChangesAsync();
                        return Ok(ok.TemplateResponse($"Animal com o id {id} foi eliminado!"));
                    }
                    else
                    {
                        return BadRequest(notFound.TemplateResponse("Este animal não pertence à sua associação"));
                    }
                }
                return NotFound(notFound.TemplateResponse("Utilizador não se encontra autenticado"));
            }
            catch
            {
                return NotFound(notFound.TemplateResponse($"Animal com o id {id} nao encontrado!"));
            }
        }

        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPut("update")]
        [Authorize]
        public async Task<IActionResult> UpdateAssociation(Animal aAnimal)
        {
            var currentUser = HttpContext.User;

            try
            {
                if (currentUser.HasClaim(c => c.Type == "id"))
                {

                    AbstractStatus state = GetState(aAnimal.Id);

                    if (aAnimal.Status == "Adotado")
                    {
                        aAnimal.Status = aAnimal.StartAdopted(state);
                    }
                    else if (aAnimal.Status == "Perdido")
                    {
                        aAnimal.Status = aAnimal.StartLosted(state);
                    }
                    else if (aAnimal.Status == "Adoção")
                    {
                        aAnimal.Status = aAnimal.StartToAdoption(state);
                    }
                    else
                    {
                        return BadRequest(notFound.TemplateResponse("Valor para o Status inválido"));
                    }
                    _context.Entry(await _context.Animals.FirstOrDefaultAsync(x => x.Id == aAnimal.Id)).CurrentValues.SetValues(aAnimal);
                    await _context.SaveChangesAsync();
                    object response = new { success = true, data = aAnimal };
                    return Ok(response);
                }
                return NotFound(notFound.TemplateResponse("Utilizador não se encontra autenticado"));
            }
            catch
            {
                return NotFound(notFound.TemplateResponse($"Nao foi possivel atualizar o animal com o id {aAnimal.Id}"));
            }
        }

        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpGet("state/{state}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetStateAnimals(string state)
        {

            try
            {
                var animalList = await _context.Animals.Where(animal => animal.Status == state).ToListAsync();
                object response = new { success = true, data = animalList };
                return Ok(response);
            }
            catch
            {
                return NotFound(notFound.TemplateResponse($"Nao tem animais com o estado {state}!"));
            }
        }

        //IGUAL MAS APENAS PARA UMA ASSOCIAÇAo
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpGet("state/{state}/association/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetStateAnimalsByAssoc(string state, int id)
        {
            try
            {
                var animalList = await _context.Animals.Where(animal => animal.Status == state).Where(animals => animals.Association_id == id).ToListAsync();
                object response = new { success = true, data = animalList };
                return Ok(response);
            }
            catch
            {
                return NotFound(notFound.TemplateResponse($"Nao tem animais com o estado {state}!"));
            }
        }

        [Produces("application/json")]
        [HttpGet("babies")]
        [AllowAnonymous]
        public async Task<IActionResult> GetBabies()
        {
            var endDate = DateTime.Now.AddYears(-2);

            try
            {
                var animals = await _context.Animals.Where(obj => obj.Age >= endDate).OrderByDescending(b => b.Age).ToListAsync();

                if (!animals.Any())
                {
                    return NotFound(notFound.TemplateResponse("Não tem animais registados"));
                }
                object response = new { success = true, data = animals };
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
            var previus_animal = _context.Animals.Find(aAnimalId);
            AbstractStatus status;
            if (previus_animal.Status == "Perdido")
            {
                status = new Lost();
            }
            else if (previus_animal.Status == "Adotado")
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
