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
using PetCareFinalVersion.Patterns.TemplateMethod;

namespace PetCareFinalVersion.Controllers
{
    [Route("api/post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        IWebHostEnvironment _environment;
        private readonly AppDbContext _context;
        private readonly AbstractPostsFactory post_factory = PostFactory.Instance;
        protected AbstractTemplate ok = new ConcreteOk();
        protected AbstractTemplate notFound = new ConcreteNotFound();

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
            try
            {
                var postsList = await _context.Posts.ToListAsync();
                if (!postsList.Any())
                {
                    return NotFound(notFound.TemplateResponse("Não existem posts registados"));
                }

                object response = new { success = true, data = postsList };
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
            try
            {
                Post post = await _context.Posts.FindAsync(id);
                post.Association = await _context.Associations.FindAsync(post.Association_id);
                object response = new { success = true, data = post };
                return Ok(response);
            }
            catch
            {
                return NotFound(notFound.TemplateResponse($"Nao existe o post com o id {id}"));
            }
        }

        [Produces("application/json")]
        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> CreatePost(Post aPost)
        {
            var currentUser = HttpContext.User;
            int id;
            try
            {
                if (currentUser.HasClaim(c => c.Type == "id"))
                {
                    id = int.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "id").Value);
                    Association association = _context.Associations.Single(assoc => assoc.User_id == id);
                    var post = (Post)post_factory.CreatePostFromPostFactory(aPost);
                    post.Association_id = association.Id;
                    await _context.Posts.AddAsync(post);
                    await _context.SaveChangesAsync();

                    object response = new {sucess = true, data = post};
                    return Ok(response);
                }
                return NotFound(notFound.TemplateResponse("Utilizador não se encontra autenticado"));
            }
            catch
            {
                return BadRequest(notFound.TemplateResponse("Não foi possivel realizar a sua ação"));
            }
        }


        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePost(int id)
        {
            var currentUser = HttpContext.User;

            try
            {
                if (currentUser.HasClaim(c => c.Type == "id"))
                {
                    var post = await _context.Posts.FindAsync(id);
                    var id_log = int.Parse(currentUser.Claims.First(c => c.Type == "id").Value);
                    var association = _context.Associations.Where(assoc => assoc.User_id == id_log).Single();
                    if (post.Association_id == association.Id)
                    {
                        _context.Posts.Remove(post);
                        await _context.SaveChangesAsync();
                        return Ok(ok.TemplateResponse($"O post com o id:{id} foi apagado com sucesso"));
                    }
                    else
                    {
                        return Ok(notFound.TemplateResponse($"O Post que tentou apagar não pertence à sua associação"));
                    }
                }
                return NotFound(notFound.TemplateResponse("Utilizador não se encontra autenticado"));
            }
            catch
            {
                return NotFound(notFound.TemplateResponse($"Nao existe o post com o id {id}"));
            }
            
        }


        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPut("update")]
        [Authorize]
        public async Task<IActionResult> UpdateAssociation(Post aPost)
        {
            var currentUser = HttpContext.User;
            try
            {
                if (currentUser.HasClaim(c => c.Type == "id"))
                {
                    _context.Posts.Update(aPost);
                    await _context.SaveChangesAsync();

                    object response = new {success = true, data = aPost};
                    return Ok(response);
                }
                return NotFound(notFound.TemplateResponse("Utilizador não se encontra autenticado"));
            }
            catch
            {
                return NotFound(notFound.TemplateResponse($"Nao foi possivel atualizar o post com o id {aPost.Id}"));
            }
        }

        

    }

}