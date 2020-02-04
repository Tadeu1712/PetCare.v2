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
    [Route("api/lost")]
    [ApiController]
    public class LostAnimalsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly AbstractPostsFactory lost_factory = LostAnimalFactory.Instance;

        public LostAnimalsController(AppDbContext aContext)
        {
            _context = aContext;
        }

        [Produces("application/json")]
        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<IActionResult> getAllLostAnimals()
        {
            object response;
            try
            {
                var lostAnimalsList = await _context.LostAnimalPosts.ToListAsync();
                if (!lostAnimalsList.Any())
                {
                    response = new { success = false, message = "Nao existe animais perdidos" };
                    return NotFound(response);
                }

                response = new {success= true, data = lostAnimalsList };

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
        public async Task<IActionResult> getLostAnimal(int id)
        {
            object response;
            try
            {
                LostAnimalPost lostAnimal = await _context.LostAnimalPosts.FindAsync(id);

                response = new { success = true, data = lostAnimal };
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
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromForm] string title, [FromForm] string description, [FromForm] string contact, [FromForm] string location, [FromForm] string date)
        {
            object response;
            var files = Request.Form.Files;
            try
            {
                var postLostAnimal = (LostAnimalPost)lost_factory.CreatePostFromPostFactory(title, description);
                postLostAnimal.Image = ImageSave.SaveImage(files, "lost_animal");
                postLostAnimal.Contact = contact;
                postLostAnimal.Location = location;
                postLostAnimal.Date = date;
                await _context.LostAnimalPosts.AddAsync(postLostAnimal);
                await _context.SaveChangesAsync();

                response = new { sucess = true, data = postLostAnimal };
                return Ok(response);
            }
            catch
            {
                response = new { sucess = false, message = "Não foi possivel realizar a sua ação" };
                return BadRequest(response);
            }
        }

        // ROUTE GET POST IMAGES 
        [HttpGet("img/{imgName}")]
        [AllowAnonymous]

        public async Task<IActionResult> GetImgAsync(string imgName)
        {
            var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Resources/images/lost_animal", imgName);
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