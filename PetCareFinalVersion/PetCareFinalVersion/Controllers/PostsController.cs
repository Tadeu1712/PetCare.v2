using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetCareFinalVersion.Models;
using PetCareFinalVersion.Patterns.FactoryPost;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using PetCareFinalVersion.Data;

namespace PetCareFinalVersion.Controllers
{
    [Route("api/post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        IWebHostEnvironment _environment;
        private readonly AppDbContext _context;
        private readonly AbstractPostsFactory post_factory = PostFactory.Instance;
        

        public PostController(AppDbContext aContext, IWebHostEnvironment aEnvironment)
        {
            _context = aContext;
            _environment = aEnvironment;
        }

        [Produces("application/json")]
        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<IActionResult> getAllPosts()
        {
            object response;
            try
            {
                var postsList = await _context.Posts.ToListAsync();
                if (!postsList.Any())
                {
                    response = new { success = true, message = "Não existem posts registados" };
                    return NotFound(response);
                }

                response = new { success = true, data = postsList };
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

        public async Task<IActionResult> getPost(int id)
        {
            object response;
            try
            {
                Post post = await _context.Posts.FindAsync(id);
                post.Association = await _context.Associations.FindAsync(post.Association_id);

                response = new { success = true, data = post };
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
        [Authorize]
        public async Task<IActionResult> CreatePost([FromForm] int Association_id, [FromForm] string Title, [FromForm] string Description)
        {
            object response;
            var files = Request.Form.Files;
            var currentUser = HttpContext.User;

            var post = (Post)post_factory.CreatePostFromPostFactory(Title, Description);
            try
            {
                if (currentUser.HasClaim(c => c.Type == "id"))
                {
                    post.Association_id = Association_id;
                    post.Image = ImageSave.SaveImage(files, "post");
                    await _context.Posts.AddAsync(post);
                    await _context.SaveChangesAsync();

                    response = new {sucess = true, data = post};
                    return Ok(response);
                }
                response = new { success = false, message = "Utilizador não se encontra autenticado" };
                return NotFound(response);
            }
            catch
            {
                response = new { sucess = false, message = "Não foi possivel realizar a sua ação" };
                return BadRequest(response);
            }
        }


        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpDelete("delete/{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePost(int id)
        {
            object response;
            var currentUser = HttpContext.User;

            try
            {
                if (currentUser.HasClaim(c => c.Type == "id"))
                {
                    var post = await _context.Posts.FindAsync(id);
                    _context.Posts.Remove(post);
                    await _context.SaveChangesAsync();

                    response = new {success = true, message = $"O post com o id:{id} foi apagado com sucesso"};
                    return Ok(response);
                }
            }
            catch
            {
                var rs = new { success = false, message = $"Nao existe o post com o id {id}" };
                return NotFound(rs);
               
            }
            response = new { success = false, message = "Utilizador não se encontra autenticado" };
            return NotFound(response);
        }


        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPut("update")]
        [Authorize]
        public async Task<IActionResult> UpdateAssociation([FromBody]Post aPost)
        {
            object response;
            var currentUser = HttpContext.User;

            try
            {
                if (currentUser.HasClaim(c => c.Type == "id"))
                {
                    _context.Posts.Update(aPost);
                    await _context.SaveChangesAsync();

                    response = new {success = true, data = aPost};
                    return Ok(response);
                }
                response = new { success = false, message = "Utilizador não se encontra autenticado" };
                return NotFound(response);
            }
            catch
            {
                var rs = new { success = false, message = $"Nao foi possivel atualizar o post com o id {aPost.Id}" };
                return NotFound(rs);
            }
        }

        // ROUTE GET POST IMAGES 
        [HttpGet("img/{imgName}")]
        [AllowAnonymous]

        public async Task<IActionResult> GetImgAsync(string imgName)
        {
            var path = Path.Combine(
                     Directory.GetCurrentDirectory(),
                     "Resources/images/post", imgName);
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