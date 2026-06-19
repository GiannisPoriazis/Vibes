using Vibes.Models;

namespace Vibes.Interfaces
{
    public interface IPlaylistService : IRepository<Playlist>
    {
        event EventHandler<Playlist> PlaylistChanged;
        Task<IEnumerable<Playlist>> GetPlaylistsWithoutTrackAsync(int trackId);
        Task AddTrackToPlaylistAsync(int playlistId, Track trackToAdd);
        Task RemoveTrackFromPlaylistAsync(int playlistId, int trackId);
    }
}
