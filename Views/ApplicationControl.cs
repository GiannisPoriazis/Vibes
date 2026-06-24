using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Drawing.Drawing2D;
using Vibes.Design;
using Vibes.Extensions;
using Vibes.Interfaces;
using Vibes.Models;

namespace Vibes.Views
{
    public partial class ApplicationControl : UserControl
    {
        private readonly IAuth0Service? _authService;
        private readonly ILogger<ApplicationControl> _logger;
        private readonly IPlaylistService _playlistService;
        private Image _fallbackCoverImage;
        private ContextMenuStrip playlistContextMenu;
        private readonly Dictionary<int, Image> _sidebarTrackImageCache = new();
        private readonly HashSet<int> _pendingImageDownloads = new();

        public event EventHandler<TrackSelectedEventArgs>? PlaylistSelected;

        public ApplicationControl(
            IAuth0Service? auth0Service,
            ILogger<ApplicationControl> logger,
            IPlaylistService playlistService)
        {
            _authService = auth0Service;
            _logger = logger;
            _playlistService = playlistService;

            _playlistService.PlaylistChanged += (s, e) => LoadPlaylists();

            InitializeComponent();

            _fallbackCoverImage = CreateDefaultCollectionPlaceholder(48, 48);

            playlistView.Columns.Add("LibraryItems", 150);
            playlistView.Columns.Add("Actions", 40);
            playlistView.SetRowItemHeight(64);
            playlistView.EnableRowHoverStyles();
            playlistView.DrawSubItem += PlaylistView_DrawSubItem;
            playlistView.Resize += playlistView_Resize;
            playlistView.MouseClick += PlaylistView_MouseClick;

            HandleCreated += (s, e) => LoadPlaylists();
            SetupContextMenu();
        }

        private void LoadPlaylists()
        {
            if (_authService?.CurrentUser == null) return;

            playlistView.BeginUpdate();
            playlistView.Items.Clear();

            using (var context = new Database.VibesDbContext())
            {
                var playlists = context.Playlists
                                       .Include(p => p.Tracks)
                                       .Where(p => p.UserId == _authService.CurrentUser.Subject)
                                       .ToList();

                foreach (var playlist in playlists)
                {
                    ListViewItem item = new ListViewItem(playlist.Name) { Tag = playlist };
                    item.SubItems.Add(string.Empty);
                    playlistView.Items.Add(item);
                }
            }

            playlistView.EndUpdate();
        }

        private void SetupContextMenu()
        {
            playlistContextMenu = new ContextMenuStrip
            {
                BackColor = ColorPalette.CardBackground,
                ForeColor = Color.White,
                Renderer = new ContextMenuThemeRenderer(),
                ShowImageMargin = false
            };

            ToolStripMenuItem playItem = new ToolStripMenuItem("Play");
            ToolStripMenuItem renameItem = new ToolStripMenuItem("Rename");
            ToolStripMenuItem deleteItem = new ToolStripMenuItem("Delete");

            playItem.Click += MenuPlay_Click;
            renameItem.Click += MenuRename_Click;
            deleteItem.Click += MenuDelete_Click;

            playlistContextMenu.Items.AddRange(new ToolStripItem[] { playItem, renameItem, deleteItem });
            playlistView.ContextMenuStrip = playlistContextMenu;
        }

        private void PlaylistView_MouseClick(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            if (playlistView.SelectedItems.Count > 0 && playlistView.SelectedItems[0].Tag is Playlist playlist)
            {
                Playlist selectedPlaylist = playlist;
                var metadata = $"Playlist • {_authService?.CurrentUser?.Username} • {TimeSpan.FromSeconds(playlist.Tracks.Sum(t => t.Duration)).ToString(@"hh\:mm\:ss")}";
                var args = new TrackSelectedEventArgs(playlist.Name, metadata, playlist.Tracks, playlist, false);
                PlaylistSelected?.Invoke(this, args);
            }

            var hitTest = playlistView.HitTest(e.Location);
            if (hitTest.Item == null || hitTest.Item.SubItems.Count <= 1) return;

            var ghostCellBounds = hitTest.Item.SubItems[1].Bounds;

            if (e.X >= ghostCellBounds.Left && e.X <= ghostCellBounds.Right)
            {
                playlistView.SelectedItems.Clear();
                hitTest.Item.Selected = true;

                playlistContextMenu.Show(playlistView, e.Location);
            }
        }

