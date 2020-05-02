using System;
using System.IO;

namespace JsonConfigForNetCoreConsoleApp.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = Config.Instanse();
            
            PrintConfig(config);

            //config.Name = "New name"; //Ошибка компиляции, не устанволен модификатор доступа set
            config.MaxTimeOut = null;
            config.MaxPing = 999;
            config.foo = "NEW BARRR";
            config.Age = config.Age + 1;
            config.Save(); // >> Обязательное сохранеие! <<

            PrintConfig(config);

            Console.WriteLine();
            Console.WriteLine("Json был обновлён:");
            Console.WriteLine(File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "appsettings.json")));
        }

        private static void PrintConfig(Config config)
        {
            Console.WriteLine("-----------------------------");
            Console.WriteLine($"config.Url: {config.Url}");
            Console.WriteLine($"config.Name: {config.Name}");
            Console.WriteLine($"config.Age: {config.Age}");
            Console.WriteLine($"config.MaxPing: {config.MaxPing}");
            Console.WriteLine($"config.MaxTimeOut: {config.MaxTimeOut}");
            Console.WriteLine($"config.foo: {config.foo}");
        }
    }
}
