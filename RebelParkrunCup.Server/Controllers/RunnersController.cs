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

        // GET: api/runners
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Runner>>> GetRunners()
        {
            var runners = await _context.Runners.ToListAsync();
            return Ok(runners);
        }
    }
}