        private async void TriggerSidebarThumbnailDownloadAsync(int trackId, string url, int itemIndex)
        {
            if (_pendingImageDownloads.Contains(trackId)) return;
            _pendingImageDownloads.Add(trackId);

            try
            {
                using var httpClient = new HttpClient();
                byte[] imageBytes = await httpClient.GetByteArrayAsync(url);

                using var ms = new MemoryStream(imageBytes);
                Image downloadedImage = Image.FromStream(ms);

                if (playlistView.Items[itemIndex].Tag is Playlist playlist)
                {
                    playlist.CachedCover = new Bitmap(ms);
                }

                Bitmap optimizedThumbnail = new Bitmap(48, 48);
                using (Graphics g = Graphics.FromImage(optimizedThumbnail))
                {
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(downloadedImage, 0, 0, 48, 48);
                }

                if (!_sidebarTrackImageCache.ContainsKey(trackId))
                {
                    _sidebarTrackImageCache.Add(trackId, optimizedThumbnail);
                }

                if (playlistView.IsHandleCreated && !playlistView.IsDisposed)
                {
                    playlistView.BeginInvoke(new Action(() => {
                        if (itemIndex < playlistView.Items.Count)
                        {
                            playlistView.RedrawItems(itemIndex, itemIndex, false);
                        }
                    }));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            finally
            {
                _pendingImageDownloads.Remove(trackId);
            }
        }

        private void MenuPlay_Click(object? sender, EventArgs e)
        {
            if (playlistView.SelectedItems.Count == 0) return;

            if (playlistView.SelectedItems[0].Tag is Playlist playlist)
            {
                Playlist selectedPlaylist = playlist;
                var metadata = $"Playlist • {_authService?.CurrentUser?.Username} • {TimeSpan.FromSeconds(playlist.Tracks.Sum(t => t.Duration)).ToString(@"hh\:mm\:ss")}";
                var args = new TrackSelectedEventArgs(playlist.Name, metadata, playlist.Tracks, playlist);
                PlaylistSelected?.Invoke(this, args);
            }
        }

        private void MenuRename_Click(object? sender, EventArgs e)
        {
            if (playlistView.SelectedItems.Count == 0) return;

            ListViewItem selectedItem = playlistView.SelectedItems[0];
            Playlist selectedPlaylist = (Playlist)selectedItem.Tag;

            string newName = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter new name:", "Rename Playlist", selectedPlaylist.Name).Trim();

            if (string.IsNullOrEmpty(newName) || newName == selectedPlaylist.Name) return;

            using (var context = new Database.VibesDbContext())
            {
                context.Playlists.Attach(selectedPlaylist);
                selectedPlaylist.Name = newName;
                context.SaveChanges();
            }

            playlistView.Invalidate(selectedItem.Bounds);
        }

        private void MenuDelete_Click(object? sender, EventArgs e)
        {
            if (playlistView.SelectedItems.Count == 0) return;

            ListViewItem selectedItem = playlistView.SelectedItems[0];
            Playlist selectedPlaylist = (Playlist)selectedItem.Tag;

            var confirmResult = MessageBox.Show(
                $"Are you sure you want to delete '{selectedPlaylist.Name}'?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                using (var context = new Database.VibesDbContext())
                {
                    context.Playlists.Remove(selectedPlaylist);
                    context.SaveChanges();
                }

                playlistView.Items.Remove(selectedItem);
            }
        }

        private void AddPlaylistBtn_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_authService?.CurrentUser?.Subject)) return;

            string playlistName = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter new playlist name:", "Create Playlist", "New Playlist").Trim();

            if (string.IsNullOrEmpty(playlistName)) return;

            using (var context = new Database.VibesDbContext())
            {
                var newPlaylist = new Playlist
                {
                    Name = playlistName,
                    UserId = _authService.CurrentUser.Subject,
                };

                context.Playlists.Add(newPlaylist);
                context.SaveChanges();
            }

            LoadPlaylists();
        }

        private void playlistView_Resize(object? sender, EventArgs e)
        {
            if (playlistView.Columns.Count > 1)
            {
                int workingWidth = playlistView.ClientSize.Width;
                playlistView.Columns[1].Width = 40;
                playlistView.Columns[0].Width = Math.Max(100, workingWidth - 40);
            }
        }
    }
}