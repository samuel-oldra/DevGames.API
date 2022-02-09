using DevGames.API.Entities;
using DevGames.API.Models;
using DevGames.API.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DevGames.API.Controllers
{
    [Route("api/boards/{id}/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository repository;

        public PostsController(IPostRepository repository)
        {
            this.repository = repository;
        }

        public IPostRepository Repository { get; }

        // GET: api/boards/{id}/posts
        [HttpGet]
        public IActionResult GetAll(int id)
        {
            Log.Information("Endpoint - GET: api/boards/{id}/posts");

            var posts = repository.GetAllByBoard(id);

            return Ok(posts);
        }

        // GET: api/boards/{id}/posts/{postId}
        [HttpGet("{postId}")]
        public IActionResult GetById(int id, int postId)
        {
            Log.Information("Endpoint - GET: api/boards/{id}/posts/{postId}");

            var post = repository.GetById(postId);

            if (post == null) return NotFound();

            return Ok(post);
        }

        // POST: api/boards/{id}/posts
        [HttpPost]
        public IActionResult Post(int id, AddPostInputModel model)
        {
            Log.Information("Endpoint - POST: api/boards/{id}/posts");

            var post = new Post(model.Title, model.Description, id);

            repository.Add(post);

            return CreatedAtAction(nameof(GetById), new { id = id, postId = post.Id }, model);
        }

        // POST: api/boards/{id}/posts/{postId}/comments
        [HttpPost("{postId}/comments")]
        public async Task<IActionResult> PostComment(int id, int postId, AddCommentInputModel model)
        {
            Log.Information("Endpoint - POST: api/boards/{id}/posts/{postId}/comments");

            var postExists = repository.PostExists(postId);

            if (!postExists) return NotFound();

            var comment = new Comment(model.Title, model.Description, model.User, postId);

            repository.AddComment(comment);

            return NoContent();
        }
    }
}