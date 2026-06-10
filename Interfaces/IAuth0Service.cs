using Vibes.Models;

namespace Vibes.Interfaces
{
    public interface IAuth0Service
    {
        Task<AuthResult> LoginAsync();
    }
}
