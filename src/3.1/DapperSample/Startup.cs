using System.IO;
using Microsoft.Extensions.Configuration;

namespace DapperSample
{
    public static class Startup
    {
        public static IConfigurationRoot Configuration(string[] args)
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .Build();
        }
    }
}
