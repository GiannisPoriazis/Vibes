using System.Drawing.Drawing2D;
using Vibes.Extensions;
using Vibes.Interfaces;
using Vibes.Models;

namespace Vibes.Views
{
    public partial class SearchBarControl : UserControl
    {
        private readonly IPlaybackQueueManagerService _queueManager;
        private readonly IAudioStreamingService _streamingService;
        private readonly System.Windows.Forms.Timer _debounceTimer;

        private readonly System.Windows.Forms.Timer _skeletonTimer;
        private int _skeletonAlpha = 30;
        private bool _skeletonIncreasing = true;
        private bool _isLoading = false;

        public event EventHandler<TrackSelectedEventArgs>? TrackSelected;

        public SearchBarControl(IPlaybackQueueManagerService queueManager, IAudioStreamingService audioStreamingService)
        {
            _queueManager = queueManager;
            _streamingService = audioStreamingService;

            _debounceTimer = new System.Windows.Forms.Timer { Interval = 350 };
            _debounceTimer.Tick += DebounceTimer_Tick;

            _skeletonTimer = new System.Windows.Forms.Timer { Interval = 50 };
            _skeletonTimer.Tick += SkeletonTimer_Tick;

            InitializeComponent();
            Load += SearchBarControl_Load;
        }

        private void SearchBarControl_Load(object? sender, EventArgs e)
        {
            Form topLevelForm = this.FindForm() ?? (Form)Application.OpenForms[0]!;

            if (searchResultsView.Columns.Count == 0)
            {
                searchResultsView.Columns.Add("MainData", searchResultsView.ClientSize.Width);
                var heightSpacer = new ImageList { ImageSize = new Size(48, 48) };
                searchResultsView.SmallImageList = heightSpacer;
            }

            topLevelForm.Controls.Add(searchResultsView);

            var clickFilter = new ClickOutsideMessageFilter(
                searchResultsView,
                searchTextBox,
                () => searchResultsView.Visible = false
            );

            Application.AddMessageFilter(clickFilter);

            Disposed += (s, ev) =>
            {
                _debounceTimer.Stop();
                _debounceTimer.Dispose();
                _skeletonTimer.Stop();
                _skeletonTimer.Dispose();
                Application.RemoveMessageFilter(clickFilter);
            };
        }

        private void SkeletonTimer_Tick(object? sender, EventArgs e)
        {
            double totalMilliseconds = DateTime.Now.TimeOfDay.TotalMilliseconds;
            double speedDivider = 800.0;
            double sineWave = Math.Sin(totalMilliseconds / speedDivider);

            _skeletonAlpha = (int)(20 + ((sineWave + 1) / 2) * 45);

            if (searchResultsView.Visible) searchResultsView.Invalidate();
        }

        private void SearchTextBox_TextChanged(object? sender, EventArgs e)
        {
            _debounceTimer.Stop();
            _debounceTimer.Start();
        }

        private async void DebounceTimer_Tick(object? sender, EventArgs e)
        {
            _debounceTimer.Stop();
            await ExecuteSearchQueryAsync();
        }

        private void ShowSkeletonLoaders()
        {
            _isLoading = true;
            searchResultsView.Items.Clear();

            for (int i = 0; i < 6; i++)
            {
                searchResultsView.Items.Add(new ListViewItem(string.Empty) { Tag = null });
            }

            PositionAndShowResults();
            _skeletonTimer.Start();
        }

        private void PositionAndShowResults()
        {
            if (searchContainerPanel != null)
            {
                Form topLevelForm = this.FindForm() ?? Application.OpenForms[0]!;
                Point globalScreenPoint = searchContainerPanel.Parent.PointToScreen(searchContainerPanel.Location);
                Point formRelativePoint = topLevelForm.PointToClient(globalScreenPoint);

                searchResultsView.Location = new Point(formRelativePoint.X, formRelativePoint.Y + searchContainerPanel.Height + 4);
                searchResultsView.Width = searchContainerPanel.Width;
                searchResultsView.Visible = true;
                searchResultsView.BringToFront();
            }
        }

        private async Task ExecuteSearchQueryAsync()
        {
            string query = searchTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(query) || query.Length < 2)
            {
                _skeletonTimer.Stop();
                searchResultsView.Visible = false;
                return;
            }

            ShowSkeletonLoaders();

            try
            {
                var tracks = await _streamingService.SearchTracksAsync(query);

                _skeletonTimer.Stop();
                _isLoading = false;
                searchResultsView.Items.Clear();

                foreach (var track in tracks)
                {
                    ListViewItem item = new ListViewItem(track.Title) { Tag = track };
                    searchResultsView.Items.Add(item);
                }

                if (searchResultsView.Items.Count > 0)
                {
                    PositionAndShowResults();
                }
                else
                {
                    searchResultsView.Visible = false;
                }
            }
            catch
            {
                _skeletonTimer.Stop();
                _isLoading = false;
                searchResultsView.Visible = false;
            }
        }

        private void SearchContainerPanel_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (var bgBrush = new SolidBrush(Color.FromArgb(36, 36, 36)))
            using (var path = new GraphicsPath())
            {
                int r = searchContainerPanel.Height - 1;
                path.AddArc(0, 0, r, r, 90, 180);
                path.AddArc(searchContainerPanel.Width - r - 1, 0, r, r, 270, 180);
                path.CloseFigure();
                g.FillPath(bgBrush, path);
            }

