using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace Vibes.Views
{
    public partial class AudioVolumeSlider : Control
    {
        private int _value = 70;
        private int _max = 100;
        private bool _isHovered = false;
        private bool _isMouseDown = false;
        private const int PaddingX = 6;

        public event EventHandler? Scroll;

        [Category("Behavior")]
        [Description("The current volume level value (0-100).")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int Value
        {
            get => _value;
            set
            {
                _value = Math.Clamp(value, 0, _max);
                Invalidate();
            }
        }

        public AudioVolumeSlider()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);

            BackColor = Color.Transparent;
            // ✨ Αυξάνουμε λίγο το ύψος (από 24 σε 32) για να έχει αέρα το control
            Size = new Size(120, 32);
            Cursor = Cursors.Hand;
        }

        protected override void OnMouseEnter(EventArgs e) { _isHovered = true; Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { _isHovered = false; Invalidate(); base.OnMouseLeave(e); }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isMouseDown = true;
                UpdateValueFromMouse(e.X);
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _isMouseDown = false;
            base.OnMouseUp(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_isMouseDown)
            {
                UpdateValueFromMouse(e.X);
            }
            base.OnMouseMove(e);
        }

        private void UpdateValueFromMouse(int mouseX)
        {
            // Υπολογισμός λαμβάνοντας υπόψη το εσωτερικό PaddingX
            int usableWidth = Width - (PaddingX * 2);
            int adjustedX = Math.Clamp(mouseX - PaddingX, 0, usableWidth);

            float percentage = (float)adjustedX / usableWidth;
            Value = (int)(percentage * _max);
            Scroll?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int barHeight = 4; // Λεπτή premium γραμμή Spotify
            int barY = (Height - barHeight) / 2;
            int usableWidth = Width - (PaddingX * 2);

            // 1. Background Bar (Σκούρο γκρι Spotify)
            using (var bgBrush = new SolidBrush(Color.FromArgb(40, 40, 40)))
            {
                g.FillRectangle(bgBrush, PaddingX, barY, usableWidth, barHeight);
            }

            // 2. Fill Bar (Λευκό ή Πράσινο στο Hover)
            int fillWidth = (int)(((float)Value / _max) * usableWidth);
            if (fillWidth > 0)
            {
                Color fillColor = _isHovered ? Color.FromArgb(30, 215, 96) : Color.White;
                using (var fillBrush = new SolidBrush(fillColor))
                {
                    g.FillRectangle(fillBrush, PaddingX, barY, fillWidth, barHeight);
                }
            }

            // 3. Slider Thumb (Εμφανίζεται ΜΟΝΟ στο Hover ή Drag)
            if (_isHovered || _isMouseDown)
            {
                int thumbSize = 12; // Spotify standard slider dot size
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