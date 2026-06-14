using Microsoft.Extensions.Logging;
using NAudio.Wave;
using Serilog.Core;
using Vibes.Interfaces;

namespace Vibes.Views
{
    public partial class AudioPlayerControl : UserControl
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private StreamMediaFoundationReader? _audioReader;
        private WaveOutEvent? _outputDevice;
        private MemoryStream? _memoryCacheStream;
        private readonly ILogger<AudioPlayerControl> _logger;
        private IPlaybackQueueManagerService _playbackQueueManagerService;
        private bool _isOpening = false;
        public event EventHandler? PlaybackFinished;

        public AudioPlayerControl(IPlaybackQueueManagerService playbackQueueManagerService, ILogger<AudioPlayerControl> logger)
        {
            InitializeComponent();
            _playbackQueueManagerService = playbackQueueManagerService;
            _playbackQueueManagerService.SetAudioPlayer(this);
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) VibesAudioEngine/1.0");
            _logger = logger;
        }

        public async Task PlayStreamAsync(string streamUrl)
        {
            if (_isOpening) return;
            _isOpening = true;

            try
            {
                Stop();

                byte[] audioBytes = await _httpClient.GetByteArrayAsync(streamUrl);

                await Task.Run(() =>
                {
                    if (_outputDevice != null)
                    {
                        _outputDevice.PlaybackStopped -= OnPlaybackStopped;
                        _outputDevice.Dispose();
                    }

                    _memoryCacheStream = new MemoryStream(audioBytes);

                    _audioReader = new StreamMediaFoundationReader(_memoryCacheStream);
                    _outputDevice = new WaveOutEvent();
                    _outputDevice.PlaybackStopped += OnPlaybackStopped;

                    _outputDevice.Init(_audioReader);
                });

                _outputDevice?.Play();
                playTrackBtn.IconChar = FontAwesome.Sharp.IconChar.Pause;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while playing audio stream.");
            }
            finally
            {
                _isOpening = false;
            }
        }

        private void OnPlaybackStopped(object? sender, StoppedEventArgs e)
        {
            if (e.Exception == null)
            {
                PlaybackFinished?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                _logger.LogError($"Audio Hardware Error: {e.Exception.Message}");
            }
        }

        public void Stop()
        {
            _outputDevice?.Stop();
            _outputDevice?.Dispose();
            _audioReader?.Dispose();

            _outputDevice = null;
            _audioReader = null;
        }

        private async void playTrackBtn_Click(object? sender, EventArgs e)
        {
            if(_outputDevice?.PlaybackState == PlaybackState.Playing)
            {
                _outputDevice?.Pause();
                playTrackBtn.IconChar = FontAwesome.Sharp.IconChar.Play;
            }
            else if(_outputDevice?.PlaybackState == PlaybackState.Paused)
            {
                _outputDevice?.Play();
                playTrackBtn.IconChar = FontAwesome.Sharp.IconChar.Pause;
            }
        }

        private async void nextTrackBtn_Click(object sender, EventArgs e)
        {
            await _playbackQueueManagerService.PlayNextTrackAsync();
        }

        private void previousTrackBtn_Click(object sender, EventArgs e)
        {
            _playbackQueueManagerService.PlayPreviousTrack();
        }
    }
}
