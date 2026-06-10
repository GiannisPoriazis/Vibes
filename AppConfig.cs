using Microsoft.Extensions.Configuration;

namespace Vibes
{
    internal static class AppConfig
    {
        public static IConfigurationRoot? Configuration { get; private set; }

        public static void Load()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public static string? Get(string key)
        {
            return Configuration?[key];
        }
    }
}
