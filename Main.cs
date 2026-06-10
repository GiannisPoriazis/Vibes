using FontAwesome.Sharp;
using System.Drawing.Drawing2D;
using Vibes.Design;
using Vibes.Views;

namespace Vibes
{
    public partial class Vibes : Form
    {
        public Vibes()
        {
            InitializeComponent();
        }

        private void UpdateMainGridRegion()
        {
            if (mainGrid == null) return;
            var r = mainGrid.ClientRectangle;
            if (r.Width <= 0 || r.Height <= 0) return;

            int d = _cornerRadius * 2;
            using var path = new System.Drawing.Drawing2D.GraphicsPath();
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

        // corner radius used by region and border drawing
        private int _cornerRadius = 4;
        // whether user is signed in
        private bool _isSignedIn = false;
        // currently hosted content control in the mainGrid center cell
        private Control? _currentContent;

        // Apply rounded corners using a Region built from a GraphicsPath
        private void SetRoundedRegion(int radius)
        {
            if (ClientSize.Width <= 0 || ClientSize.Height <= 0)
                return;

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

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            // If maximized, remove rounded clipping and border so the window fills screen corners
            if (WindowState == FormWindowState.Maximized)
            {
                // remove form region (restore square corners)
                if (Region != null)
                {
                    Region.Dispose();
                    Region = null;
                }

                // remove child clipping so content fills
                if (mainGrid?.Region != null)
                {
                    mainGrid.Region.Dispose();
                    mainGrid.Region = null;
                }

                // remove padding so painted border (if any) is not visible
                pageContainer.Padding = new Padding(0);
                Invalidate();
                return;
            }

            // normal (restored) window: re-apply rounded region and child clipping
            SetRoundedRegion(_cornerRadius);
            UpdateMainGridRegion();
            // leave a 1px gap for the custom border
            pageContainer.Padding = new Padding(1);
            Invalidate();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            SetRoundedRegion(_cornerRadius);
            if (!_isSignedIn)
                ShowLogin();
        }

        private void ShowLogin()
        {
            ClearCurrentContent();
            var login = new LoginControl { Dock = DockStyle.Fill };
            login.SignedIn += Login_SignedIn;
            // add to mainGrid center cell (column 1, row 1)
            mainGrid.Controls.Add(login, 1, 1);
            _currentContent = login;
        }

        private void Login_SignedIn(object? sender, LoginControl.UserInfoEventArgs e)
        {
            if (sender is LoginControl login)
                login.SignedIn -= Login_SignedIn;

            _isSignedIn = true;
            ShowAppContent();
        }

        private void ShowAppContent()
        {
            ClearCurrentContent();
            var appPanel = new Panel { Dock = DockStyle.Fill, BackColor = Color.FromArgb(20, 20, 20) };
            var lbl = new Label { Text = "App Content", ForeColor = Color.White, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter };
            appPanel.Controls.Add(lbl);
            mainGrid.Controls.Add(appPanel, 1, 1);
            _currentContent = appPanel;
        }

        private void ClearCurrentContent()
        {
            if (_currentContent != null)
            {
                mainGrid.Controls.Remove(_currentContent);
                _currentContent.Dispose();
                _currentContent = null;
            }
        }

        private void pageContainer_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is not Control ctl) return;
            var rect = ctl.ClientRectangle;
            if (rect.Width <= 0 || rect.Height <= 0) return;

            // When maximized we don't draw rounded borders
            if (WindowState == FormWindowState.Maximized)
            {
                // Ensure no special clipping on children
                if (mainGrid?.Region != null)
                {
                    mainGrid.Region.Dispose();
                    mainGrid.Region = null;
                }
                return;
            }

            // update child clipping so corners don't cover the rounded border
            UpdateMainGridRegion();

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            int w = rect.Width - 1;
            int h = rect.Height - 1;
            int d = _cornerRadius * 2;

            using var path = new System.Drawing.Drawing2D.GraphicsPath();
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

        // --- Dragging Logic Variables ---
        private bool _dragging = false;
        private Point _startPoint = new Point(0, 0);

        private void Logo_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _dragging = true;
                _startPoint = new Point(e.X, e.Y);
            }
        }

        private void Logo_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                // Calculate the movement relative to the screen screen coordinates
                Point p = PointToScreen(e.Location);
                this.Location = new Point(p.X - _startPoint.X, p.Y - _startPoint.Y);
            }
        }

        private void Logo_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void EnlargeButton_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
                enlargeButton.IconChar = IconChar.Square;
            }
            else
            {
                WindowState = FormWindowState.Normal;
                enlargeButton.IconChar = IconChar.Expand;
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
