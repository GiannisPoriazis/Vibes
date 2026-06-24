using Vibes.Database;
using Vibes.Design;
using Vibes.Extensions;
using Vibes.Models;

namespace Vibes.Views
{
    public partial class MediaDisplayControl
    {
        private void TrackListView_DrawColumnHeader(object? sender, DrawListViewColumnHeaderEventArgs e)
        {
            using (var headerBrush = new SolidBrush(Color.FromArgb(25, 25, 25)))
            {
                e.Graphics.FillRectangle(headerBrush, e.Bounds);
            }

            TextFormatFlags flags = TextFormatFlags.VerticalCenter | TextFormatFlags.Left;
            if (e.ColumnIndex == 0) flags = TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter;

            TextRenderer.DrawText(e.Graphics, e.Header!.Text, e.Font, e.Bounds, Color.Gray, flags);
        }

        private void TrackListView_DrawSubItem(object? sender, DrawListViewSubItemEventArgs e)
        {
            if (e.Item == null) return;

            var g = e.Graphics;
            var track = (Track)e.Item.Tag!;
            bool isSelected = e.Item.Selected;
            bool isHovered = trackListView.IsRowHovered(e.ItemIndex);

            Color rowColor = (isSelected || isHovered) ? Color.FromArgb(255, 45, 45, 45) : Color.FromArgb(255, 15, 15, 15);
            using (var rowBrush = new SolidBrush(rowColor))
            {
                g.FillRectangle(rowBrush, e.Bounds);
            }

            if (e.ColumnIndex == 0)
            {
                TextRenderer.DrawText(g, e.SubItem!.Text, e.Item.Font, e.Bounds, Color.Gray,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
                return;
            }

            if (e.ColumnIndex == 5)
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

            if (e.ColumnIndex == 1)
            {
                bool isCurrentTrack = (_queueService.CurrentTrack == track);
                Color titleColor = isCurrentTrack ? Color.FromArgb(30, 215, 96) : Color.White;
                Color artistColor = Color.FromArgb(170, 170, 170);

                Font titleFont = new Font("Segoe UI", 10, FontStyle.Regular);
                Font artistFont = new Font("Segoe UI", 9, FontStyle.Regular);

                Size titleSize = TextRenderer.MeasureText(track.Title, titleFont);
                int startY = e.Bounds.Top + (e.Bounds.Height - titleSize.Height - 14) / 2;

                Point titlePoint = new Point(e.Bounds.Left + 4, startY);
                TextRenderer.DrawText(g, track.Title, titleFont, titlePoint, titleColor, TextFormatFlags.NoPadding);

                Point artistPoint = new Point(e.Bounds.Left + 4, titlePoint.Y + titleSize.Height + 2);
                TextRenderer.DrawText(g, track.Artist, artistFont, artistPoint, artistColor, TextFormatFlags.NoPadding);
                return;
            }

            Color textColor = Color.White;
            if (e.ColumnIndex == 2) textColor = Color.Gray;

            TextRenderer.DrawText(g, e.SubItem!.Text, e.Item.Font, e.Bounds, textColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
        }

        private Color ExtractDominantColor(Bitmap bmp)
        {
            using (Bitmap lowRes = new Bitmap(bmp, new Size(16, 16)))
            {
                long r = 0, g = 0, b = 0;
                int totalPixels = lowRes.Width * lowRes.Height;

                for (int x = 0; x < lowRes.Width; x++)
                {
                    for (int y = 0; y < lowRes.Height; y++)
                    {
                        Color c = lowRes.GetPixel(x, y);
                        r += c.R; g += c.G; b += c.B;
                    }
                }

                int finalR = (int)((r / totalPixels) * 0.4);
                int finalG = (int)((g / totalPixels) * 0.4);
                int finalB = (int)((b / totalPixels) * 0.4);

                return Color.FromArgb(255, Math.Max(15, finalR), Math.Max(15, finalG), Math.Max(15, finalB));
            }
        }

        private async void ShowTrackContextMenu(Track track, Point displayLocation, bool isPlaylistView)
        {
            var menu = new ContextMenuStrip
            {
                BackColor = Color.FromArgb(35, 35, 35),
                ForeColor = Color.White,
                ShowImageMargin = false,
                Renderer = new ContextMenuThemeRenderer()
            };

            var itemPlay = new ToolStripMenuItem("Play");
            itemPlay.Click += (s, e) => _queueService.PlaySearchTrackNow(track);
            menu.Items.Add(itemPlay);

            var itemQueue = new ToolStripMenuItem("Add to queue");
            itemQueue.Click += (s, e) => _queueService.AppendToFutureQueue(track);
            menu.Items.Add(itemQueue);

            IEnumerable<Playlist> availablePlaylists;
            using (var db = new VibesDbContext())
            {
                availablePlaylists = await _playlistService.GetPlaylistsWithoutTrackAsync(track.Id);
            }

            if (availablePlaylists.Any())
            {
                var itemAddToPlaylist = new ToolStripMenuItem("Add to playlist");
                ((ToolStripDropDownMenu)itemAddToPlaylist.DropDown).ShowImageMargin = false; 

                foreach (var playlist in availablePlaylists)
                {
                    var subItem = new ToolStripMenuItem(playlist.Name);
                    subItem.Click += async (s, e) => {
                        await _playlistService.AddTrackToPlaylistAsync(playlist.Id, track);
                    };
                    itemAddToPlaylist.DropDownItems.Add(subItem);
                }

                menu.Items.Add(itemAddToPlaylist);
            }

            if (isPlaylistView)
            {
                menu.Items.Add(new ToolStripSeparator());
                var itemRemove = new ToolStripMenuItem("Remove from this playlist");
                itemRemove.ForeColor = Color.FromArgb(255, 100, 100);
                itemRemove.Click += async (s, e) => {
                    await _playlistService.RemoveTrackFromPlaylistAsync(Entity.Id, track.Id);
                };
                menu.Items.Add(itemRemove);
            }

            menu.Show(trackListView, displayLocation);
        }
    }
}