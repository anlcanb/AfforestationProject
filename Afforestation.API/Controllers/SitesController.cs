using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Afforestation.Core.Entities;
using Afforestation.API.Data;
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
    }
}
