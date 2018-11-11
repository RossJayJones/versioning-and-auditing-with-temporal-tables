using System.IO;
using Microsoft.Extensions.Configuration;

namespace Host
{
    public static class ConfigurationHelper
    {
        public static IConfiguration CreateConfiguration()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables();
            return configurationBuilder.Build();
        }
    }
}
