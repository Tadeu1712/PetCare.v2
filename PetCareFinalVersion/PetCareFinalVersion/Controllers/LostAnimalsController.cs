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
using PetCareFinalVersion.Patterns.FactoryPost;
using PetCareFinalVersion.Data;

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
        public async Task<IActionResult> getAllLostAnimals()
        {
            try
            {
                var lostAnimalsList = await _context.LostAnimalPosts.ToListAsync();
                if (!lostAnimalsList.Any())
                {
                    var rs = new { success = false, message = "Nao existe animais perdidos" };
                    return NotFound(rs);
                }

                var response = new {success= true, lostAnimals = lostAnimalsList };

                return Ok(response);
            }
            catch
            {
                return BadRequest();
            }

        }

        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]LostAnimalPost aLostAnimalPost)
        {
            object response;
            var lostAnimalPost = (LostAnimalPost)lost_factory.CreatePostFromPostFactory(aLostAnimalPost);
            try
            {
                lostAnimalPost.Contact = aLostAnimalPost.Contact;
                await _context.LostAnimalPosts.AddAsync(lostAnimalPost);
                await _context.SaveChangesAsync();

                 response = new { success = true, lostAnimalPost = lostAnimalPost };
                return Ok(response);
            }

            catch
            {
                response = new { success = false };
                return BadRequest(response);
            }
        }


    }
}