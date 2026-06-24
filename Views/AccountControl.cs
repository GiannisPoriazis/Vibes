using Vibes.Interfaces;

namespace Vibes.Views
{
    public partial class AccountControl : UserControl
    {
        private readonly IAuth0Service? _authService;
        private readonly IAvatarService? _avatarService;

        public event EventHandler? Logout;
        public event EventHandler? BackBtnPressed;

        public AccountControl()
        {
            InitializeComponent();
        }

        public AccountControl(IAuth0Service? authService, IAvatarService? avatarService) : this()
        {
            _authService = authService;
            _avatarService = avatarService;
            _authService?.UserChanged += SetupAccountForm;
        }

        private void SetupAccountForm(object? sender, EventArgs e)
        {
            if (_authService != null && _authService.CurrentUser != null)
            {
                userIdInput.Text = _authService.CurrentUser.Subject ?? string.Empty;
                usernameInput.Text = _authService.CurrentUser.Username ?? string.Empty;
                emailInput.Text = _authService.CurrentUser.Email ?? string.Empty;

                if (_avatarService != null && !string.IsNullOrEmpty(_authService.CurrentUser.Picture))
                {
                    _ = _avatarService.LoadAvatarIntoAsync(avatarIcon, _authService.CurrentUser.Picture);
                }
            }
        }

        private void BtnBack_Click(object? sender, EventArgs e)
        {
            BackBtnPressed?.Invoke(this, new EventArgs());
        }

        private async void BtnLogout_Click(object? sender, EventArgs e)
        {
            if (_authService == null) return;

            var confirmResult = MessageBox.Show(
                "Are you sure you want to log out?",
                "Confirm Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmResult == DialogResult.Yes)
            {
                Logout?.Invoke(this, new EventArgs());
            }
        }
    }
}