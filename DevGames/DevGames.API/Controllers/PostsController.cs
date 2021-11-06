using DevGames.API.Entities;
using DevGames.API.Models;
using DevGames.API.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DevGames.API.Controllers
{
    [ApiController]
    [Route("api/boards/{id}/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository repository;

        public PostsController(IPostRepository repository)
        {
            this.repository = repository;
        }

        public IPostRepository Repository { get; }

        // GET: api/boards/{id}/posts
        /// <summary>
        /// Listagem de Posts
        /// </summary>
        /// <param name="id">ID do Board</param>
        /// <returns>Lista de Posts</returns>
        /// <response code="200">Sucesso</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll(int id)
        {
            Log.Information("Endpoint - GET: api/boards/{id}/posts");

            var posts = repository.GetAllByBoard(id);

            return Ok(posts);
        }

        // GET: api/boards/{id}/posts/{postId}
        /// <summary>
        /// Detalhes do Post
        /// </summary>
        /// <param name="id">ID do Board</param>
        /// <param name="postId">ID do Post</param>
        /// <returns>Mostra um Post</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("{postId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id, int postId)
        {
            Log.Information("Endpoint - GET: api/boards/{id}/posts/{postId}");

            var post = repository.GetById(postId);

            if (post == null) return NotFound();

            return Ok(post);
        }

        // POST: api/boards/{id}/posts
        /// <summary>
        /// Cadastro de Post
        /// </summary>
        /// <remarks>
        /// Requisição:
        /// {
        ///     "title": "Nova fase",
        ///     "description": "Desenvolver novas fases",
        ///     "user": "Carlos"
        /// }
        /// </remarks>
        /// <param name="id">ID do Board</param>
        /// <param name="model">Dados do Post</param>
        /// <returns>Objeto criado</returns>
        /// <response code="201">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post(int id, AddPostInputModel model)
        {
            Log.Information("Endpoint - POST: api/boards/{id}/posts");

            var post = new Post(model.Title, model.Description, id);

            repository.Add(post);

            return CreatedAtAction(nameof(GetById), new { id = id, postId = post.Id }, model);
        }

        // POST: api/boards/{id}/posts/{postId}/comments
        /// <summary>
        /// Cadastro de Comentário
        /// </summary>
        /// <remarks>
        /// Requisição:
        /// {
        ///     "title": "Nova fase",
        ///     "description": "Poderia ter uma fase na neve",
        ///     "user": "Jorge"
        /// }
        /// </remarks>
        /// <param name="id">ID do Board</param>
        /// <param name="postId">ID do Post</param>
        /// <param name="model">Dados do Comentário</param>
        /// <response code="204">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="404">Não encontrado</response>
        [HttpPost("{postId}/comments")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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