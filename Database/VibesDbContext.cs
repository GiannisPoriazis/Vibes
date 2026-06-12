using Microsoft.EntityFrameworkCore;
using Vibes.Models;

namespace Vibes.Database
{
    internal class VibesDbContext: DbContext
    {
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Track> Tracks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=portfolio.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Playlist>().HasKey(p => p.Id);
            modelBuilder.Entity<Track>().HasKey(t => t.Id);
        }
    }
}
