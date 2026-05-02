using Afforestation.API.Data;
using Afforestation.Core.DTO;
using Afforestation.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Afforestation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObservationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ObservationsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var observations = await _context.Observations
                .Select(o => new ObservationDto
                {
                    Id = o.Id,
                    SiteId = o.SiteId,
                    Date = o.Date,
                    ProductivityScore = o.ProductivityScore,
                    Note = o.Note
                })
                .ToListAsync();

            return Ok(observations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var observation = await _context.Observations
                .Where(o => o.Id == id)
                .Select(o => new ObservationDto
                {
                    Id = o.Id,
                    SiteId = o.SiteId,
                    Date = o.Date,
                    ProductivityScore = o.ProductivityScore,
                    Note = o.Note
                })
                .FirstOrDefaultAsync();

            if (observation == null)
                return NotFound();

            return Ok(observation);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateObservationDto dto)
        {
            if (dto.ProductivityScore < 0 || dto.ProductivityScore > 100)
                return BadRequest("Productivity score must be between 0 and 100.");

            var siteExists = await _context.Sites.AnyAsync(s => s.Id == dto.SiteId);

            if (!siteExists)
                return BadRequest("Invalid site id.");

            var observation = new Observation
            {
                SiteId = dto.SiteId,
                Date = dto.Date,
                ProductivityScore = dto.ProductivityScore,
                Note = dto.Note
            };

            _context.Observations.Add(observation);
            await _context.SaveChangesAsync();

            var response = new ObservationDto
            {
                Id = observation.Id,
                SiteId = observation.SiteId,
                Date = observation.Date,
                ProductivityScore = observation.ProductivityScore,
                Note = observation.Note
            };

            return CreatedAtAction(nameof(GetById), new { id = observation.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateObservationDto dto)
        {
            if (dto.ProductivityScore < 0 || dto.ProductivityScore > 100)
                return BadRequest("Productivity score must be between 0 and 100.");

            var observation = await _context.Observations.FindAsync(id);

            if (observation == null)
                return NotFound();

            observation.Date = dto.Date;
            observation.ProductivityScore = dto.ProductivityScore;
            observation.Note = dto.Note;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var observation = await _context.Observations.FindAsync(id);

            if (observation == null)
                return NotFound();

            _context.Observations.Remove(observation);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}