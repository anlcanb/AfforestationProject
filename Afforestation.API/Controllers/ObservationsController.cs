using Afforestation.API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Afforestation.Core.Entities;

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
            var obser = await _context.Observations.ToListAsync();
            return Ok(obser);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var obser = await _context.Observations.FindAsync(id);
            if (obser == null) return NotFound();
            return Ok(obser);
        }
        [HttpPost]

        public async Task<IActionResult> Create([FromBody] Observation observation)
        {
            _context.Observations.Add(observation);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Create), new { id = observation.Id }, observation);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            var obser = await _context.Observations.FindAsync(id);
            if(obser==null) return NotFound();
            _context.Observations.Remove(obser);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, [FromBody] Observation updatedObservation)
        {
            if (updatedObservation == null) return NotFound();
            if (id != updatedObservation.Id) return BadRequest(nameof(id));

            var obser = await _context.Observations.FindAsync(id);
            if (obser == null) return NotFound();

            obser.Note = updatedObservation.Note;
            obser.Site = updatedObservation.Site;
            obser.Date = updatedObservation.Date;
            obser.ProductivityScore = updatedObservation.ProductivityScore;
            obser.SiteId = updatedObservation.SiteId;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
    }
}
