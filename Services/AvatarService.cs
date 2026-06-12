using Auth0.OidcClient;
using Microsoft.Extensions.Logging;
using System.Drawing.Drawing2D;
using Vibes.Interfaces;

namespace Vibes.Services
{
    public class AvatarService : IAvatarService
    {
        private readonly ILogger<AvatarService> _logger;
        private static readonly HttpClient _httpClient = new HttpClient();

        public AvatarService(ILogger<AvatarService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task LoadAvatarIntoAsync(PictureBox pictureBox, string? url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                if (pictureBox.InvokeRequired)
                {
                    pictureBox.Invoke(() => { pictureBox.Image?.Dispose(); pictureBox.Image = null; });
                }
                else
                {
                    pictureBox.Image?.Dispose();
                    pictureBox.Image = null;
                }
                return;
            }

            try
            {
                using var resp = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                resp.EnsureSuccessStatusCode();
                using var stream = await resp.Content.ReadAsStreamAsync().ConfigureAwait(false);
                using var img = Image.FromStream(stream);
                var bmp = new Bitmap(img);

                if (pictureBox.InvokeRequired)
                {
                    pictureBox.Invoke(() =>
                    {
                        pictureBox.Image?.Dispose();
                        pictureBox.Image = bmp;
                        UpdateAvatarRegion(pictureBox);
                    });
                }
                else
                {
                    pictureBox.Image?.Dispose();
                    pictureBox.Image = bmp;
                    UpdateAvatarRegion(pictureBox);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogWarning("Failed to load avatar image from {Url}: {Message}", url, ex.Message);
                if (pictureBox.InvokeRequired)
                {
                    pictureBox.Invoke(() => { pictureBox.Image?.Dispose(); pictureBox.Image = null; });
                }
                else
                {
                    pictureBox.Image?.Dispose();
                    pictureBox.Image = null;
                }
            }
        }

        public void UpdateAvatarRegion(PictureBox pictureBox)
        {
            try
            {
                var r = pictureBox.ClientRectangle;
                int diameter = Math.Min(r.Width, r.Height);
                using var path = new GraphicsPath();
                path.AddEllipse((r.Width - diameter) / 2, (r.Height - diameter) / 2, diameter, diameter);

                pictureBox.Region?.Dispose();
                pictureBox.Region = new Region(path);
            }
            catch (Exception ex)
            {
                _logger?.LogWarning("Failed to update avatar region: {Message}", ex.Message);
            }
        }
    }
}