            using (var iconPen = new Pen(Color.FromArgb(180, 180, 180), 2f))
            {
                int cx = 18;
                int cy = searchContainerPanel.Height / 2 - 1;
                int radius = 6;
                g.DrawEllipse(iconPen, cx - radius, cy - radius, radius * 2, radius * 2);
                g.DrawLine(iconPen, cx + 4, cy + 4, cx + 10, cy + 10);
            }
        }

        private void SearchResultsView_Click(object? sender, EventArgs e)
        {
            if (_isLoading || searchResultsView.SelectedItems.Count == 0) return;

            ListViewItem selectedItem = searchResultsView.SelectedItems[0];
            if (selectedItem.Tag is not Track selectedTrack) return;

            searchResultsView.Visible = false;

            if (!string.IsNullOrEmpty(selectedTrack?.StreamUrl))
            {
                var pageTitle = selectedTrack.Title;
                var metadata = $"Single • {selectedTrack.Artist} • {selectedTrack.FormattedDuration}";
                var tracks = new List<Track> { selectedTrack };

                var args = new TrackSelectedEventArgs(pageTitle, metadata, tracks, selectedTrack);
                TrackSelected?.Invoke(this, args);
            }
        }

        private async void SearchTextBox_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                searchResultsView.Visible = false;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                _debounceTimer.Stop();
                e.Handled = true;
                e.SuppressKeyPress = true;
                await ExecuteSearchQueryAsync();
            }
        }

        private void SearchResultsView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = false;
        }

        private void SearchResultsView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            Rectangle bounds = e.Bounds;

            if (_isLoading || e?.Item?.Tag == null)
            {
                using (var bgBrush = new SolidBrush(Color.FromArgb(20, 20, 20)))
                {
                    g.FillRectangle(bgBrush, bounds);
                }

                int pad = 6;
                int imgS = bounds.Height - (pad * 2);
                var imgR = new Rectangle(bounds.X + pad, bounds.Y + pad, imgS, imgS);

                using (var skeletonBrush = new SolidBrush(Color.FromArgb(_skeletonAlpha, 255, 255, 255)))
                {
                    using (var path = new GraphicsPath())
                    {
                        int radius = 4; int d = radius * 2;
                        path.AddArc(imgR.X, imgR.Y, d, d, 180, 90);
                        path.AddArc(imgR.Right - d, imgR.Y, d, d, 270, 90);
                        path.AddArc(imgR.Right - d, imgR.Bottom - d, d, d, 0, 90);
                        path.AddArc(imgR.X, imgR.Bottom - d, d, d, 90, 90);
                        path.CloseFigure();
                        g.FillPath(skeletonBrush, path);
                    }

                    int startX = imgR.Right + 12;

                    g.FillRectangle(skeletonBrush, new Rectangle(startX, bounds.Y + 10, 180, 12));

                    g.FillRectangle(skeletonBrush, new Rectangle(startX, bounds.Y + 28, 100, 9));
                }
                return;
            }

            if (e.Item.Tag is not Track track) return;

            bool isSelected = e.Item.Selected;
            bool isHovered = searchResultsView.IsRowHovered(e.ItemIndex);
            Color rowColor = (isSelected || isHovered) ? Color.FromArgb(45, 45, 45) : Color.FromArgb(20, 20, 20);

            using (var bgBrush = new SolidBrush(rowColor))
            {
                g.FillRectangle(bgBrush, bounds);
            }

            int padding = 6;
            int imgSize = bounds.Height - (padding * 2);
            var imgRect = new Rectangle(bounds.X + padding, bounds.Y + padding, imgSize, imgSize);

            if (track.CachedCover != null)
            {
                using (var path = new GraphicsPath())
                {
                    int radius = 4; int d = radius * 2;
                    path.AddArc(imgRect.X, imgRect.Y, d, d, 180, 90);
                    path.AddArc(imgRect.Right - d, imgRect.Y, d, d, 270, 90);
                    path.AddArc(imgRect.Right - d, imgRect.Bottom - d, d, d, 0, 90);
                    path.AddArc(imgRect.X, imgRect.Bottom - d, d, d, 90, 90);
                    path.CloseFigure();

                    g.SetClip(path);
                    g.DrawImage(track.CachedCover, imgRect);
                    g.ResetClip();
                }
            }
            else
            {
                using var fallbackBrush = new SolidBrush(Color.FromArgb(40, 40, 40));
                g.FillRectangle(fallbackBrush, imgRect);
            }

            int textStartX = imgRect.Right + 12;

            using (var titleFont = new Font("Segoe UI", 10, FontStyle.Bold))
            using (var titleBrush = new SolidBrush(Color.White))
            {
                g.DrawString(track.Title, titleFont, titleBrush, textStartX, bounds.Y + 6);
            }

            string subtitleText = $"{track.Type} • {track.Artist}";
            using (var subFont = new Font("Segoe UI", 9, FontStyle.Regular))
            using (var subBrush = new SolidBrush(Color.FromArgb(160, 160, 160)))
            {
                g.DrawString(subtitleText, subFont, subBrush, textStartX, bounds.Y + 26);
            }
        }
    }
}
