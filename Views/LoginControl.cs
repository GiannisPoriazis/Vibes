using Vibes.Design;
using Vibes.Interfaces;

namespace Vibes.Views
{
    public partial class LoginControl : UserControl
    {
        public event EventHandler<UserInfoEventArgs>? SignedIn;
        private bool _btnHover = false;
        private readonly IAuth0Service? _authService;

        public LoginControl()
        {
            InitializeComponent();
        }

        public LoginControl(IAuth0Service? authService) : this()
        {
            _authService = authService;
        }

        private void BtnLogin_Paint(object? sender, PaintEventArgs e)
        {
            var btn = sender as Button;
            if (btn == null) return;

            var rect = btn.ClientRectangle;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Clear control background with parent's background to avoid white corners
            var parentBack = btn.Parent?.BackColor ?? this.BackColor;
            using (var bgBrush = new SolidBrush(parentBack))
            {
                e.Graphics.FillRectangle(bgBrush, rect);
            }

            // Draw gradient pill slightly inset so edges are clean
            int inset = 2;
            var pillRect = Rectangle.Inflate(rect, -inset, -inset);
            using var path = new System.Drawing.Drawing2D.GraphicsPath();
            int radius = Math.Max(2, pillRect.Height / 2);
            path.AddArc(pillRect.X, pillRect.Y, radius, radius, 180, 90);
            path.AddArc(pillRect.Right - radius, pillRect.Y, radius, radius, 270, 90);
            path.AddArc(pillRect.Right - radius, pillRect.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(pillRect.X, pillRect.Bottom - radius, radius, radius, 90, 90);
            path.CloseFigure();

            // Gradient colors
            Color left = ColorPalette.AccentPurple;
            Color right = ColorPalette.AccentPink;

            if (_btnHover)
            {
                left = ControlPaint.Dark(left, 0.05f);
                right = ControlPaint.Dark(right, 0.05f);
            }

            using var brush = new System.Drawing.Drawing2D.LinearGradientBrush(pillRect, left, right, 0f);
            e.Graphics.FillPath(brush, path);

            // Draw subtle 1px stroke around the pill to increase contrast
            using (var outlinePen = new Pen(Color.FromArgb(140, 0, 0, 0), 1f))
            {
                outlinePen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
                e.Graphics.DrawPath(outlinePen, path);
            }

            // Draw text centered
            var textRect = pillRect;
            TextRenderer.DrawText(e.Graphics, btn.Text, btn.Font, textRect, btn.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        private void BtnLogin_MouseEnter(object? sender, EventArgs e)
        {
            _btnHover = true;
            if (sender is Control c)
            {
                c.Invalidate();
                c.Cursor = Cursors.Hand;
            }
        }

        private void BtnLogin_MouseLeave(object? sender, EventArgs e)
        {
            _btnHover = false;
            if (sender is Control c)
            {
                c.Invalidate();
                c.Cursor = Cursors.Default;
            }
        }

        private async void BtnLogin_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_authService is null)
                {
                    return;
                }

                var loginResult = await _authService.LoginAsync();

                if (loginResult?.IsError == true)
                {
                    return;
                }

                var userName = loginResult?.User?.FindFirst("name")?.Value
                               ?? loginResult?.User?.Identity?.Name
                               ?? string.Empty;

                SignedIn?.Invoke(this, new UserInfoEventArgs { Username = userName });
            }
            catch
            {
                return;
            }

        }

        public class UserInfoEventArgs : EventArgs
        {
            public string Username { get; set; } = string.Empty;
        }

        private void LoginControl_Load(object sender, EventArgs e)
        {

        }
    }
}
