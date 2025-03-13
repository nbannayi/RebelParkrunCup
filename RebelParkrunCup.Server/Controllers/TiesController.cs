using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RebelParkrunCup.Server.Data;
using RebelParkrunCup.Shared;

namespace RebelParkrunCup.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TiesController(ApplicationDbContext context)
        {
            _context = context;
        }

         // POST: api/runners
        [HttpPost]
        public async Task<IActionResult> CreateTie([FromBody] Tie tie)
        {
            if (tie == null)
            {
                return BadRequest("Tie data is required");
            }

            // Optionally: Add validation for data here (like checking if the name is valid)

            _context.Ties.Add(tie);
            await _context.SaveChangesAsync();

            // Return a 201 status code with the created runner (you could return just the ID too if desired)
            return Ok(tie.Id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTie(int id)
        {
            var tie = await _context.Ties.FindAsync(id);
            if (tie == null)
            {
                return NotFound();
            }

            _context.Ties.Remove(tie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<List<TieDto>>> GetTies(int tournamentId)
        {
            var ties = await _context.Ties
                .Include(t => t.Competitor1)
                .Include(t => t.Competitor1.Runner)
                .Include(t => t.Competitor2)
                .Include(t => t.Competitor2.Runner)
                .Include(t => t.Location)
                .Where(t => t.Competitor1.TournamentId == tournamentId)
                .ToListAsync();

            var tieDtos = ties.Select(t => new TieDto
            {                
                Id = t.Id,
                Competitor1FirstName = t.Competitor1.Runner.FirstName,
                Competitor1LastName = t.Competitor1.Runner.LastName, 
                Competitor2FirstName = t.Competitor2.Runner.FirstName,
                Competitor2LastName = t.Competitor2.Runner.LastName,
                Round = t.Round,
                Location = t.Location.Name,
                Competitor1ResultMins = t.Competitor1ResultMins,
                Competitor1ResultSecs = t.Competitor1ResultSecs,
                Competitor1Delta = t.Competitor1Delta,                
                Competitor2ResultMins = t.Competitor2ResultMins,
                Competitor2ResultSecs = t.Competitor2ResultSecs,
                Competitor2Delta = t.Competitor2Delta,
                Date = t.Date
            }).ToList();

            return Ok(tieDtos);
        }
    }
}