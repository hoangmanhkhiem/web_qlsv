using Microsoft.EntityFrameworkCore;
using qlsv.Models;

namespace qlsv.Data
{
    public class SessionDbContext : DbContext
    {
        // Variables
        public DbSet<AccessToken> AccessTokens { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<UserToken> JwtUserTokens { get; set; }

        // Constructors
        public SessionDbContext(DbContextOptions<SessionDbContext> options)
            : base(options)
        {
        }

    }
}