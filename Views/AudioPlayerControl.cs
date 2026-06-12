using NAudio.Wave;

namespace Vibes.Views
{
    public partial class AudioPlayerControl : UserControl
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private StreamMediaFoundationReader? _audioReader;
        private WaveOutEvent? _outputDevice;
        private MemoryStream? _memoryCacheStream;
        private bool _isOpening = false;

        public AudioPlayerControl()
        {
            InitializeComponent();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) VibesAudioEngine/1.0");
        }

        public async Task PlayStreamAsync(string streamUrl)
        {
            if (_isOpening) return;
            _isOpening = true;

            try
            {
                Stop();

                // 1. Fetch the raw audio stream data from the web using HttpClient
                byte[] audioBytes = await _httpClient.GetByteArrayAsync(streamUrl);

                // 2. Offload the COM initialization using our byte payload to a separate thread
                await Task.Run(() =>
                {
                    // Pack the array into a volatile memory stream (RAM only)
                    _memoryCacheStream = new MemoryStream(audioBytes);

                    // Wrap it with StreamMediaFoundationReader instead of MediaFoundationReader
                    _audioReader = new StreamMediaFoundationReader(_memoryCacheStream);
                    _outputDevice = new WaveOutEvent();

                    _outputDevice.Init(_audioReader);
                });

                _outputDevice?.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Audio Engine Streaming Crash: {ex.Message}",
                                "Playback Subsystem Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isOpening = false;
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
            string jazzStream = "https://github.com/rafaelreis-hotmart/Audio-Sample-files/raw/master/sample.mp3";
            await PlayStreamAsync(jazzStream);
        }
    }
}
