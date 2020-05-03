using System;
using System.IO;

namespace JsonConfigForNetCoreConsoleApp.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = Config.Instanse();
            
            PrintConfig("Before:");

            config.Name = null;
            config.MaxTimeOut = 300;
            config.Age++;
            config.bool1 = !config.bool1;
            config.bool2 = false;
            config.Save(); // >> Обязательное сохранеие! <<

            PrintConfig("After:");
        }

        private static void PrintConfig(string txt)
        {
            Console.WriteLine(txt);
            Console.WriteLine(File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "appsettings.json")));
            Console.WriteLine();
        }
    }
}
