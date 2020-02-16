using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace AppsettingConfiguration
{
    public static class Startup
    {
        public static IConfigurationRoot Configuration()
        {
            // Version1
            //return new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json", false)
            //    .Build();

            var consoleEnv = Environment.GetEnvironmentVariable("CONSOLE_ENVIRONMENT");
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{consoleEnv}.json", optional: true, true)
                .Build();
        }
    }
}
