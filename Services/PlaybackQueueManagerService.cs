using Vibes.Models;
using Vibes.Interfaces;
using Vibes.Views;

namespace Vibes.Services
{
    public class PlaybackQueueManagerService : IPlaybackQueueManagerService
    {
        private readonly List<Track> _futureQueue = new List<Track>();
        private readonly Stack<Track> _historyStack = new Stack<Track>();
        private Track? _currentTrack;
        private AudioPlayerControl? _audioPlayer;
        private bool _isInternalChanging = false;

        public Track? CurrentTrack => _currentTrack;

        public PlaybackQueueManagerService() { }

        public void SetAudioPlayer(AudioPlayerControl audioPlayer)
        {
            _audioPlayer = audioPlayer;
            _audioPlayer.PlaybackFinished += AudioPlayer_PlaybackFinished;
        }

        private async void AudioPlayer_PlaybackFinished(object? sender, EventArgs e)
        {
            if (_isInternalChanging) return;

            await PlayNextTrackAsync();
        }

        public async void PlayPlaylist(List<Track> playlistTracks, int startingIndex = 0)
        {
            if (playlistTracks == null || playlistTracks.Count == 0) return;

            _futureQueue.Clear();
            _historyStack.Clear();
            _futureQueue.AddRange(playlistTracks);

            await PlayNextTrackAsync();
        }

        public async void PlaySearchTrackNow(Track newTrack)
        {
            if (newTrack == null) return;

            _futureQueue.Insert(0, newTrack);

            await PlayNextTrackAsync();
        }

        public async Task PlayNextTrackAsync()
        {
            _isInternalChanging = true;

            try
            {
                if (_futureQueue.Count == 0) return;

                if (_currentTrack != null)
                {
                    _historyStack.Push(_currentTrack);
                }

                _currentTrack = _futureQueue[0];
                _futureQueue.RemoveAt(0);

                if (!string.IsNullOrEmpty(_currentTrack.StreamUrl) && _audioPlayer != null)
                {
                    await _audioPlayer.PlayStreamAsync(_currentTrack.StreamUrl);
                }
            }
            finally
            {
                _isInternalChanging = false;
            }
        }

        public async void PlayPreviousTrack()
        {
            _isInternalChanging = true;

            try
            {
                if (_historyStack.Count == 0) return;

                if (_currentTrack != null)
                {
                    _futureQueue.Insert(0, _currentTrack);
                }

                _currentTrack = _historyStack.Pop();

                if (!string.IsNullOrEmpty(_currentTrack.StreamUrl) && _audioPlayer != null)
                {
                    await _audioPlayer.PlayStreamAsync(_currentTrack.StreamUrl);
                }
            }
            finally
            { 
                _isInternalChanging = false; 
            }
        }
    }
}