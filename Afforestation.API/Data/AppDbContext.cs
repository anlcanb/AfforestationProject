using Microsoft.EntityFrameworkCore;
using Afforestation.Core.Entities;

namespace Afforestation.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options) { }
        public DbSet<Site> Sites { get; set; }
        public DbSet<Observation> Observations { get; set; }
    }
}
