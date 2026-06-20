using FontAwesome.Sharp;
using Microsoft.Extensions.Logging;
using Vibes.Design;
using Vibes.Interfaces;
using Vibes.Models;
using Vibes.Views;

namespace Vibes
{
    public partial class Vibes : Form
    {
        private readonly IAuth0Service _authService;
        private readonly IAvatarService _avatarService;
        private readonly ApplicationControl _appControl;
        private readonly AudioPlayerControl _audioPlayerControl;
        private readonly SearchBarControl _searchBarControl;
        private readonly AccountControl _accountControl;
        private readonly MediaDisplayControl _mediaDisplayControl;
        private ContextMenuStrip? _avatarMenu;
        private readonly ILogger<Vibes> _logger;

        private int _cornerRadius = 4;
        private bool _isSignedIn = false;
        private Control? _currentContent;

        public Vibes(
            ApplicationControl appControl,
            AudioPlayerControl audioPlayerControl,
            SearchBarControl searchBarControl,
            MediaDisplayControl mediaDisplayControl,
            AccountControl accountControl,
            IAuth0Service authService,
            ILogger<Vibes> logger,
            IAvatarService avatarService
         )
        {
            _appControl = appControl;
            _audioPlayerControl = audioPlayerControl;
            _searchBarControl = searchBarControl;
            _authService = authService;
            _mediaDisplayControl = mediaDisplayControl;
            _accountControl = accountControl;
            _avatarService = avatarService;
            _logger = logger;

            _searchBarControl.TrackSelected += NavigateToTrackPage;
            _appControl.PlaylistSelected += NavigateToTrackPage;
            _accountControl.Logout += UserLogout;

            InitializeComponent();

            if (avatarMenu != null)
            {
                avatarMenu.Renderer = new ContextMenuThemeRenderer();
                avatarMenu.ShowImageMargin = false;
            }

            _avatarMenu = avatarMenu;

            MouseEventHandler nativeDragBind = (s, e) => {
                if (e.Button == MouseButtons.Left) TriggerNativeDrag();
            };

            titleBar.MouseDown += nativeDragBind;
            titleBarLayout.MouseDown += nativeDragBind;
            logoIcon.MouseDown += nativeDragBind;
            headerCenterLayout.MouseDown += nativeDragBind;
        }

        private void HomeButton_Click(object? sender, EventArgs e)
        {
            // Pending return assignment logic context mapping rules
        }

        public void NavigateToTrackPage(object? sender, TrackSelectedEventArgs e)
        {
            _mediaDisplayControl.Dock = DockStyle.Fill;
            _mediaDisplayControl.Entity = e.Entity;
            _mediaDisplayControl.RenderContentContext(e.PageTitle, e.Metadata, e.Tracks);

            if (e.Autoplay) _mediaDisplayControl.AutoplayTracks();

            var currentContent = _appControl.applicationLayout.GetControlFromPosition(1, 0);
            if (currentContent != null)
            {
                _appControl.applicationLayout.Controls.Remove(currentContent);
            }

            _appControl.applicationLayout.Controls.Add(_mediaDisplayControl, 1, 0);
            _mediaDisplayControl.BringToFront();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (WindowState == FormWindowState.Maximized)
            {
                Region?.Dispose(); Region = null;
                mainGrid?.Region?.Dispose(); if (mainGrid != null) mainGrid.Region = null;
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
            if (!_isSignedIn) ShowLogin();
        }

        private void Login_SignedIn(object? sender, EventArgs e)
        {
            if (sender is LoginControl login) login.SignedIn -= Login_SignedIn;
            _isSignedIn = true;
            ShowAppContent();
        }

        private void InitializeHeader_Event(object? sender, EventArgs e)
        {
            if (sender is IAuth0Service auth)
            {
                homeButton.Visible = auth.CurrentUser != null;

                if (!string.IsNullOrEmpty(auth.CurrentUser?.Picture))
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

            var activeAudioPlayer = mainGrid.GetControlFromPosition(0, 2);
            if (activeAudioPlayer != null) mainGrid.Controls.Remove(activeAudioPlayer);

            var activeSearchBar = headerCenterLayout.GetControlFromPosition(0, 0);
            if (activeSearchBar != null) headerCenterLayout.Controls.Remove(activeSearchBar);

            if (!mainGrid.Controls.Contains(copyrightLabel)) mainGrid.Controls.Add(copyrightLabel, 0, 2);

            copyrightLabel.Text = $"Vibes © {DateTime.Now.Year} | All Rights Reserved.";
            copyrightLabel.Visible = true;
            copyrightLabel.Enabled = true;

            var login = _authService is null ? new LoginControl { Dock = DockStyle.Fill } : new LoginControl(_authService) { Dock = DockStyle.Fill };
            login.SignedIn += Login_SignedIn;
            mainGrid.Controls.Add(login, 0, 1);

            _currentContent = login;
            _authService!.UserChanged += InitializeHeader_Event;
        }

        private void ShowAppContent()
        {
            ClearCurrentContent();
            mainGrid.Controls.Remove(copyrightLabel);

            _appControl.Dock = DockStyle.Fill;
            _audioPlayerControl.Dock = DockStyle.Fill;
            _searchBarControl.Dock = DockStyle.Fill;
            _searchBarControl.Margin = new Padding(0);
            _searchBarControl.Anchor = AnchorStyles.Left;

            mainGrid.Controls.Add(_appControl, 0, 1);
            mainGrid.Controls.Add(_audioPlayerControl, 0, 2);
            headerCenterLayout.Controls.Add(_searchBarControl, 0, 0);

            _currentContent = _appControl;
        }

        private void ClearCurrentContent()
        {
            if (_currentContent != null)
            {
                mainGrid.Controls.Remove(_currentContent);
                _currentContent = null;
            }

            var appMain = _appControl.applicationLayout.GetControlFromPosition(1, 0);
            if (appMain != null) _appControl.applicationLayout.Controls.Remove(appMain);
        }

        private void UserAvatar_MouseUp(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || userAvatar?.Image == null) return;
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
            if (_currentContent is ApplicationControl appControl)
            {
                appControl.applicationLayout.Controls.Add(_accountControl, 1, 0);
            }
            else
            {
                _logger?.LogWarning("Cannot open account details: current content is not ApplicationControl");
            }
        }

        private async void UserLogout(object? sender, EventArgs e)
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

        private void MinimizeButton_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;

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

        private void ExitButton_Click(object sender, EventArgs e) => Close();
    }
}