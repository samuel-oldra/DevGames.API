using AutoMapper;
using DevGames.API.Entities;
using DevGames.API.Models;
using DevGames.API.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DevGames.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoardsController : ControllerBase
    {
        private readonly IMapper mapper;

        private readonly IBoardRepository boardRepository;

        public BoardsController(
            IMapper mapper,
            IBoardRepository boardRepository)
        {
            this.mapper = mapper;
            this.boardRepository = boardRepository;
        }

        // GET: api/boards
        /// <summary>
        /// Listagem de Boards
        /// </summary>
        /// <returns>Lista de Boards</returns>
        /// <response code="200">Sucesso</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            Log.Information("Endpoint - GET: api/boards");

            var boards = boardRepository.GetAll();

            Log.Information($"{boards.Count()} boards retrieved");

            return Ok(boards);
        }

        // GET: api/boards/{id}
        /// <summary>
        /// Detalhes do Board
        /// </summary>
        /// <param name="id">ID do Board</param>
        /// <returns>Mostra um Board</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            Log.Information("Endpoint - GET: api/boards/{id}");

            var board = boardRepository.GetById(id);

            if (board == null)
                return NotFound("Board não encontrado.");

            return Ok(board);
        }

        // POST: api/boards
        /// <summary>
        /// Cadastro de Board
        /// </summary>
        /// <remarks>
        /// Requisição:
        /// {
        ///     "gameTitle": "Mario Kart",
        ///     "description": "Jogo de corrida divertido",
        ///     "rules": "Corra, ultrapasse e use os itens especiais"
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

            boardRepository.Add(board);

            return CreatedAtAction(nameof(GetById), new { id = board.Id }, model);
        }

        // PUT: api/boards/{id}
        /// <summary>
        /// Atualiza um Board
        /// </summary>
        /// <remarks>
        /// Requisição:
        /// {
        ///     "description": "Jogo de corrida frenético",
        ///     "rules": "Corra, ultrapasse, use os itens especiais e descubra atalhos"
        /// }
        /// </remarks>
        /// <param name="id">ID do Board</param>
        /// <param name="model">Dados do Board</param>
        /// <response code="204">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="404">Não encontrado</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(int id, UpdateBoardInputModel model)
        {
            Log.Information("Endpoint - PUT: api/boards/{id}");

            var board = boardRepository.GetById(id);

            if (board == null)
                return NotFound("Board não encontrado.");

            board.Update(model.Description, model.Rules);

            boardRepository.Update(board);

            return NoContent();
        }
    }
}