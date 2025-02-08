using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RebelParkrunCup.Server.Data;
using RebelParkrunCup.Shared;

namespace RebelParkrunCup.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RunnersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RunnersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRunner(int id)
        {
            var runner = await _context.Runners.FindAsync(id);
            if (runner == null)
            {
                return NotFound();
            }

            _context.Runners.Remove(runner);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/runners
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Runner>>> GetRunners()
        {
            var runners = await _context.Runners.ToListAsync();
            return Ok(runners);
        }

        // POST: api/runners
        [HttpPost]
        public async Task<ActionResult<Runner>> PostRunner(Runner runner)
        {
            if (runner == null)
            {
                return BadRequest("Runner data is required");
            }

            // Optionally: Add validation for data here (like checking if the name is valid)

            _context.Runners.Add(runner);
            await _context.SaveChangesAsync();

            // Return a 201 status code with the created runner (you could return just the ID too if desired)
            return CreatedAtAction(nameof(GetRunners), new { id = runner.Id }, runner);
        }
    }
}
