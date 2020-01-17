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
                var lostAnimalsList = _context.LostAnimalPosts.ToList();
                if (!lostAnimalsList.Any())return NotFound("Não tem animais perdidos");
                else return Ok(lostAnimalsList);
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

            IPost factory = PostFactory.Instance;
            var lostAnimalPost = (LostAnimalPost)factory.CreatePostFromPostFactory(aLostAnimalPost);
            try
            {
                _context.Posts.Add(lostAnimalPost);
                _context.SaveChanges();

                return Ok(lostAnimalPost);
            }

            catch
            {
                return BadRequest();
            }
        }


    }
}