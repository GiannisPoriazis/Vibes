using Vibes.Models;
using Vibes.Views;

namespace Vibes.Interfaces
{
    public interface IPlaybackQueueManagerService
    {
        Track? CurrentTrack { get; }
        void SetAudioPlayer(AudioPlayerControl audioPlayer);
        void PlayPlaylist(IEnumerable<Track> playlistTracks, int startingIndex = 0);
        void PlaySearchTrackNow(Track newTrack);
        void AppendToFutureQueue(Track newTrack);
        Task PlayNextTrackAsync();
        void PlayPreviousTrack();
    }
}
