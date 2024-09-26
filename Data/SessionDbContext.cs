using Microsoft.EntityFrameworkCore;
using qlsv.Models;

namespace qlsv.Data
{
    public class SessionDbContext : DbContext
    {
        // Variables
        public DbSet<AccessToken> AccessToken { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }
        public DbSet<UserToken> UserToken { get; set; }

        // Constructors
        public SessionDbContext(DbContextOptions<SessionDbContext> options)
            : base(options)
        {
        }

    }
}