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

        [HttpPost("saveall")]
        public async Task<IActionResult> SaveAllCompetitors([FromBody] List<Competitor> competitors)
        {
            if (competitors == null)
            {
                return BadRequest("No competitors provided.");
            }

            try
            {
                if (competitors.Count == 0)
                {
                    // If no competitors are provided, delete all existing competitors
                    var allCompetitors = await _context.Competitors.ToListAsync();
                    _context.Competitors.RemoveRange(allCompetitors);
                    await _context.SaveChangesAsync();
                    return Ok("All competitors deleted.");
                }

                // Get existing competitors for the same tournament
                if (competitors.Count > 0)
                {
                    int tournamentId = competitors.First().TournamentId;
                    var existingCompetitors = await _context.Competitors
                        .Where(c => c.TournamentId == tournamentId)
                        .ToListAsync();

                    // Find competitors to delete (exist in DB but not in new list)
                    var competitorsToDelete = existingCompetitors
                        .Where(ec => !competitors.Any(nc => nc.Id == ec.Id))
                        .ToList();

                    _context.Competitors.RemoveRange(competitorsToDelete);

                    foreach (var competitor in competitors)
                    {
                        var existingCompetitor = existingCompetitors.FirstOrDefault(c => c.Id == competitor.Id);

                        if (existingCompetitor != null)
                        {
                            // Update existing competitor
                            existingCompetitor.BaselineTimeMins = competitor.BaselineTimeMins;
                            existingCompetitor.BaselineTimeSecs = competitor.BaselineTimeSecs;
                        }
                        else
                        {
                            // Ensure valid Runner & Tournament references before adding
                            var runner = await _context.Runners.FindAsync(competitor.RunnerId);
                            var tournament = await _context.Tournaments.FindAsync(competitor.TournamentId);

                            if (runner == null || tournament == null)
                            {
                                return BadRequest("Invalid RunnerId or TournamentId.");
                            }

                            competitor.Runner = runner;
                            competitor.Tournament = tournament;

                            _context.Competitors.Add(competitor);
                        }
                    }

                    await _context.SaveChangesAsync();
                }

                return Ok("Competitors saved successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("getall")]
        public async Task<ActionResult<List<CompetitorDto>>> GetCompetitors()
        {
            var competitors = await _context.Competitors
                .Include(c => c.Runner)
                .Include(c => c.Tournament)
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
    }
}