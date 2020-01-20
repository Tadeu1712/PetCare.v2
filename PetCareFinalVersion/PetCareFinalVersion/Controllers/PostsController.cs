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
            object response;
            try
            {
                var postsList = await _context.Posts.ToListAsync();
                if (!postsList.Any())
                {
                    response = new { success = false, message = "Não existem posts registados" };
                    return NotFound(response);
                }

                response = new {success = true, postList = postsList };
                return Ok(response);
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
            object response;
            try
            {
                Post post = await _context.Posts.FindAsync(id);
                post.Association = await _context.Associations.FindAsync(post.Association_id);

                response = new { success = true, post = post };
                return Ok(response);
            }
            catch
            {
                response = new { success = false, message = $"Nao existe o post com o id {id}" };
                return NotFound(response);
            }
        }

        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost("create/post")]
        public async Task<IActionResult> CreatePost([FromBody]Post aPost)
        {

            object response;

            var post = (Post)post_factory.CreatePostFromPostFactory(aPost);
            try
            {
                post.Association_id = aPost.Association_id;
                await _context.Posts.AddAsync(post);
                await _context.SaveChangesAsync();

                response = new { sucess = true, post = post };
                return Ok(response);
            }

            catch
            {
                response = new { sucess = false , message = "Não foi possivel realizar a sua ação"};
                return BadRequest(response);
            }
        }

        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost("create/event")]
        public async Task<IActionResult> CreateEvent([FromBody]Event aPost)
        {
            object response;

            var post = (Event)event_factory.CreatePostFromPostFactory(aPost);
            try
            {
                post.Location = aPost.Location;
                post.Type = aPost.Type;
                post.Price = aPost.Price;
                post.DateEnd = aPost.DateEnd;
                post.DateInit = aPost.DateInit;
                post.Association_id = aPost.Association_id;
                await _context.Events.AddAsync(post);
                await _context.SaveChangesAsync();

                response = new { success = true, post = post };
                return Ok(response);
            }
            catch
            {
                response = new { success = false, message = "Não foi possivel realizar o seu pedido" };
                return BadRequest(response);
            }
        }

        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpDelete("delete/post/{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            object response;
            try
            {
                var post = await _context.Posts.FindAsync(id);
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();

                response = new { success = true, message= $"O post com o id:{id} foi apagado" };
                return Ok(response);
            }
            catch
            {
                var rs = new { success = false, message = $"Nao existe o post com o id {id}" };
                return NotFound(rs);
               
            }
        }

        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpDelete("delete/event/{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            object response;
            try
            {
                var post = await _context.Events.FindAsync(id);
                _context.Events.Remove(post);
                await _context.SaveChangesAsync();

                response = new { success = true, message = $"O post com o id:{id} foi apagado" };
                return Ok(response);
            }
            catch
            {
                var rs = new { success = false, message = $"Nao existe o post com o id {id}" };
                return NotFound(rs);

            }
        }

        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateAssociation([FromBody]Post aPost)
        {
            object response;
            try
            {
                _context.Posts.Update(aPost);
                await _context.SaveChangesAsync();

                response = new { success = true, };
                return Ok(aPost);
            }
            catch
            {
                var rs = new { success = false, message = $"Nao foi possivel atualizar o post com o id {aPost.Id}" };
                return NotFound(rs);
            }
        }
    }
}