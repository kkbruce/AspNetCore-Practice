using System.IO;
using Microsoft.Extensions.Configuration;

namespace AppsettingConfiguration
{
    public static class Startup
    {
        public static IConfigurationRoot Configuration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .Build();
        }
    }
}
