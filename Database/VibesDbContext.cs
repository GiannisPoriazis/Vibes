using Microsoft.EntityFrameworkCore;
using Vibes.Models;

namespace Vibes.Database
{
    public class VibesDbContext: DbContext
    {
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Track> Tracks { get; set; }

        public VibesDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=vibes.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Playlist>().HasKey(p => p.Id);
            modelBuilder.Entity<Track>().HasKey(t => t.Id);

            modelBuilder.Entity<Track>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<Song>("Song")
                .HasValue<Podcast>("Podcast");

            modelBuilder.Entity<Playlist>()
                .HasMany(p => p.Tracks)
                .WithMany(t => t.Playlists)
                .UsingEntity(j => j.ToTable("PlaylistTrack"));
        }
    }
}
