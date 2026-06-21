namespace Vibes.Views
{
    public partial class MusicCardSkeleton : UserControl
    {
        private System.Windows.Forms.Timer _pulseTimer;
        private int _currentAlpha = 25;
        private bool _increasing = true;

        public MusicCardSkeleton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);

            Size = new Size(160, 220);
            Margin = new Padding(0, 0, 16, 0);
            BackColor = Color.FromArgb(18, 18, 18); 

            _pulseTimer = new System.Windows.Forms.Timer { Interval = 16 };
            _pulseTimer.Tick += PulseTimer_Tick;
            _pulseTimer.Start();
        }

        private void PulseTimer_Tick(object? sender, EventArgs e)
        {
            if (_increasing)
            {
                _currentAlpha += 2;
                if (_currentAlpha >= 45) _increasing = false;
            }
            else
            {
                _currentAlpha -= 2;
                if (_currentAlpha <= 20) _increasing = true;
            }

            Invalidate(); 
        }
    }
}