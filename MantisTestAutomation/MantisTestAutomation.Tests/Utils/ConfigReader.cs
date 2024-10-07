using Microsoft.Extensions.Configuration;

namespace MantisTestAutomation.Utils
{
    public static class ConfigReader
    {
        private static IConfigurationRoot configuration;

        static ConfigReader()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("Resources/appsettings.json");
            configuration = builder.Build();
        }

        public static string BaseUrl => configuration["baseUrl"];
        public static string Browser => configuration["browser"];
        public static string Username => configuration["username"];
        public static string Password => configuration["password"];
    }
}
