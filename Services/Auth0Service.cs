using Auth0.OidcClient;
using Vibes.Interfaces;
using Vibes.Models;

namespace Vibes.Services
{
    public class Auth0Service: IAuth0Service
    {
        private readonly Auth0Client _client;

        public Auth0Service(Auth0ClientOptions options)
        {
            if (options == null || string.IsNullOrEmpty(options.Domain) || string.IsNullOrEmpty(options.ClientId))
                throw new ArgumentException("Auth0ClientOptions must contain Domain and ClientId", nameof(options));

            _client = new Auth0Client(options);
        }

        public async Task<AuthResult> LoginAsync()
        {
            var result = await _client.LoginAsync();
            if (result == null)
                return new AuthResult { IsError = true, Error = null };

            return new AuthResult
            {
                IsError = result.IsError,
                Error = result.Error,
                User = result.User,
                UserName = result.User?.Identity?.Name ?? result.User?.FindFirst("name")?.Value
            };
        }
    }
}
