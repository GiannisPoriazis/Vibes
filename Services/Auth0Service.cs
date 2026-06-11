using Auth0.OidcClient;
using System.Security.Claims;
using Vibes.Interfaces;
using Vibes.Models;
using Duende.IdentityModel.OidcClient.Browser;
using Microsoft.Extensions.Logging;

namespace Vibes.Services
{
    public class Auth0Service: IAuth0Service
    {
        private readonly Auth0Client client;
        private readonly ILogger<Auth0Service> _logger;
        private readonly object _userLock = new();
        private UserInfo? _currentUser;

        public UserInfo? CurrentUser
        {
            get
            {
                lock (_userLock)
                {
                    return _currentUser;
                }
            }
            private set
            {
                lock (_userLock)
                {
                    _currentUser = value;
                }
            }
        }

        public event EventHandler<UserChangedEventArgs>? UserChanged;

        public Auth0Service(Auth0ClientOptions options, ILogger<Auth0Service> logger)
        {
            if (options == null || string.IsNullOrEmpty(options.Domain) || string.IsNullOrEmpty(options.ClientId))
            {
                throw new ArgumentException("Auth0ClientOptions must contain Domain and ClientId", nameof(options));
            }

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            client = new Auth0Client(options);
        }

        public async Task<AuthResult> LoginAsync()
        {
            var result = await client.LoginAsync();
            if (result == null)
                return new AuthResult { IsError = true, Error = null };

            if (!result.IsError && result.User != null)
            {
                var mapped = MapFromClaims(result.User);
                CurrentUser = mapped;
                _logger.LogInformation("User logged in: {Username} ({Subject})", mapped.Username, mapped.Subject);

                try
                {
                    UserChanged?.Invoke(this, new UserChangedEventArgs(mapped));
                }
                catch (Exception ex)
                {
                    _logger.LogWarning("UserChanged event handler threw an exception: {Message}", ex.Message);
                }
            }

            return new AuthResult
            {
                IsError = result.IsError,
                Error = result.Error,
            };
        }

        public async Task<AuthResult> LogoutAsync()
        {
            try
            {
                var result = await client.LogoutAsync();

                if (result == BrowserResultType.Success)
                {
                    CurrentUser = null;

                    try
                    {
                        UserChanged?.Invoke(this, new UserChangedEventArgs(null));
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning("UserChanged event handler threw an exception: {Message}", ex.Message);
                    }

                    return new AuthResult
                    {
                        IsError = false,
                        Error = null,
                    };
                }
                else
                {
                    return new AuthResult
                    {
                        IsError = true,
                        Error = null,
                    };
                }
            }
            catch(Exception ex)
            {
                _logger.LogWarning("Logout threw an exception: {Message}", ex.Message);

                return new AuthResult
                {
                    IsError = true,
                    Error = null,
                };
            }
         }

        private static UserInfo MapFromClaims(ClaimsPrincipal? user)
        {
            if (user == null) return new UserInfo(null, null, null, null);

            string? subject = user.FindFirst("sub")?.Value ?? user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            string? username = user.FindFirst("nickname")?.Value
                               ?? user.FindFirst("name")?.Value
                               ?? user.Identity?.Name;
            string? email = user.FindFirst(ClaimTypes.Email)?.Value ?? user.FindFirst("email")?.Value;
            string? picture = user.FindFirst("picture")?.Value;

            return new UserInfo(subject, username, email, picture);
        }
    }
}
