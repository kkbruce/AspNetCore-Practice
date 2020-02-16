using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace AppsettingConfiguration
{
    public static class Startup
    {
        public static IConfigurationRoot Configuration()
        {
            // Default
            //return new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json", false)
            //    .Build();

            // Add CONSOLE_ENVIRONMENT
            //var consoleEnv = Environment.GetEnvironmentVariable("CONSOLE_ENVIRONMENT");
            //return new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json", false, true)
            //    .AddJsonFile($"appsettings.{consoleEnv}.json", optional: true, true)
            //    .Build();

            // Add .AddEnvironmentVariables()
            var consoleEnv = Environment.GetEnvironmentVariable("CONSOLE_ENVIRONMENT");
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{consoleEnv}.json", optional: true, true)
                .AddEnvironmentVariables()
                .Build();

        }
    }
}
