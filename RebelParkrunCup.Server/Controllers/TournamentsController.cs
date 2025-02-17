using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RebelParkrunCup.Server.Data;
using RebelParkrunCup.Shared;

namespace RebelParkrunCup.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TournamentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(int id)
        {
            var tournament = await _context.Tournaments.FindAsync(id);
            if (tournament == null)
            {
                return NotFound();
            }

            _context.Tournaments.Remove(tournament);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/runners
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentDto>>> GetTournaments()
        {
            var tournaments = await _context.Tournaments.ToListAsync();
            
            var tournamentDtos = tournaments.Select(t => new TournamentDto
            {                
                Id = t.Id,
                Name = t.Name,
                StartDate = t.StartDate,
                EndDate = t.EndDate,
                Active = t.Active
            }).ToList();

            return Ok(tournamentDtos);
        }

        // POST: api/runners
        [HttpPost]
        public async Task<ActionResult<Tournament>> PostRunner(Tournament tournament)
        {
            if (tournament == null)
            {
                return BadRequest("Tournament data is required");
            }

            // Optionally: Add validation for data here (like checking if the name is valid)

            _context.Tournaments.Add(tournament);
            await _context.SaveChangesAsync();

            // Return a 201 status code with the created runner (you could return just the ID too if desired)
            return CreatedAtAction(nameof(GetTournaments), new { id = tournament.Id }, tournament);
        }

        // PUT: api/runners/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournament(int id, Tournament tournament)
        {
            if (id != tournament.Id)
            {
                return BadRequest("ID mismatch");
            }

            var existingTournament = await _context.Tournaments.FindAsync(id);
            if (existingTournament == null)
            {
                return NotFound();
            }

            // Update fields
            existingTournament.Name = tournament.Name;
            existingTournament.StartDate = tournament.StartDate;
            existingTournament.EndDate = tournament.EndDate;
            existingTournament.Active = tournament.Active;

            await _context.SaveChangesAsync();

            return NoContent(); // Success, no content returned
        }
    }
}