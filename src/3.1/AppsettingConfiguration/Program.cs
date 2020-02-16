﻿using System;
using Microsoft.Extensions.Configuration;

namespace AppsettingConfiguration
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = Startup.Configuration(args);
            GetKeyValue(config);
            GetConnectionString(config);
            GetHierarchicalData(config);
            BindToAClass(config);
            GetEnvModeConfig(config);
            GetOSEnvironment(config);
            GetMemoryConfig(config);
            GetCommandLineConfig(config);

            Console.Read();
        }

        private static void GetCommandLineConfig(IConfigurationRoot config)
        {
            var argMode = config.GetSection("Mode").Value;
            Print(nameof(GetCommandLineConfig), $"CommandLine Mode: {argMode}");
        }

        private static void GetMemoryConfig(IConfigurationRoot config)
        {
            var blogName = config.GetSection("Blog:Name").Value;
            var blogURL = config.GetSection("Blog:URL").Value;
            Print(nameof(GetMemoryConfig), $"Blog: {blogName}, URL: {blogURL}");
        }

        private static void GetOSEnvironment(IConfigurationRoot config)
        {
            var os = config.GetSection("OS").Value;
            var hostname = config.GetSection("COMPUTERNAME").Value;
            Print(nameof(GetOSEnvironment), $"OS: {os}, HostName: {hostname}");
        }

        private static void GetEnvModeConfig(IConfigurationRoot config)
        {
            // From ConsoleEnv
            var server = config.GetSection("Server").Value;
            var user = config.GetSection("User").Value;
            Print(nameof(GetEnvModeConfig), $"Server(EnvMode):{server}, User: {user}");
        }

        private static void BindToAClass(IConfigurationRoot config)
        {
            var app = config.GetSection("AppInfo").Get<AppInfo>();
            Print(nameof(BindToAClass), $"AppName: {app.AppName}, Maintainer: {app.Maintainer}");
        }

        private static void GetHierarchicalData(IConfigurationRoot config)
        {
            var appName = config.GetSection("AppInfo.AppName").Value;
            var maintainer = config.GetSection("AppInfo:Maintainer").Value;
            Print(nameof(GetHierarchicalData), $"AppName: {appName}, Maintainer: {maintainer}");
        }

        private static void GetConnectionString(IConfigurationRoot config)
        {
            var conn = config.GetConnectionString("DefaultConnection");
            Print(nameof(GetConnectionString), $"ConnectionString: {conn}");
        }

        private static void GetKeyValue(IConfigurationRoot config)
        {
            var server = config.GetSection("Server").Value;
            var user = config.GetSection("User").Value;
            var pwd = config.GetSection("Pwd").Value;
            Print(nameof(GetKeyValue), $"Server: {server}, User: {user}, Pwd: {pwd}");
        }

        private static void Print(string title, string result)
        {
            Console.WriteLine($"{title} result --> {result}");
            Console.WriteLine("------------------------------------------------------------");
        }
    }
}