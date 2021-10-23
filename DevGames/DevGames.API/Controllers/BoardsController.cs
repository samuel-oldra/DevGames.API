using AutoMapper;
using DevGames.API.Entities;
using DevGames.API.Models;
using DevGames.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> GetAll()
        {
            var boards = await context.Boards.ToListAsync();

            return Ok(boards);
        }

        // GET: api/boards/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var board = await context.Boards.SingleOrDefaultAsync(b => b.Id == id);

            if (board == null) return NotFound();

            return Ok(board);
        }

        // POST: api/boards
        [HttpPost]
        public async Task<IActionResult> Post(AddBoardInputModel model)
        {
            var board = mapper.Map<Board>(model);

            await context.Boards.AddAsync(board);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = board.Id }, model);
        }

        // PUT: api/boards/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateBoardInputModel model)
        {
            var board = await context.Boards.SingleOrDefaultAsync(b => b.Id == id);

            if (board == null) return NotFound();

            board.Update(model.Description, model.Rules);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}