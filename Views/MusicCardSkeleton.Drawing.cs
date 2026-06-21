using System.Drawing.Drawing2D;

namespace Vibes.Views
{
    public partial class MusicCardSkeleton
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (var path = new GraphicsPath())
            {
                int r = 6;
                var rect = this.ClientRectangle;
                path.AddArc(rect.X, rect.Y, r * 2, r * 2, 180, 90);
                path.AddArc(rect.Right - (r * 2), rect.Y, r * 2, r * 2, 270, 90);
                path.AddArc(rect.Right - (r * 2), rect.Bottom - (r * 2), r * 2, r * 2, 0, 90);
                path.AddArc(rect.X, rect.Bottom - (r * 2), r * 2, r * 2, 90, 90);
                path.CloseFigure();
                this.Region = new Region(path);
            }

            using (var brush = new SolidBrush(Color.FromArgb(_currentAlpha, 255, 255, 255)))
            {
                g.FillRectangle(brush, new Rectangle(12, 12, 136, 136));

                g.FillRectangle(brush, new Rectangle(12, 160, 110, 14));

                g.FillRectangle(brush, new Rectangle(12, 182, 70, 11));
            }
        }
    }
}