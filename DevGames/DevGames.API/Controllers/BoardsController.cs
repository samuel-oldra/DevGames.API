using AutoMapper;
using DevGames.API.Entities;
using DevGames.API.Models;
using DevGames.API.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

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
            var boards = repository.GetAll();

            return Ok(boards);
        }

        // GET: api/boards/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var board = repository.GetById(id);

            if (board == null) return NotFound();

            return Ok(board);
        }

        // POST: api/boards
        [HttpPost]
        public IActionResult Post(AddBoardInputModel model)
        {
            var board = mapper.Map<Board>(model);

            repository.Add(board);

            return CreatedAtAction(nameof(GetById), new { id = board.Id }, model);
        }

        // PUT: api/boards/1
        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateBoardInputModel model)
        {
            var board = repository.GetById(id);

            if (board == null) return NotFound();

            board.Update(model.Description, model.Rules);

            repository.Update(board);

            return NoContent();
        }
    }
}