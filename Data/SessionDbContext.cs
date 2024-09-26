using Microsoft.EntityFrameworkCore;
using qlsv.Models;

namespace qlsv.Data
{
    public class SessionDbContext : DbContext
    {
        // Variables
        public DbContext AccessToken;
        public DbContext RefreshToken;
        public DbContext UserToken;

        // Constructors
        public SessionDbContext(DbContextOptions<SessionDbContext> options)
            : base(options)
        {
        }

    }
}