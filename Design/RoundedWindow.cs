using System.Drawing.Drawing2D;

namespace Vibes.Design
{
    internal static class RoundedWindow
    {
        public static void roundedWindow_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is not Control ctl) return;
            var rect = ctl.ClientRectangle;
            if (rect.Width <= 0 || rect.Height <= 0) return;

            int _cornerRadius = 4;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            int w = rect.Width - 1;
            int h = rect.Height - 1;
            int d = _cornerRadius * 2;

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
    }
}
