using DevGames.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevGames.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardsController : ControllerBase
    {
        // GET: api/boards
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok();
        }

        // GET: api/boards/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // NotFound()

            return Ok();
        }

        // POST: api/boards
        [HttpPost]
        public IActionResult Post(AddBoardInputModel model)
        {
            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        }

        // PUT: api/boards/1
        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateBoardInputModel model)
        {
            return NoContent();
        }

        // DELETE: api/boards/1
        [HttpDelete("id")]
        public IActionResult Delete(int id)
        {
            return NoContent();
        }
    }
}