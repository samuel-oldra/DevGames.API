using AutoMapper;
using DevGames.API.Entities;
using DevGames.API.Models;
using DevGames.API.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace DevGames.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardsController : ControllerBase
    {
        private readonly DevGamesContext context;
        private readonly IMapper mapper;

        public BoardsController(DevGamesContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/boards
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(context.Boards);
        }

        // GET: api/boards/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var board = context.Boards.SingleOrDefault(b => b.Id == id);

            if (board == null) return NotFound();

            return Ok(board);
        }

        // POST: api/boards
        [HttpPost]
        public IActionResult Post(AddBoardInputModel model)
        {
            var board = mapper.Map<Board>(model);

            context.Boards.Add(board);

            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        }

        // PUT: api/boards/1
        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateBoardInputModel model)
        {
            var board = context.Boards.SingleOrDefault(b => b.Id == id);

            if (board == null) return NotFound();

            board.Update(model.Description, model.Rules);

            return NoContent();
        }
    }
}