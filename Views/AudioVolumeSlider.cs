using System.ComponentModel;

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
            int usableWidth = Width - (PaddingX * 2);
            int adjustedX = Math.Clamp(mouseX - PaddingX, 0, usableWidth);

            float percentage = (float)adjustedX / usableWidth;
            Value = (int)(percentage * _max);
            Scroll?.Invoke(this, EventArgs.Empty);
        }
    }
}