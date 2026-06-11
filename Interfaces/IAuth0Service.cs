using Vibes.Models;

namespace Vibes.Interfaces
{
    public interface IAuth0Service
    {
        Task<AuthResult> LoginAsync();
        UserInfo? CurrentUser { get; }
        event EventHandler<UserChangedEventArgs>? UserChanged;
        Task<AuthResult> LogoutAsync();
    }
}
