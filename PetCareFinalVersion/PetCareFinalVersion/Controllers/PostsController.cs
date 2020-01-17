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

namespace PetCareFinalVersion.Controllers
{
    [Route("api/post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PostController(AppDbContext aContext)
        {
            _context = aContext;
        }

        [Produces("application/json")]
        [HttpGet("all")]
        public async Task<IActionResult> getAllPosts()
        {
            try
            {
                var json = _context.Posts.ToList();
                if (json == null)
                {

                }
                return Ok(json);
            }
            catch
            {
                return BadRequest();
            }

        }

        [Produces("application/json")]
        [HttpGet("find/{id}")]
        public async Task<IActionResult> getPost(int id)
        {
            try
            {
                Post query = _context.Posts.Find(id);
                query.Association = _context.Associations.Find(query.Association_id);
                return Ok(query);
            }
            catch
            {
                return NotFound();
            }
        }

        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]Post aPost)
        {

            AbstractPostFactory factory = PostFactory.Instance;
            var post = (Post)factory.CreatePostFromPostFactory(aPost.Title, aPost.Description, aPost.Association_id);
            try
            {
                _context.Posts.Add(post);
                _context.SaveChanges();

                return Ok(post);
            }

            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            try
            {
                var post = _context.Posts.Find(id);
                _context.Posts.Remove(post);
                _context.SaveChanges();
                return Ok("Deleted " + id);
            }
            catch
            {
                return NotFound("Post with " + id + " not found!");
            }
        }

        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateAssociation([FromBody]Post aPost)
        {
            try
            {
                _context.Posts.Update(aPost);
                _context.SaveChanges();
                return Ok(aPost);
            }
            catch
            {
                return NotFound("Not Found!!!");
            }
        }

    }
}