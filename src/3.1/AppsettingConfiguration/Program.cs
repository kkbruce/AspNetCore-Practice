using System;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace AppsettingConfiguration
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = Startup.Configuration();
            GetKeyValue(config);
            GetConnectionString(config);
            GetHierarchicalData(config);

            Console.Read();
        }

        private static void GetHierarchicalData(IConfigurationRoot config)
        {
            var appName = config.GetSection("AppInfo.AppName").Value;
            var maintainer = config.GetSection("AppInfo:Maintainer").Value;
            Print($"AppName: {appName}, Maintainer: {maintainer}");
        }

        private static void GetConnectionString(IConfigurationRoot config)
        {
            var conn = config.GetConnectionString("DefaultConnection");
            Print($"ConnectionString: {conn}");
        }

        private static void GetKeyValue(IConfigurationRoot config)
        {
            var server = config.GetSection("Server").Value;
            var user = config.GetSection("User").Value;
            var pwd = config.GetSection("Pwd").Value;
            Print($"Server: {server}, User: {user}, Pwd: {pwd}");
        }

        private static void Print(string configString)
        {
            Console.WriteLine(configString);
            Console.WriteLine("------------------------------------------------------------");
        }
    }
}
