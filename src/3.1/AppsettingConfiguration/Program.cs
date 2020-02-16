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
            Console.Read();
        }

        static void GetKeyValue(IConfigurationRoot config)
        {
            var server = config.GetSection("Server").Value;
            var user = config.GetSection("User").Value;
            var pwd = config.GetSection("Pwd").Value;
            Print($"Server: {server}, User: {user}, Pwd: {pwd}");
        }

        static void Print(string configString)
        {
            Console.WriteLine(configString);
            Console.WriteLine("--------------------");
        }
    }
}
