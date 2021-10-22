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
        public IActionResult GetAll(int id)
        {
            var posts = context.Posts.Where(p => p.BoardId == id);

            return Ok(posts);
        }

        // GET: api/boards/1/posts/2
        [HttpGet("{postId}")]
        public IActionResult GetById(int id, int postId)
        {
            var post = context.Posts
                .Include(p => p.Comments)
                .SingleOrDefault(p => p.Id == postId);

            if (post == null) return NotFound();

            return Ok(post);
        }

        // POST: api/boards/1/posts
        [HttpPost]
        public IActionResult Post(int id, AddPostInputModel model)
        {
            var post = new Post(model.Title, model.Description, id);

            context.Posts.Add(post);
            context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = id, postId = post.Id }, model);
        }

        // POST: api/boards/1/posts/2/comments
        [HttpPost("{postId}/comments")]
        public IActionResult PostComment(int id, int postId, AddCommentInputModel model)
        {
            var postExists = context.Posts.Any(p => p.Id == postId);

            if (!postExists) return NotFound();

            var comment = new Comment(model.Title, model.Description, model.User, postId);

            context.Comments.Add(comment);
            context.SaveChanges();

            return NoContent();
        }
    }
}