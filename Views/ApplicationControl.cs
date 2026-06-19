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

        private void PlaylistView_DrawSubItem(object? sender, DrawListViewSubItemEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            if (e.Item?.Tag is not Playlist playlist) return;

            bool isSelected = e.Item.Selected;
            bool isHovered = playlistView.IsRowHovered(e.ItemIndex);

            Color bg = Color.FromArgb(18, 18, 18);
            if (isSelected) bg = Color.FromArgb(40, 40, 40);
            else if (isHovered) bg = Color.FromArgb(28, 28, 28);

            using (var bgBrush = new SolidBrush(bg))
            {
                g.FillRectangle(bgBrush, e.Bounds);
            }

            if (e.ColumnIndex == 1)
            {
                if (isHovered || isSelected)
                {
                    string ellipsis = "•••";
                    Font boldFont = new Font("Segoe UI", 10, FontStyle.Bold);
                    TextRenderer.DrawText(g, ellipsis, boldFont, e.Bounds, Color.FromArgb(200, 200, 200),
                        TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
                }
                return; 
            }

            if (e.ColumnIndex == 0)
            {
                Image coverArt = _fallbackCoverImage;

                if (playlist.CachedCover == null)
                {
                    playlist.CachedCover = (Bitmap)CreateDefaultCollectionPlaceholder(124, 124);
                }

                if (playlist.Tracks != null && playlist.Tracks.Count > 0)
                {
                    var firstTrack = playlist.Tracks.First();

                    if (firstTrack.CachedCover != null)
                    {
                        coverArt = firstTrack.CachedCover;
                    }
                    else if (_sidebarTrackImageCache.TryGetValue(firstTrack.Id, out var cachedImage))
                    {
                        coverArt = cachedImage;
                    }
                    else if (!string.IsNullOrEmpty(firstTrack.CoverUrl))
                    {
                        TriggerSidebarThumbnailDownloadAsync(firstTrack.Id, firstTrack.CoverUrl, e.ItemIndex);
                    }
                }

                int imgSize = 48;
                int margin = 8;
                int imgX = e.Bounds.Left + margin;
                int imgY = e.Bounds.Top + ((e.Bounds.Height - imgSize) / 2);
                g.DrawImage(coverArt, new Rectangle(imgX, imgY, imgSize, imgSize));

                int textX = imgX + imgSize + 12;

                string titleText = playlist.Name;
                Font titleFont = new Font("Segoe UI", 10, isSelected ? FontStyle.Bold : FontStyle.Regular);
                Color titleColor = isSelected || isHovered ? Color.White : Color.FromArgb(220, 220, 220);
                Size titleSize = TextRenderer.MeasureText(titleText, titleFont);
                int titleY = e.Bounds.Top + (e.Bounds.Height / 2) - titleSize.Height + 2;
                TextRenderer.DrawText(g, titleText, titleFont, new Point(textX, titleY), titleColor, TextFormatFlags.NoPadding);

                string owner = _authService?.CurrentUser?.Username ?? "You";
                int trackCount = playlist.Tracks?.Count ?? 0;
                string subtitleText = $"Playlist • {owner} • {trackCount} tracks";

                Font subFont = new Font("Segoe UI", 9, FontStyle.Regular);
                Color subColor = Color.FromArgb(160, 160, 160);
                int subtitleY = e.Bounds.Top + (e.Bounds.Height / 2) + 2;
                TextRenderer.DrawText(g, subtitleText, subFont, new Point(textX, subtitleY), subColor, TextFormatFlags.NoPadding);
            }
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
            catch(Exception ex)
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
            if (playlistView.SelectedItems.Count == 0)
            {
                return;
            }

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
                "Enter new playlist name:",
                "Create Playlist",
                "New Playlist"
            ).Trim();

            if (string.IsNullOrEmpty(playlistName)) return;

            using (var context = new Database.VibesDbContext())
            {
                var newPlaylist = new Playlist { 
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

        private Image CreateDefaultCollectionPlaceholder(int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;

                using (LinearGradientBrush lgb = new LinearGradientBrush(new Point(0, 0), new Point(width, height),
                    Color.FromArgb(70, 40, 180), Color.FromArgb(140, 120, 240)))
                {
                    g.FillRectangle(lgb, 0, 0, width, height);
                }

                Font iconFont = new Font("Segoe UI", 16, FontStyle.Bold);
                TextRenderer.DrawText(g, "🎵", iconFont, new Rectangle(0, 0, width, height), Color.White,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
            return bmp;
        }
    }
}