using Microsoft.EntityFrameworkCore;
using qlsv.Models;

namespace qlsv.Data
{
    public class ViewDbContext : DbContext
    {
        // Variables
        public DbSet<ViewNavItem> NavBarPages { get; set; }

        // Constructor
        public ViewDbContext(DbContextOptions<ViewDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Call the base method to ensure any default configurations are applied
            base.OnModelCreating(modelBuilder);

            // Auto generated primary key
            modelBuilder.Entity<ViewNavItem>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();
            
        }
    }
}