using Microsoft.Extensions.Logging;
using Vibes.Interfaces;
using Vibes.Models;

namespace Vibes.Views
{
    public partial class HomeControl : UserControl
    {
        private readonly IAudioStreamingService? _audioStreamingService;
        private readonly IAvatarService? _avatarService;
        private readonly ILogger<HomeControl>? _logger;

        public event EventHandler<TrackSelectedEventArgs>? EntitySelected;

        public HomeControl()
        {
            InitializeComponent();
        }

        public HomeControl(IAudioStreamingService audioStreamingService, IAvatarService avatarService, ILogger<HomeControl> logger) : this()
        {
            _audioStreamingService = audioStreamingService;
            _avatarService = avatarService;
            _logger = logger;

            Load += async (s, e) => await LoadHomepageFeedsAsync();
        }

        private async Task LoadHomepageFeedsAsync()
        {
            if (_audioStreamingService == null) return;

            try
            {
                ShowSkeletons(hotRightNowLayout);
                ShowSkeletons(allTimeGreatestLayout);
                ShowSkeletons(vibesPicksLayout);

                var hotTracksTask = _audioStreamingService.GetTracksAsync(limit: 6, order: "popularity_week");
                var allTimeTracksTask = _audioStreamingService.GetTracksAsync(limit: 6, order: "listens_total");
                var vibesPicksTask = _audioStreamingService.GetTracksAsync(limit: 6, featured: true);

                await Task.WhenAll(hotTracksTask, allTimeTracksTask, vibesPicksTask);

                PopulateShelf(hotRightNowLayout, hotTracksTask.Result);
                PopulateShelf(allTimeGreatestLayout, allTimeTracksTask.Result);
                PopulateShelf(vibesPicksLayout, vibesPicksTask.Result);
            }
            catch (Exception ex)
            {
                _logger?.LogError("Error loading feeds: {Message}", ex.Message);
            }
        }

        private void ShowSkeletons(FlowLayoutPanel shelf)
        {
            shelf.Controls.Clear();
            for (int i = 0; i < 6; i++)
            {
                shelf.Controls.Add(new MusicCardSkeleton());
            }
        }

        private void PopulateShelf(FlowLayoutPanel shelf, List<Track> tracks)
        {
            shelf.Controls.Clear();
            if (tracks == null) return;

            foreach (var track in tracks)
            {
                var card = new MusicCardControl(track, _avatarService);
                var metadata = $"Single • {track.Artist} • {track.FormattedDuration}";
                var selectedTrackList = new List<Track> { track };

                card.Click += (s, e) => EntitySelected?.Invoke(this, new TrackSelectedEventArgs(track.Title, metadata, selectedTrackList, track, false));
                shelf.Controls.Add(card);
            }
        }
    }
}