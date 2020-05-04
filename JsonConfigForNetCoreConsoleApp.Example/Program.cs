using System;
using System.IO;

namespace JsonConfigForNetCoreConsoleApp.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintConfig("Before:");

            var config = Config.Instanse();

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
            Console.WriteLine(File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "appsettings.json")));
            Console.WriteLine();
        }
    }
}
