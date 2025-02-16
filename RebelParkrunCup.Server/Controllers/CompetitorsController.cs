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
    }
}