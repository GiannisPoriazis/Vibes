using FontAwesome.Sharp;
using Microsoft.Extensions.Logging;
using System.Drawing.Drawing2D;
using Vibes.Design;
using Vibes.Services;
using Vibes.Views;

namespace Vibes
{
    public partial class Vibes : Form
    {
        private readonly Interfaces.IAuth0Service? _authService;
        private readonly Interfaces.IAvatarService? _avatarService;
        private ContextMenuStrip? _avatarMenu;
        private readonly ILogger<Auth0Service>? _logger;
        private static readonly HttpClient _httpClient = new HttpClient();

        public Vibes()
        {
            InitializeComponent();

            if (avatarMenu != null)
            {
                var roundedRenderer = new RoundedToolStripRenderer(_cornerRadius);
                avatarMenu.Renderer = roundedRenderer;
                avatarMenu.RenderMode = ToolStripRenderMode.Professional;
                ToolStripManager.Renderer = roundedRenderer;
                avatarMenu.ShowImageMargin = false;
                avatarMenu.Opened += AvatarMenu_Opened;
            }
            
            _avatarMenu = avatarMenu;
            userAvatar.MouseUp += UserAvatar_MouseUp;
        }

        public Vibes(Interfaces.IAuth0Service? authService, ILogger<Auth0Service>? logger, Interfaces.IAvatarService? avatarService) : this()
        {
            _authService = authService;
            _avatarService = avatarService;
            _logger = logger;
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

        private int _cornerRadius = 4;
        private bool _isSignedIn = false;
        private Control? _currentContent;

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
            if (WindowState == FormWindowState.Maximized)
            {
                if (Region != null)
                {
                    Region.Dispose();
                    Region = null;
                }

                if (mainGrid?.Region != null)
                {
                    mainGrid.Region.Dispose();
                    mainGrid.Region = null;
                }

                pageContainer.Padding = new Padding(0);
                Invalidate();
                return;
            }

            SetRoundedRegion(_cornerRadius);
            UpdateMainGridRegion();
            _avatarService?.UpdateAvatarRegion(userAvatar);
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

        private void Login_SignedIn(object? sender, EventArgs e)
        {
            if (sender is LoginControl login)
                login.SignedIn -= Login_SignedIn;

            _isSignedIn = true;
            ShowAppContent();
        }

        private void InitializeHeader_Event(object? sender, EventArgs e)
        {
            if (sender is Interfaces.IAuth0Service auth)
            {
                if (!String.IsNullOrEmpty(auth.CurrentUser?.Picture))
                {
                    _ = _avatarService?.LoadAvatarIntoAsync(userAvatar, auth.CurrentUser.Picture);
                    userAvatar.Visible = true;
                }
                else
                {
                    userAvatar.Image = null;
                    userAvatar.Visible = false;
                }
            }
        }

        private void ShowLogin()
        {
            ClearCurrentContent();
            var login = _authService is null ? new LoginControl { Dock = DockStyle.Fill } : new LoginControl(_authService) { Dock = DockStyle.Fill };
            login.SignedIn += Login_SignedIn;
            mainGrid.Controls.Add(login, 0, 1);
            _currentContent = login;
            _authService?.UserChanged += InitializeHeader_Event;
        }

        private void ShowAppContent()
        {
            ClearCurrentContent();
            var app = _authService is null ? new ApplicationControl { Dock = DockStyle.Fill } : new ApplicationControl(_authService) { Dock = DockStyle.Fill };
            var audioPlayer = new AudioPlayerControl { Dock = DockStyle.Fill };
            copyrightLabel.Enabled = false;
            mainGrid.Controls.Add(app, 0, 1);
            mainGrid.Controls.Add(audioPlayer, 0, 2);
            _currentContent = app;
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

            if (WindowState == FormWindowState.Maximized)
            {
                if (mainGrid?.Region != null)
                {
                    mainGrid.Region.Dispose();
                    mainGrid.Region = null;
                }
                return;
            }

            UpdateMainGridRegion();

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
                Point p = PointToScreen(e.Location);
                this.Location = new Point(p.X - _startPoint.X, p.Y - _startPoint.Y);
            }
        }

        private void Logo_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }

        private void UserAvatar_MouseUp(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            if (userAvatar == null || userAvatar.Image == null) return;

            var menu = avatarMenu ?? _avatarMenu ?? userAvatar.ContextMenuStrip;
            if (menu == null) return;

            int marginY = 5;

            try
            {
                var anchor = new Point(userAvatar.Width, userAvatar.Height + marginY);
                menu.Show(userAvatar, anchor, ToolStripDropDownDirection.Left);
            }
            catch
            {
                var pref = menu.GetPreferredSize(Size.Empty);
                if (pref.Width <= 0) pref = menu.Size;
                int offsetX = userAvatar.Width - pref.Width;
                var desired = userAvatar.PointToScreen(new Point(offsetX, userAvatar.Height + marginY));
                var wa = Screen.GetWorkingArea(this);

                if (desired.X < wa.Left) desired.X = wa.Left;
                if (desired.X + pref.Width > wa.Right) desired.X = Math.Max(wa.Left, wa.Right - pref.Width);
                if (desired.Y + pref.Height > wa.Bottom) desired.Y = Math.Max(wa.Top, wa.Bottom - pref.Height);

                try { menu.Show(desired); }
                catch { menu.Show(userAvatar, new Point(0, userAvatar.Height + marginY)); }
            }
        }

        private void AvatarMenu_Account_Click(object? sender, EventArgs e)
        {
            var account = new AccountControl(_authService, _avatarService) { Dock = DockStyle.Fill };
            if (_currentContent is ApplicationControl appControl)
            {
                appControl.applicationLayout.Controls.Add(account, 1, 0);
            }
            else
            {
                _logger?.LogWarning("Cannot open account details: current content is not ApplicationControl");
            }
        }

        private void AvatarMenu_Opened(object? sender, EventArgs e)
        {
            if (sender is ContextMenuStrip cms)
            {
                var rect = new Rectangle(Point.Empty, cms.Size);
                using var path = MakeRoundedPath(rect, 4);
                cms.Region?.Dispose();
                cms.Region = new Region(path);
            }
            else if (sender is ToolStripDropDownMenu dmenu)
            {
                var rect = new Rectangle(Point.Empty, dmenu.Size);
                using var path = MakeRoundedPath(rect, 4);
                dmenu.Region?.Dispose();
                dmenu.Region = new Region(path);
            }
        }

        private static GraphicsPath MakeRoundedPath(Rectangle rect, int radius)
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

        private async void AvatarMenu_Logout_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_authService != null)
                {
                    var logoutResult = await _authService.LogoutAsync();

                    if (!logoutResult.IsError)
                    {
                        _isSignedIn = false;
                        ShowLogin();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError("Logout failed: {Message}", ex.Message);
            }
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
