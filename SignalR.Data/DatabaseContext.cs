using Microsoft.EntityFrameworkCore;

namespace SignalR.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Stream> Streams { get; set; }
    }
}