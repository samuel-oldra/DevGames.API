using DevGames.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevGames.API.Controllers
{
    [Route("api/boards/{id}/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        // GET: api/boards/1/posts
        [HttpGet]
        public IActionResult GetAll(int id)
        {
            return Ok();
        }

        // GET: api/boards/1/posts/2
        [HttpGet("{postId}")]
        public IActionResult GetById(int id, int postId)
        {
            // NotFound

            return Ok();
        }

        // POST: api/boards/1/posts
        [HttpPost]
        public IActionResult Post(int id, AddPostInputModel model)
        {
            return CreatedAtAction(nameof(GetById), new { id = id, postId = model.Id }, model);
        }

        // POST: api/boards/1/posts/2/comments
        [HttpPost("{postId}/comments")]
        public IActionResult PostComment(int id, int postId, AddCommentInputModel model)
        {
            return NoContent();
        }
    }
}