using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RebelParkrunCup.Server.Data;
using RebelParkrunCup.Shared;

namespace RebelParkrunCup.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LocationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            var location = await _context.Locations.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location>>> GetLocations()
        {
            var locations = await _context.Locations.ToListAsync();
            return Ok(locations);            
        }
        
        [HttpPost]
        public async Task<ActionResult<Location>> PostLocation(Location location)
        {
            if (location == null)
            {
                return BadRequest("Location data is required");
            }
            
            _context.Locations.Add(location);
            await _context.SaveChangesAsync();

            // Return a 201 status code with the created runner.
            return CreatedAtAction(nameof(GetLocations), new { id = location.Id }, location);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocation(int id, Location location)
        {
            if (id != location.Id)
            {
                return BadRequest("ID mismatch");
            }

            var existingLocation = await _context.Locations.FindAsync(id);
            if (existingLocation == null)
            {
                return NotFound();
            }

            // Update fields
            existingLocation.Name = location.Name;
            await _context.SaveChangesAsync();

            return NoContent(); // Success, no content returned
        }
    }
}