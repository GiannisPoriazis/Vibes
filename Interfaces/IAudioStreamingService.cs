using Vibes.Models;

namespace Vibes.Interfaces
{
    public interface IAudioStreamingService
    {
        Task<List<Track>> SearchTracksAsync(string query);
    }
}
