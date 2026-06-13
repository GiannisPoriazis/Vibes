using System;
using System.Collections.Generic;
using System.Text;

namespace Vibes.Interfaces
{
    internal interface IAudioStreamingService
    {
        Task<List<TrackSearchResult>> SearchTracksAsync(string query);
    }
}
