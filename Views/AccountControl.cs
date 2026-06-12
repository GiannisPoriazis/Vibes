using Vibes.Interfaces;

namespace Vibes.Views
{
    public partial class AccountControl : UserControl
    {
        private readonly IAuth0Service? _authService;
        private readonly IAvatarService? _avatarService;

        public AccountControl()
        {
            InitializeComponent();
        }

        public AccountControl(IAuth0Service? authService, IAvatarService? avatarService) : this()
        {
            _authService = authService;
            _avatarService = avatarService;

            if (_authService != null)
            {
                usernameInput.Text = _authService.CurrentUser?.Username ?? string.Empty;
                emailInput.Text = _authService.CurrentUser?.Email ?? string.Empty;

                if(_avatarService != null)
                {
                    _avatarService.LoadAvatarIntoAsync(avatarIcon, _authService.CurrentUser?.Picture);
                }
            }
        }
    }
}
