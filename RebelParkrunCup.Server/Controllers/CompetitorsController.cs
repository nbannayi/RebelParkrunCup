using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RebelParkrunCup.Server.Data;
using RebelParkrunCup.Shared;

namespace RebelParkrunCup.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompetitorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CompetitorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<CompetitorDto>>> GetCompetitors(int tournamentId)
        {
            var competitors = await _context.Competitors
                .Include(c => c.Runner)
                .Include(c => c.Tournament)
                .Where(c => c.TournamentId == tournamentId)
                .ToListAsync();

            var competitorDtos = competitors.Select(c => new CompetitorDto
            {                
                Id = c.Id,
                RunnerId = c.Runner.Id,
                RunnerFirstName = c.Runner.FirstName,
                RunnerLastName = c.Runner.LastName,    
                RunnerParkrunId = c.Runner.ParkrunID,   
                BaselineTimeMins = c.BaselineTimeMins,  
                BaselineTimeSecs = c.BaselineTimeSecs   
            }).ToList();

            return Ok(competitorDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Competitor>> GetCompetitor(int id)
        {
            var competitor = await _context.Competitors.FindAsync(id);
            if (competitor == null)
            {
                return NotFound();
            }
            return competitor;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompetitor([FromBody] Competitor competitor)
        {
            if (competitor == null)
            {
                return BadRequest("Invalid competitor data.");
            }

            // Manually attach Runner and Tournament to avoid JSON deserialization issues
            competitor.Runner = await _context.Runners.FindAsync(competitor.RunnerId);
            competitor.Tournament = await _context.Tournaments.FindAsync(competitor.TournamentId);

            if (competitor.Runner == null || competitor.Tournament == null)
            {
                return BadRequest("Invalid RunnerId or TournamentId.");
            }

            _context.Competitors.Add(competitor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCompetitor), new { id = competitor.Id }, competitor);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCompetitors(List<Competitor> competitors)
        {
            foreach (var competitor in competitors)
            {
                var existing = await _context.Competitors.FindAsync(competitor.Id);
                if (existing != null)
                {
                    existing.BaselineTimeMins = competitor.BaselineTimeMins;
                    existing.BaselineTimeSecs = competitor.BaselineTimeSecs;
                }
                else
                {
                    _context.Competitors.Add(competitor);
                }
            }

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}