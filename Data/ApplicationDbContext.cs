using Faculity_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Faculity_System.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Students> Students { get; set; }
        public DbSet<Departments> Departments { get; set; }
    }
}
