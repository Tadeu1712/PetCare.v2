using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetCareFinalVersion.Models;
using Microsoft.AspNetCore.Authorization;
using PetCareFinalVersion.Patterns.FactoryPost;
using PetCareFinalVersion.Data;
using System.IO;
using System;


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
                var lostAnimalsList = await _context.LostAnimalPosts.OrderByDescending(b => b.Id).ToListAsync();
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
        [HttpGet("{id}")]
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
        public async Task<IActionResult> Create(LostAnimalPost aLostAnimalPost)
        {
            object response;
            try
            {
                var postLostAnimal = (LostAnimalPost)lost_factory.CreatePostFromPostFactory(aLostAnimalPost);
                postLostAnimal.Contact = aLostAnimalPost.Contact;
                postLostAnimal.Location = aLostAnimalPost.Location;
                postLostAnimal.Date = aLostAnimalPost.Date;
                postLostAnimal.Image = aLostAnimalPost.Image;
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

    }
}