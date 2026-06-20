using FontAwesome.Sharp;
using System.Drawing.Drawing2D;
using Vibes.Design;

namespace Vibes
{
    public partial class Vibes
    {
        private void HomeButton_Paint(object sender, PaintEventArgs e)
        {
            var btn = (IconButton)sender;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (var path = new GraphicsPath())
            {
                path.AddEllipse(0, 0, btn.Width - 1, btn.Height - 1);
                btn.Region?.Dispose();
                btn.Region = new Region(path);
            }
        }

        private void UpdateMainGridRegion()
        {
            if (mainGrid == null) return;
            var r = mainGrid.ClientRectangle;
            if (r.Width <= 0 || r.Height <= 0) return;

            int d = _cornerRadius * 2;
            using var path = new GraphicsPath();
            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddLine(r.X + _cornerRadius, r.Y, r.Right - _cornerRadius, r.Y);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddLine(r.Right, r.Y + _cornerRadius, r.Right, r.Bottom - _cornerRadius);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddLine(r.Right - _cornerRadius, r.Bottom, r.X + _cornerRadius, r.Bottom);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();

            mainGrid.Region?.Dispose();
            mainGrid.Region = new Region(path);
        }

        private void SetRoundedRegion(int radius)
        {
            if (ClientSize.Width <= 0 || ClientSize.Height <= 0) return;

            var path = new GraphicsPath();
            int w = ClientSize.Width;
            int h = ClientSize.Height;
            int d = radius * 2;

            path.AddArc(0, 0, d, d, 180, 90);
            path.AddLine(radius, 0, w - radius, 0);
            path.AddArc(w - d, 0, d, d, 270, 90);
            path.AddLine(w, radius, w, h - radius);
            path.AddArc(w - d, h - d, d, d, 0, 90);
            path.AddLine(w - radius, h, radius, h);
            path.AddArc(0, h - d, d, d, 90, 90);
            path.CloseFigure();

            Region?.Dispose();
            Region = new Region(path);
        }

        private void pageContainer_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is not Control ctl) return;
            var rect = ctl.ClientRectangle;
            if (rect.Width <= 0 || rect.Height <= 0) return;

            if (WindowState == FormWindowState.Maximized) return;

            UpdateMainGridRegion();
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            int w = rect.Width - 1; int h = rect.Height - 1; int d = _cornerRadius * 2;

            using var path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddLine(rect.X + _cornerRadius, rect.Y, rect.X + w - _cornerRadius, rect.Y);
            path.AddArc(rect.X + w - d, rect.Y, d, d, 270, 90);
            path.AddLine(rect.X + w, rect.Y + _cornerRadius, rect.X + w, rect.Y + h - _cornerRadius);
            path.AddArc(rect.X + w - d, rect.Y + h - d, d, d, 0, 90);
            path.AddLine(rect.X + w - _cornerRadius, rect.Y + h, rect.X + _cornerRadius, rect.Y + h);
            path.AddArc(rect.X, rect.Y + h - d, d, d, 90, 90);
            path.CloseFigure();

            using var pen = new Pen(ColorPalette.ApplicationBorder, 1);
            e.Graphics.DrawPath(pen, path);
        }

        private void headerCenterLayout_Paint(object sender, PaintEventArgs e) { }
    }
}