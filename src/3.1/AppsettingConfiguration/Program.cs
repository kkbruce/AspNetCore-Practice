using System;
using System.ComponentModel.DataAnnotations;
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
            GetValueSample(config);
            GetSectionSample(config);
            GetChildrenSample(config);
            ExistsSample(config);
            GetIniSample(config);
            GetXMLSample(config);

            Console.Read();
        }

        private static void GetXMLSample(IConfigurationRoot config)
        {
            var key = config.GetSection("Auth:Key").Value;
            var token = config.GetSection("Auth:Token").Value;
            Print(nameof(GetXMLSample), $"Key: {key}, Token: {token}");
        }

        private static void GetIniSample(IConfigurationRoot config)
        {
            var twmvc = config.GetSection("mvcgroup");
            Print(nameof(GetIniSample), $"Title: {twmvc["title"]}, Url: {twmvc["url"]}");
            var skilltree = config.GetSection("skilltreegroup");
            Print(nameof(GetIniSample), $"Title: {skilltree["skilltree:title"]}, Url: {skilltree["skilltree:url"]}");
            var study4 = config.GetSection("study4:website");
            Print(nameof(GetIniSample), $"Title: {study4["title"]}, Url: {study4["url"]}");
        }

        private static void ExistsSample(IConfigurationRoot config)
        {
            var sectionExists = config.GetSection("section10:key10").Exists();
            Print(nameof(ExistsSample), $"Section Exists: {sectionExists}");
        }

        private static void GetChildrenSample(IConfigurationRoot config)
        {
            var sectionList = config.GetSection("section1").GetChildren();
            foreach (var section in sectionList)
            {
                Print(nameof(GetChildrenSample), $"Key: {section.Key}");
                Print(nameof(GetChildrenSample), $"Value: {section.Value}");
                Print(nameof(GetChildrenSample), $"Path: {section.Path}");

                Print(nameof(GetChildrenSample), $"Key0: {section["key0"]}");
                Print(nameof(GetChildrenSample), $"Key1: {section["key1"]}");
            }
        }

        private static void GetSectionSample(IConfigurationRoot config)
        {
            var appInfo = config.GetSection("AppInfo");
            var Name = appInfo["Name"];
            var Maintainer = appInfo["Maintainer"];
            Print(nameof(GetSectionSample), $"AppName: {Name}, Maintainer: {Maintainer}");
        }

        private static void GetValueSample(IConfigurationRoot config)
        {
            var argMode = config.GetValue<string>("Mode");
            Print(nameof(GetValueSample), $"CommandLine Mode: {argMode}");
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
            Console.WriteLine($"Call {title} --> {result}");
            Console.WriteLine("------------------------------------------------------------");
        }
    }
}
