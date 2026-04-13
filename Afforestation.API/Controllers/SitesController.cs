using Afforestation.API.Data;
using Afforestation.Core.DTO;
using Afforestation.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Afforestation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SitesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SitesController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sites = await _context.Sites.ToListAsync();
            return Ok(sites);
        } 


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var site = await _context.Sites.FindAsync(id);
            if (site == null) return NotFound();
            return Ok(site);
        }


        [HttpPost]

        public async Task<IActionResult> Create([FromBody] Site site)
        {
            _context.Sites.Add(site);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = site.Id }, site);
        }


        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            var site = await _context.Sites.FindAsync(id);          
            if (site == null) return NotFound();

            _context.Sites.Remove(site);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, [FromBody] Site updatedSite)
        {
            if (updatedSite == null) return BadRequest();
            
            var site = await _context.Sites.FindAsync(id);
            if (site == null) return NotFound();
            site.Name = updatedSite.Name;
            site.Longitude = updatedSite.Longitude;
            site.Latitude = updatedSite.Latitude;
            site.Status = updatedSite.Status;
            site.District = updatedSite.District;
            site.PlantingData = updatedSite.PlantingData;
            site.City = updatedSite.City;

            await _context.SaveChangesAsync();

            return NoContent();
        }


            [HttpGet("{siteId}/observations")]

            public async Task<IActionResult> GetObservationsBySiteId(int siteId)
            {
                var siteExists = await _context.Sites.AnyAsync(x => x.Id == siteId);

                if (!siteExists)
                    return NotFound();

                var observations = await _context.Observations
                    .Where(x => x.SiteId == siteId)
                    .OrderByDescending(x => x.Date)
                    .ToListAsync();

                return Ok(observations);
            }

        [HttpGet("map-data")]
        public async Task<IActionResult> GetMapData()
        {
            var mapData = await _context.Sites
                .Select(site => new SiteMapDataDto
                {
                    SiteId = site.Id,
                    Name = site.Name,
                    Latitude = site.Latitude,
                    Longitude = site.Longitude,
                    City = site.City,
                    District = site.District,
                    ProductivityScore = _context.Observations
                        .Where(o => o.SiteId == site.Id)
                        .OrderByDescending(o => o.Date)
                        .Select(o => (int?)o.ProductivityScore)
                        .FirstOrDefault(),
                    Note = _context.Observations
                        .Where(o => o.SiteId == site.Id)
                        .OrderByDescending(o => o.Date)
                        .Select(o => o.Note)
                        .FirstOrDefault(),
                    ObservationDate = _context.Observations
                        .Where(o => o.SiteId == site.Id)
                        .OrderByDescending(o => o.Date)
                        .Select(o => (DateTime?)o.Date)
                        .FirstOrDefault()
                })
                .ToListAsync();

            return Ok(mapData);
        }

    }
}


