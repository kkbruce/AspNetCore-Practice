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
            Demo1(config);
        }

        public static void Demo1(IConfigurationRoot config)
        {
            var server = config.GetSection("Server").Value;
            var user = config.GetSection("User").Value;
            var pwd = config.GetSection("Pwd").Value;
            Console.WriteLine($"Server: {server}, User: {user}, Pwd: {pwd}");
        }
    }
}
