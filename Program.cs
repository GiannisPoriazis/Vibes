using Microsoft.Extensions.DependencyInjection;
using Auth0.OidcClient;
using Vibes.Interfaces;
using Vibes.Services;

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
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            try
            {
                AppConfig.Load();
            }
            catch
            {
                // ignore load errors; defaults remain null
            }

            // Setup DI
            var services = new ServiceCollection();

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
                services.AddSingleton<Auth0Service>();
                services.AddSingleton<IAuth0Service>(sp => sp.GetRequiredService<Auth0Service>());
            }

            services.AddSingleton<Vibes>();

            var serviceProvider = services.BuildServiceProvider();

            Vibes mainForm;

            try
            {
                mainForm = serviceProvider.GetService<Vibes>() ?? new Vibes();
            }
            catch
            {
                mainForm = new Vibes();
            }

            Application.Run(mainForm);
        }
    }
}