using Vibes.Models;
using Vibes.Views;

namespace Vibes.Interfaces
{
    public interface IPlaybackQueueManagerService
    {
        Track? CurrentTrack { get; }
        void SetAudioPlayer(AudioPlayerControl audioPlayer);
        void PlayPlaylist(List<Track> playlistTracks, int startingIndex = 0);
        void PlaySearchTrackNow(Track newTrack);
        Task PlayNextTrackAsync();
        void PlayPreviousTrack();
    }
}
