using System.Drawing.Drawing2D;
using Vibes.Interfaces;
using Vibes.Models;

namespace Vibes.Views
{
    public partial class MusicCardControl : UserControl
    {
        private readonly Track _track;
        private readonly IAvatarService? _avatarService;
        private readonly PictureBox _coverBox;
        private readonly Label _titleLabel;
        private readonly Label _subLabel;

        public MusicCardControl(Track track, IAvatarService? avatarService)
        {
            _track = track;
            _avatarService = avatarService;

            Size = new Size(160, 220);
            Margin = new Padding(0, 0, 16, 0);
            BackColor = Color.FromArgb(24, 24, 24);
            Cursor = Cursors.Hand;

            _coverBox = new PictureBox
            {
                Size = new Size(136, 136),
                Location = new Point(12, 12),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.FromArgb(18, 18, 18)
            };

            _titleLabel = new Label
            {
                Text = _track.Title,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Location = new Point(12, 156),
                Size = new Size(136, 20),
                AutoEllipsis = true
            };

            _subLabel = new Label
            {
                Text = _track.Artist,
                ForeColor = Color.FromArgb(160, 160, 160),
                Font = new Font("Segoe UI", 8.5F, FontStyle.Regular),
                Location = new Point(12, 180),
                Size = new Size(136, 35),
                AutoEllipsis = true
            };

            Controls.Add(_coverBox);
            Controls.Add(_titleLabel);
            Controls.Add(_subLabel);

            MouseEnter += (s, e) => BackColor = Color.FromArgb(40, 40, 40);
            MouseLeave += (s, e) => BackColor = Color.FromArgb(24, 24, 24);
            foreach (Control child in Controls)
            {
                child.Click += (s, e) => OnClick(e);
                child.MouseEnter += (s, e) => BackColor = Color.FromArgb(40, 40, 40);
            }

            if (_avatarService != null && !string.IsNullOrEmpty(_track.CoverUrl))
            {
                _ = _avatarService.LoadAvatarIntoAsync(_coverBox, _track.CoverUrl);
            }

            Paint += MusicCardControl_Paint;
        }

        private void MusicCardControl_Paint(object? sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (var path = new GraphicsPath())
            {
                int r = 6; 
                var rect = ClientRectangle;
                path.AddArc(rect.X, rect.Y, r * 2, r * 2, 180, 90);
                path.AddArc(rect.Right - (r * 2), rect.Y, r * 2, r * 2, 270, 90);
                path.AddArc(rect.Right - (r * 2), rect.Bottom - (r * 2), r * 2, r * 2, 0, 90);
                path.AddArc(rect.X, rect.Bottom - (r * 2), r * 2, r * 2, 90, 90);
                path.CloseFigure();
                Region = new Region(path);
            }
        }
    }
}
