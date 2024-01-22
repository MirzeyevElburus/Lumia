using Lumia.Models;
using Microsoft.EntityFrameworkCore;

namespace Lumia.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Team>Teams { get; set; }
    }
}
