using System;
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
            BindToAClass(config);

                 Console.Read();
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
