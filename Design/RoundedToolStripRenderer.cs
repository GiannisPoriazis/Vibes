using System.Drawing.Drawing2D;

namespace Vibes.Design
{
    public class RoundedToolStripRenderer : ToolStripProfessionalRenderer
    {
        private readonly int _radius;

        public RoundedToolStripRenderer(int radius) : base()
        {
            _radius = Math.Max(0, radius);
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            if (e.ToolStrip is ToolStripDropDown || e.ToolStrip is ToolStripDropDownMenu || e.ToolStrip is ContextMenuStrip)
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = Rectangle.Truncate(e.AffectedBounds);
                if (rect.Width <= 0 || rect.Height <= 0)
                {
                    rect = new Rectangle(Point.Empty, e.ToolStrip.Size);
                }

                using var path = RoundedRect(rect, _radius);
                using var brush = new SolidBrush(e.ToolStrip.BackColor);
                g.FillPath(brush, path);
                using var pen = new Pen(Color.FromArgb(60, 60, 60));
                g.DrawPath(pen, path);
            }
            else
            {
                base.OnRenderToolStripBackground(e);
            }
        }

        private static GraphicsPath RoundedRect(Rectangle rect, int radius)
        {
            var path = new GraphicsPath();
            int d = radius * 2;
            if (d <= 0)
            {
                path.AddRectangle(rect);
                return path;
            }
            rect.Inflate(-1, -1);
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
