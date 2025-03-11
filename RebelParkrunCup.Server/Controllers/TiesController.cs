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

        [HttpGet]
        public async Task<ActionResult<List<TieDto>>> GetTies(int tournamentId)
        {
            var ties = await _context.Ties
                .Include(t => t.Competitor1)
                    .ThenInclude(c => c.Runner)
                .Include(t => t.Competitor2)
                    .ThenInclude(c => c.Runner)
                .Include(t => t.Tournament)
                .Include(t => t.Location)
                .Where(t => t.Tournament.Id == tournamentId)
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