using Auth0.OidcClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Vibes.Database;
using Vibes.Interfaces;
using Vibes.Services;
using Vibes.Views;

namespace Vibes.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var logPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Vibes");
            Directory.CreateDirectory(logPath);

            var serilogConfig = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .WriteTo.File(new Serilog.Formatting.Compact.RenderedCompactJsonFormatter(), Path.Combine(logPath, "vibes-.json"), rollingInterval: RollingInterval.Day, retainedFileCountLimit: 14, shared: true)
                .WriteTo.Trace();

            try
            {
                if (AppConfig.Configuration != null)
                {
                    serilogConfig = serilogConfig.ReadFrom.Configuration(AppConfig.Configuration);
                }
            }
            catch { }

            Log.Logger = serilogConfig.CreateLogger();

            services.AddSingleton<ILoggerFactory>(sp => new Serilog.Extensions.Logging.SerilogLoggerFactory(Log.Logger, dispose: true));
            services.AddLogging();

            var domain = AppConfig.Get("Auth0:Domain");
            var clientId = AppConfig.Get("Auth0:ClientId");

            if (!string.IsNullOrEmpty(domain) && !string.IsNullOrEmpty(clientId))
            {
                string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string webViewCachePath = Path.Combine(appDataPath, "Vibes", "WebView2_Cache");
                Directory.CreateDirectory(webViewCachePath);

                var customBrowser = new AuthBrowser(webViewCachePath);

                var options = new Auth0ClientOptions
                {
                    Domain = domain,
                    ClientId = clientId,
                    Browser = customBrowser 
                };

                services.AddSingleton(options);
                services.AddSingleton<IAuth0Service, Auth0Service>();
            }

            services.AddDbContextFactory<VibesDbContext>();

            services.AddSingleton<IPlaylistService, PlaylistService>();
            services.AddSingleton<IAudioStreamingService, AudioStreamingService>();
            services.AddSingleton<IAvatarService, AvatarService>();
            services.AddSingleton<IPlaybackQueueManagerService, PlaybackQueueManagerService>();

            services.AddTransient<ApplicationControl>();
            services.AddTransient<AudioPlayerControl>();
            services.AddTransient<SearchBarControl>();
            services.AddTransient<MediaDisplayControl>();
            services.AddTransient<AccountControl>();
            services.AddTransient<HomeControl>();
            services.AddTransient<Vibes>();

            return services;
        }
    }
}