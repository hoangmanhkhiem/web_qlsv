using Microsoft.EntityFrameworkCore;
using qlsv.Models;

namespace qlsv.Data
{
    public class ViewDbContext : DbContext
    {
        // Constructor
        public ViewDbContext(DbContextOptions<ViewDbContext> options)
            : base(options)
        {
        }
        // Var
        public DbSet<ViewNavItem> NavItems { get; set; }
    }
}