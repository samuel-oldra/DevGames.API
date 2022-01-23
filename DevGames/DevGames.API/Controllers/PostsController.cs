using DevGames.API.Entities;
using DevGames.API.Models;
using DevGames.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevGames.API.Controllers
{
    [Route("api/boards/{id}/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly DevGamesContext context;

        public PostsController(DevGamesContext context)
        {
            this.context = context;
        }

        // GET: api/boards/1/posts
        [HttpGet]
        public async Task<IActionResult> GetAll(int id)
        {
            var posts = await context.Posts.Where(p => p.BoardId == id).ToListAsync();

            return Ok(posts);
        }

        // GET: api/boards/1/posts/2
        [HttpGet("{postId}")]
        public async Task<IActionResult> GetById(int id, int postId)
        {
            var post = await context.Posts
                .Include(p => p.Comments)
                .SingleOrDefaultAsync(p => p.Id == postId);

            if (post == null) return NotFound();

            return Ok(post);
        }

        // POST: api/boards/1/posts
        [HttpPost]
        public async Task<IActionResult> Post(int id, AddPostInputModel model)
        {
            var post = new Post(model.Title, model.Description, id);

            await context.Posts.AddAsync(post);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = id, postId = post.Id }, model);
        }

        // POST: api/boards/1/posts/2/comments
        [HttpPost("{postId}/comments")]
        public async Task<IActionResult> PostComment(int id, int postId, AddCommentInputModel model)
        {
            var postExists = await context.Posts.AnyAsync(p => p.Id == postId);

            if (!postExists) return NotFound();

            var comment = new Comment(model.Title, model.Description, model.User, postId);

            context.Comments.Add(comment);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}