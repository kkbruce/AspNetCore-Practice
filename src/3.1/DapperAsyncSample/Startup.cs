using Microsoft.Extensions.Configuration;
using System.IO;

namespace DapperAsyncSample
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
