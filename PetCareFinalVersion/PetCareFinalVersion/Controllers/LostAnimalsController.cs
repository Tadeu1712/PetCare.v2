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
        [HttpPost("create")]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromForm] string aTitle, [FromForm] string aDescription, [FromForm] string aContact)
        {
            object response;
            var files = Request.Form.Files;
            var postLostAnimal = (LostAnimalPost)lost_factory.CreatePostFromPostFactory(aTitle, aDescription);
            try
            {
                postLostAnimal.Image = ImageSave.SaveImage(files, "lost_animal");
                postLostAnimal.Contact = aContact;
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