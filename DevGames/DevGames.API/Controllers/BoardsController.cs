using AutoMapper;
using DevGames.API.Entities;
using DevGames.API.Models;
using DevGames.API.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DevGames.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardsController : ControllerBase
    {
        private readonly IMapper mapper;

        private readonly IBoardRepository repository;

        public BoardsController(IMapper mapper, IBoardRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        // GET: api/boards
        [HttpGet]
        public IActionResult GetAll()
        {
            Log.Information("Endpoint - GET: api/boards");

            var boards = repository.GetAll();

            Log.Information($"{boards.Count()} boards retrieved");

            return Ok(boards);
        }

        // GET: api/boards/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Log.Information("Endpoint - GET: api/boards/{id}");

            var board = repository.GetById(id);

            if (board == null) return NotFound();

            return Ok(board);
        }

        /// <summary>
        /// Post Board
        /// </summary>
        /// <remarks>
        /// Request Body Example:
        /// {
        /// "gameTitle": "Mario Kart",
        /// "description": "Jogo de corrida divertido",
        /// "rules": "Corra, ultrapasse e use os itens especiais"
        /// }
        /// </remarks>
        /// <param name="model">Dados do Board</param>
        /// <returns>Objeto criado</returns>
        /// <response code="201">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post(AddBoardInputModel model)
        {
            Log.Information("Endpoint - POST: api/boards");

            var board = mapper.Map<Board>(model);

            repository.Add(board);

            return CreatedAtAction(nameof(GetById), new { id = board.Id }, model);
        }

        // PUT: api/boards/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateBoardInputModel model)
        {
            Log.Information("Endpoint - PUT: api/boards/{id}");

            var board = repository.GetById(id);

            if (board == null) return NotFound();

            board.Update(model.Description, model.Rules);

            repository.Update(board);

            return NoContent();
        }
    }
}