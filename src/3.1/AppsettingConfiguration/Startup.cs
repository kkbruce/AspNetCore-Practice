using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace AppsettingConfiguration
{
    public static class Startup
    {
        public static IConfigurationRoot Configuration(string[] args)
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
            //var consoleEnv = Environment.GetEnvironmentVariable("CONSOLE_ENVIRONMENT");
            //return new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json", false, true)
            //    .AddJsonFile($"appsettings.{consoleEnv}.json", optional: true, true)
            //    .AddEnvironmentVariables()
            //    .Build();

            // Add .AddInMemoryCollection()
            //var consoleEnv = Environment.GetEnvironmentVariable("CONSOLE_ENVIRONMENT");
            //return new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json", false, true)
            //    .AddJsonFile($"appsettings.{consoleEnv}.json", optional: true, true)
            //    .AddEnvironmentVariables()
            //    .AddInMemoryCollection(new Dictionary<string, string>
            //    {
            //        { "Blog:Name", "KingKong Bruce記事" },
            //        { "Blog:URL", "https://blog.kkbruce.net" }
            //    })
            //    .AddCommandLine(args)
            //    .Build();

            // Add .AddIniFile()
            var consoleEnv = Environment.GetEnvironmentVariable("CONSOLE_ENVIRONMENT");
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{consoleEnv}.json", optional: true, true)
                .AddEnvironmentVariables()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "Blog:Name", "KingKong Bruce記事" },
                    { "Blog:URL", "https://blog.kkbruce.net" }
                })
                .AddCommandLine(args)
                .AddIniFile("appsettings.ini")
                .Build();

        }
    }
}
