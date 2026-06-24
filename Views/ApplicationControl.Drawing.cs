using System.Drawing.Drawing2D;
using Vibes.Extensions;
using Vibes.Models;

namespace Vibes.Views
{
    public partial class ApplicationControl
    {
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