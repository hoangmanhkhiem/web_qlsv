using Microsoft.EntityFrameworkCore;
using qlsv.Models;

namespace qlsv.Data
{
    public class ViewDbContext : DbContext
    {   
        // Variables
        public DbSet<ViewNavItem> navBarAdminPage { get; set; }
        public DbSet<ViewNavItem> navBarStudentPage { get; set; }
        public DbSet<ViewNavItem> navBarTeacherPage { get; set; }

        // Constructor
        public ViewDbContext(DbContextOptions<ViewDbContext> options)
            : base(options)
        {
        }
    }
}