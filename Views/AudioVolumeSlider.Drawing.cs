using System.Drawing.Drawing2D;

namespace Vibes.Views
{
    public partial class AudioVolumeSlider
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int barHeight = 4; 
            int barY = (Height - barHeight) / 2;
            int usableWidth = Width - (PaddingX * 2);

            using (var bgBrush = new SolidBrush(Color.FromArgb(40, 40, 40)))
            {
                g.FillRectangle(bgBrush, PaddingX, barY, usableWidth, barHeight);
            }

            int fillWidth = (int)(((float)Value / _max) * usableWidth);
            if (fillWidth > 0)
            {
                Color fillColor = _isHovered ? Color.FromArgb(30, 215, 96) : Color.White;
                using (var fillBrush = new SolidBrush(fillColor))
                {
                    g.FillRectangle(fillBrush, PaddingX, barY, fillWidth, barHeight);
                }
            }

            if (_isHovered || _isMouseDown)
            {
                int thumbSize = 12; 
                int thumbX = (PaddingX + fillWidth) - (thumbSize / 2);
                int thumbY = (Height - thumbSize) / 2;

                using (var thumbBrush = new SolidBrush(Color.White))
                {
                    g.FillEllipse(thumbBrush, thumbX, thumbY, thumbSize, thumbSize);
                }
            }
        }
    }
}