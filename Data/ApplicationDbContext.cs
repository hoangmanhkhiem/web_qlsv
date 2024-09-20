using Microsoft.EntityFrameworkCore;
//
using web_qlsv.Models;

namespace web_qlsv.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ErrorViewModel> ErrorViewModels { get; set; }
    }
}