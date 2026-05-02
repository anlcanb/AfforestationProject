using Afforestation.API.Data;
using Afforestation.Core.DTO;
using Afforestation.Core.Entities;
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
            var sites = await _context.Sites
                .Select(s => new SiteDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Latitude = s.Latitude,
                    Longitude = s.Longitude,
                    City = s.City,
                    District = s.District,
                    PlantingData = s.PlantingData,
                    Status = s.Status
                })
                .ToListAsync();

            return Ok(sites);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var site = await _context.Sites
                .Where(s => s.Id == id)
                .Select(s => new SiteDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Latitude = s.Latitude,
                    Longitude = s.Longitude,
                    City = s.City,
                    District = s.District,
                    PlantingData = s.PlantingData,
                    Status = s.Status
                })
                .FirstOrDefaultAsync();

            if (site == null)
                return NotFound();

            return Ok(site);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSiteDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("Site name is required.");

            if (string.IsNullOrWhiteSpace(dto.City))
                return BadRequest("City is required.");

            if (string.IsNullOrWhiteSpace(dto.District))
                return BadRequest("District is required.");

            if (string.IsNullOrWhiteSpace(dto.Status))
                return BadRequest("Status is required.");

            var site = new Site
            {
                Name = dto.Name,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                City = dto.City,
                District = dto.District,
                PlantingData = dto.PlantingData,
                Status = dto.Status
            };

            _context.Sites.Add(site);
            await _context.SaveChangesAsync();

            var response = new SiteDto
            {
                Id = site.Id,
                Name = site.Name,
                Latitude = site.Latitude,
                Longitude = site.Longitude,
                City = site.City,
                District = site.District,
                PlantingData = site.PlantingData,
                Status = site.Status
            };

            return CreatedAtAction(nameof(GetById), new { id = site.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateSiteDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("Site name is required.");

            if (string.IsNullOrWhiteSpace(dto.City))
                return BadRequest("City is required.");

            if (string.IsNullOrWhiteSpace(dto.District))
                return BadRequest("District is required.");

            if (string.IsNullOrWhiteSpace(dto.Status))
                return BadRequest("Status is required.");

            var site = await _context.Sites.FindAsync(id);

            if (site == null)
                return NotFound();

            site.Name = dto.Name;
            site.Latitude = dto.Latitude;
            site.Longitude = dto.Longitude;
            site.City = dto.City;
            site.District = dto.District;
            site.PlantingData = dto.PlantingData;
            site.Status = dto.Status;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var site = await _context.Sites.FindAsync(id);

            if (site == null)
                return NotFound();

            _context.Sites.Remove(site);
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