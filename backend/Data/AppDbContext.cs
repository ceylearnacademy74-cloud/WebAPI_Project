using backend_EM.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_EM.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<employee> Employees { get; set; }
    }
}

