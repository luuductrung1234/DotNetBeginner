using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace MyFirstEfCoreApp.ConsoleUI
{
    class Program
    {
        private static IConfigurationRoot configuration;

        static void Main(string[] args)
        {
            LoadConfiguration();
        }

        private static void LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            configuration = builder.Build();
        }
    }
}
