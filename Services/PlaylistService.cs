using Microsoft.EntityFrameworkCore;
using Vibes.Database;
using Vibes.Interfaces;
using Vibes.Models;

namespace Vibes.Services
{
    public class PlaylistService : IPlaylistService
    {
        private readonly IDbContextFactory<VibesDbContext> _contextFactory;
        
        public event EventHandler<Playlist> PlaylistChanged;

        public PlaylistService(IDbContextFactory<VibesDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<Playlist?> GetByIdAsync(int id)
        {
            using var db = _contextFactory.CreateDbContext();
            return await db.Playlists.Include(p => p.Tracks).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Playlist>> GetAllAsync()
        {
            using var db = _contextFactory.CreateDbContext();
            return await db.Playlists.ToListAsync();
        }

        public async Task AddAsync(Playlist entity)
        {
            using var db = _contextFactory.CreateDbContext();
            await db.Playlists.AddAsync(entity);
            await db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Playlist entity)
        {
            using var db = _contextFactory.CreateDbContext();
            db.Playlists.Update(entity);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var db = _contextFactory.CreateDbContext();
            var playlist = await db.Playlists.FindAsync(id);
            if (playlist != null)
            {
                db.Playlists.Remove(playlist);
                await db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Playlist>> GetPlaylistsWithoutTrackAsync(int trackId)
        {
            using var db = _contextFactory.CreateDbContext();
            return await db.Playlists
                .Where(p => !p.Tracks.Any(t => t.Id == trackId))
                .ToListAsync();
        }

        public async Task AddTrackToPlaylistAsync(int playlistId, Track trackToAdd)
        {
            using var db = _contextFactory.CreateDbContext();
            var playlist = await db.Playlists.Include(p => p.Tracks).FirstOrDefaultAsync(p => p.Id == playlistId);
            var track = await db.Tracks.FindAsync(trackToAdd.Id);

            if (playlist != null && track != null && !playlist.Tracks.Any(t => t.Id == trackToAdd.Id))
            {
                playlist.Tracks.Add(track);
                await db.SaveChangesAsync();

                PlaylistChanged.Invoke(this, playlist);
            }
            else if (playlist != null && track == null)
            {
                db.Tracks.Add(trackToAdd);
                playlist.Tracks.Add(trackToAdd);
                await db.SaveChangesAsync();

                PlaylistChanged.Invoke(this, playlist);
            }
        }

        public async Task RemoveTrackFromPlaylistAsync(int playlistId, int trackId)
        {
            using var db = _contextFactory.CreateDbContext();
            var playlist = await db.Playlists.Include(p => p.Tracks).FirstOrDefaultAsync(p => p.Id == playlistId);
            if (playlist != null)
            {
                var track = playlist.Tracks.FirstOrDefault(t => t.Id == trackId);
                if (track != null)
                {
                    playlist.Tracks.Remove(track);
                    await db.SaveChangesAsync();

                    PlaylistChanged.Invoke(this, playlist);
                }
            }
        }
    }
}