using Microsoft.VisualStudio.TestPlatform.Utilities;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using JsonConfigForNetCoreConsoleApp.Tests.TestSource;

namespace JsonConfigForNetCoreConsoleApp.Tests
{
    class MyConfig : BaseJsonConfig
    {
        public MyConfig(string FileName = null) : base(FileName)
        {

        }
    }

    public class BaseJsonConfigTest
    {
        private static string _TestFileName = "Test.json";
        private static string _TestFilePath = Path.Combine(AppContext.BaseDirectory, _TestFileName);

        [TestCaseSource(typeof(BaseJsonConfigSource), "ReadValueTestData")]
        public static void ReadValueTest<T>(ReadValueTestDataParam<T> param)
        {
            Console.WriteLine("##### TEST BEGIN #####");
            Console.WriteLine(param);
            Console.WriteLine("######################");

            // Сохраняем Json в файл
            File.WriteAllText(_TestFilePath, param.BaseJson);

            // Загружаем конфиг
            var config = new MyConfig(_TestFileName);
            File.Delete(_TestFilePath);

            //Выбираем метод через который будем запроашивать занчение
            string methodName = string.Empty;
            switch (param.MethodType)
            {
                case eMethodType.General:
                    methodName = nameof(config.GetValue);
                    break;
                case eMethodType.Nullable:
                    methodName = nameof(config.GetNullableValue);
                    break;
                case eMethodType.Enumerable:
                    methodName = nameof(config.GetEnumerableValue);
                    break;
                default:
                    break;
            }

            // Получаем значение
            var value = typeof(BaseJsonConfig).GetMethod(methodName).MakeGenericMethod(typeof(T)).Invoke(config, new[] { "mykey" });

            Console.WriteLine($"Полученное значение: [{(value == null ? "null" : value.ToString())}]");
            Console.WriteLine($"Ожидаемое значение: [{(value == null ? "null" : value.ToString())}]");

            Assert.AreEqual(param.ExceptedResult, value);
        }

        [TestCaseSource(typeof(BaseJsonConfigSource), "SetValueTestData")]
        public static void SetValueTest(SetValueTestDataParam param)
        {
            Console.WriteLine("##### TEST BEGIN #####");
            Console.WriteLine(param);
            Console.WriteLine("######################");

            // Сохраняем Json в файл
            File.WriteAllText(_TestFilePath, param.BaseJson);

            // Загружаем конфиг
            var config = new MyConfig(_TestFileName);

            // Обновляем значение
            config.SetValue("mykey", param.Value);
            config.Save();

            string ResultJsonString = File.ReadAllText(_TestFilePath);
            File.Delete(_TestFilePath);

            JObject ResultJson = JObject.Parse(ResultJsonString);
            JObject ExceptedJson = JObject.Parse(param.ExceptedJson);

            Console.WriteLine($"Полученный JSON:\r\n{ResultJson.ToString()}");
            Console.WriteLine($"Ожидаемый JSON:\r\n{ExceptedJson.ToString()}");

            Assert.IsTrue(JToken.DeepEquals(ExceptedJson, ResultJson));
        }
    }
}