using System.Drawing.Drawing2D;
using Vibes.Interfaces;
using Vibes.Models;

namespace Vibes.Views
{
    public partial class SearchBarControl : UserControl
    {
        private IPlaybackQueueManagerService _queueManager;
        private IAudioStreamingService _streamingService;

        public SearchBarControl(IPlaybackQueueManagerService queueManager, IAudioStreamingService audioStreamingService)
        {
            InitializeComponent();
            _queueManager = queueManager;
            _streamingService = audioStreamingService;
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

            Disposed += (s, ev) => Application.RemoveMessageFilter(clickFilter);
        }

        private async void SearchButton_Click(object? sender, EventArgs e)
        {
            string query = searchTextBox.Text;
            if (string.IsNullOrWhiteSpace(query)) return;

            var tracks = await _streamingService.SearchTracksAsync(query);
            searchResultsView.Items.Clear();

            using var client = new HttpClient();

            foreach (var track in tracks)
            {
                if (!string.IsNullOrEmpty(track.CoverUrl))
                {
                    try
                    {
                        byte[] imgBytes = await client.GetByteArrayAsync(track.CoverUrl);
                        using var ms = new MemoryStream(imgBytes);
                        track.CachedCover = new Bitmap(ms);
                    }
                    catch { }
                }

                ListViewItem item = new ListViewItem(track.Title) { Tag = track };
                searchResultsView.Items.Add(item);
            }

            if (searchResultsView.Items.Count > 0 && searchTextBox.Parent != null)
            {
                Form topLevelForm = this.FindForm() ?? Application.OpenForms[0]!;
                Point globalScreenPoint = searchTextBox.Parent.PointToScreen(searchTextBox.Location);
                Point formRelativePoint = topLevelForm.PointToClient(globalScreenPoint);

                searchResultsView.Location = new Point(formRelativePoint.X, formRelativePoint.Y + searchTextBox.Height);
                searchResultsView.Visible = true;
                searchResultsView.BringToFront();
                searchResultsView.Focus();
            }
        }

        private async void SearchResultsView_Click(object? sender, EventArgs e)
        {
            if (searchResultsView.SelectedItems.Count == 0) return;

            ListViewItem selectedItem = searchResultsView.SelectedItems[0];
            Track selectedTrack = (Track)selectedItem.Tag!;

            searchResultsView.Visible = false;

            if (!string.IsNullOrEmpty(selectedTrack?.StreamUrl))
            {
                _queueManager.PlaySearchTrackNow(selectedTrack);
            }
        }

        private void SearchTextBox_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                searchResultsView.Visible = false;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                SearchButton_Click(this, EventArgs.Empty);
                e.Handled = true;
                e.SuppressKeyPress = true;
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

            if(e?.Item?.Tag == null || e.Item.Tag is not Track)
            {
                e?.DrawDefault = true;
                return;
            }

            var track = (Track)e.Item.Tag;
            Rectangle bounds = e.Bounds;

            bool isSelected = e.Item.Selected;
            bool isHovered = (e.Item == _hoveredItem);

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
                    int radius = 4;
                    int d = radius * 2;
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

        private void SearchResultsView_MouseMove(object? sender, MouseEventArgs e)
        {
            ListViewHitTestInfo hitTest = searchResultsView.HitTest(e.Location);
            ListViewItem? currentItem = hitTest.Item;

            if (currentItem != null)
            {
                searchResultsView.Cursor = Cursors.Hand;
            }
            else
            {
                searchResultsView.Cursor = Cursors.Default;
            }

            if (currentItem != _hoveredItem)
            {
                ListViewItem? oldHovered = _hoveredItem;
                _hoveredItem = currentItem;

                if (oldHovered != null && oldHovered.Index < searchResultsView.Items.Count)
                {
                    searchResultsView.Invalidate(oldHovered.Bounds);
                }

                if (_hoveredItem != null)
                {
                    searchResultsView.Invalidate(_hoveredItem.Bounds);
                }
            }
        }

        private void SearchResultsView_MouseLeave(object? sender, EventArgs e)
        {
            searchResultsView.Cursor = Cursors.Default;

            if (_hoveredItem != null)
            {
                ListViewItem? oldHovered = _hoveredItem;
                _hoveredItem = null;

                if (oldHovered.Index < searchResultsView.Items.Count)
                {
                    searchResultsView.Invalidate(oldHovered.Bounds);
                }
            }
        }
    }
}
