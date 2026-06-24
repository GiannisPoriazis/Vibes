using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Vibes.Extensions;

namespace Vibes
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            try
            {
                AppConfig.Load();
            }
            catch { }

            var serviceProvider = new ServiceCollection()
                .AddApplicationServices()
                .BuildServiceProvider(validateScopes: true);

            var mainForm = serviceProvider.GetRequiredService<Vibes>();

            Application.ApplicationExit += (s, e) => {
                try { Log.CloseAndFlush(); } catch { }
            };

            Application.Run(mainForm);
        }
    }
}