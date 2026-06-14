using Microsoft.Extensions.DependencyInjection;
using Auth0.OidcClient;
using Vibes.Interfaces;
using Vibes.Services;
using Serilog;
using Microsoft.Extensions.Logging;
using Vibes.Database;
using Vibes.Views;

namespace Vibes
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            try
            {
                AppConfig.Load();
            }
            catch { }

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
            var services = new ServiceCollection();

            services.AddSingleton<ILoggerFactory>(sp => new Serilog.Extensions.Logging.SerilogLoggerFactory(Log.Logger, dispose: true));
            services.AddLogging();

            var domain = AppConfig.Get("Auth0:Domain");
            var clientId = AppConfig.Get("Auth0:ClientId");

            if (!string.IsNullOrEmpty(domain) && !string.IsNullOrEmpty(clientId))
            {
                var options = new Auth0ClientOptions
                {
                    Domain = domain,
                    ClientId = clientId
                };
                services.AddSingleton(options);
                services.AddSingleton<IAuth0Service, Auth0Service>();
            }

            services.AddSingleton<IAudioStreamingService, AudioStreamingService>();
            services.AddSingleton<IAvatarService, AvatarService>();
            services.AddSingleton<IPlaybackQueueManagerService, PlaybackQueueManagerService>();

            services.AddTransient<ApplicationControl>();
            services.AddTransient<AudioPlayerControl>();
            services.AddTransient<SearchBarControl>();
            services.AddTransient<Vibes>();

            var serviceProvider = services.BuildServiceProvider(validateScopes: true);
            var mainForm = serviceProvider.GetRequiredService<Vibes>();

            using (var db = new VibesDbContext())
            {
                db.Database.EnsureCreated();
            }

            Application.ApplicationExit += (s, e) => {
                try { Log.CloseAndFlush(); } catch { }
            };

            Application.Run(mainForm);
        }
    }
}