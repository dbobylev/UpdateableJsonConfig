using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace UpdateableConfig.Example
{
    class Program
    {
        static readonly string configPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");

        static void Main(string[] args)
        {
            PrintConfig("Before:");

            var configRoot = new ConfigurationBuilder().AddJsonFile(configPath).Build();
            var config = Config.InitConfig(configRoot, configPath);

            config.Name = null;
            config.MaxTimeOut = 300;
            config.Age++;
            config.bool1 = !config.bool1;
            config.MyArray = new int[] { -6, 55 };
            config.Save(); // >> Обязательное сохранеие! <<

            PrintConfig("After:");
        }

        private static void PrintConfig(string txt)
        {
            Console.WriteLine(txt);
            Console.WriteLine(File.ReadAllText(configPath));
            Console.WriteLine();
        }
    }
}
