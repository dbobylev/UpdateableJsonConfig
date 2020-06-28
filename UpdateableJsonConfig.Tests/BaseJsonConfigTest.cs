using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.IO;
using UpdateableConfig.Tests.TestSource;
using Microsoft.Extensions.Configuration;

namespace UpdateableConfig.Tests
{
    class Config : UpdateableJsonConfig
    {
        private static Config _Instanse;
        public static Config Instanse()
        {
            if (_Instanse == null)
                throw new ArgumentNullException("Configuration not loaded");
            return _Instanse;
        }
        public static Config InitConfig(IConfigurationRoot config, string filepath)
        {
            _Instanse = new Config(config, filepath);
            return _Instanse;
        }
        private Config(IConfigurationRoot config, string filepath) : base(config, filepath)
        {
        }
    }

    public class BaseJsonConfigTest
    {
        private static string _TestFileName = "Test.json";
        private static string _TestFilePath = Path.Combine(AppContext.BaseDirectory, _TestFileName);

        private static Config LoadConfig()
        {
            return Config.InitConfig(new ConfigurationBuilder().AddJsonFile(_TestFilePath).Build(), _TestFilePath);
        }

        [TestCaseSource(typeof(BaseJsonConfigSource), "ReadValueTestData")]
        public static void ReadValueTest<T>(ReadValueTestDataParam<T> param)
        {
            Console.WriteLine("##### TEST BEGIN #####");
            Console.WriteLine(param);
            Console.WriteLine("######################");

            // Сохраняем Json в файл
            File.WriteAllText(_TestFilePath, param.BaseJson);

            // Загружаем конфиг
            var config = LoadConfig();
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
            var value = typeof(UpdateableJsonConfig).GetMethod(methodName).MakeGenericMethod(typeof(T)).Invoke(config, new[] { "mykey" });

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
            var config = LoadConfig();

            // Обновляем значение
            config.SetValue(param.Value, "mykey");
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