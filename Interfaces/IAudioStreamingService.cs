using Vibes.Models;

namespace Vibes.Interfaces
{
    public interface IAudioStreamingService
    {
        Task<List<Track>> SearchTracksAsync(string query);
        Task<List<Track>> GetTracksAsync(int limit, string? order = null, bool featured = false);
    }
}