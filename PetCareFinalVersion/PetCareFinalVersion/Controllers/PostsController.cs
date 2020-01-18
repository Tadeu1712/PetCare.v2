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
        private readonly AbstractPostsFactory post_factory = PostFactory.Instance;
        private readonly AbstractPostsFactory event_factory = EventFactory.Instance;

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
                if (!json.Any()) return NotFound("Não tem posts registados");
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
        [HttpPost("create/post")]
        public async Task<IActionResult> CreatePost([FromBody]Post aPost)
        {

          
            var post = (Post)post_factory.CreatePostFromPostFactory(aPost);
            try
            {
                post.Association_id = aPost.Association_id;
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
        [HttpPost("create/event")]
        public async Task<IActionResult> CreateEvent([FromBody]Event aPost)
        {
            var post = (Event)event_factory.CreatePostFromPostFactory(aPost);
            try
            {
                post.Location = aPost.Location;
                post.Type = aPost.Type;
                post.Price = aPost.Price;
                post.DateEnd = aPost.DateEnd;
                post.DateInit = aPost.DateInit;
                post.Association_id = aPost.Association_id;
                _context.Events.Add(post);
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