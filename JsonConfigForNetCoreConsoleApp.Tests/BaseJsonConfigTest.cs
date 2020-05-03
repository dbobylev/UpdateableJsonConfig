using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;

namespace JsonConfigForNetCoreConsoleApp.Tests
{
    public class BaseJsonConfigTest
    {
        private static string _TestFileName = "appsettings.json";
        private static string _TestFilePath = Path.Combine(AppContext.BaseDirectory, _TestFileName);

        [TestCase("{\"mykey\": \"myvalue\"}", typeof(string), "myvalue")]
        [TestCase("{\"mykey\": null}", typeof(string), "")]
        [TestCase("{\"mykey\": \"\"}", typeof(string), "")]
        [TestCase("{\"mykey\": 10 }", typeof(int), 10)]
        [TestCase("{\"mykey\": 10 }", typeof(int), 10, true)]
        [TestCase("{\"mykey\": null }", typeof(int), 0)]
        [TestCase("{\"mykey\": null }", typeof(int), null, true)]
        [TestCase("{\"mykey\": true }", typeof(bool), true)]
        [TestCase("{\"mykey\": true }", typeof(bool), true, true)]
        [TestCase("{\"mykey\": null }", typeof(bool), false)]
        [TestCase("{\"mykey\": null }", typeof(bool), null, true)]
        public static void ReadValueTest(string json, Type T, object exceptedValue, bool IsNullable = false)
        {
            File.WriteAllText(_TestFilePath, json);

            BaseJsonConfig.Reload();
            var config = BaseJsonConfig.Instance();

            MethodInfo method;
            if (IsNullable)
                method = typeof(BaseJsonConfig).GetMethod(nameof(config.GetNullableValue));
            else
                method = typeof(BaseJsonConfig).GetMethod(nameof(config.GetValue));
           
            MethodInfo generic = method.MakeGenericMethod(T);
            var val = generic.Invoke(config, new[] { "mykey" });


            File.Delete(_TestFilePath);

            Assert.AreEqual(exceptedValue, val);
        }

        [TestCase("{\"mykey\": \"myvalue\"}", "{\r\n  \"mykey\": \"newvalue\"\r\n}", "newvalue")]
        [TestCase("{\"mykey\": \"myvalue\"}", "{\r\n  \"mykey\": \"\"\r\n}", "")]
        [TestCase("{\"mykey\": \"myvalue\"}", "{\r\n  \"mykey\": null\r\n}", null)]
        [TestCase("{\"mykey\": \"\"}", "{\r\n  \"mykey\": \"newvalue\"\r\n}", "newvalue")]
        [TestCase("{\"mykey\": \"\"}", "{\r\n  \"mykey\": \"\"\r\n}", "")]
        [TestCase("{\"mykey\": \"\"}", "{\r\n  \"mykey\": null\r\n}", null)]
        [TestCase("{\"mykey\": null}", "{\r\n  \"mykey\": \"newvalue\"\r\n}", "newvalue")]
        [TestCase("{\"mykey\": null}", "{\r\n  \"mykey\": \"\"\r\n}", "")]
        [TestCase("{\"mykey\": null}", "{\r\n  \"mykey\": null\r\n}", null)]
        [TestCase("{\"mykey\": 10}", "{\r\n  \"mykey\": 20\r\n}", 20)]
        [TestCase("{\"mykey\": null}", "{\r\n  \"mykey\": 20\r\n}", 20)]
        [TestCase("{\"mykey\": 10}", "{\r\n  \"mykey\": null\r\n}", null)]
        [TestCase("{\"mykey\": null}", "{\r\n  \"mykey\": null\r\n}", null)]
        [TestCase("{\"mykey\": true}", "{\r\n  \"mykey\": false\r\n}", false)]
        [TestCase("{\"mykey\": true}", "{\r\n  \"mykey\": null\r\n}", null)]
        [TestCase("{\"mykey\": null}", "{\r\n  \"mykey\": false\r\n}", false)]
        [TestCase("{\"mykey\": null}", "{\r\n  \"mykey\": null\r\n}", null)]
        public static void SetValueTest(string BaseJson, string ExceptedJson, object value)
        {
            File.WriteAllText(_TestFilePath, BaseJson);
            
            BaseJsonConfig.Reload();
            var config = BaseJsonConfig.Instance();
            
            config.SetValue("mykey", value);
            config.Save();
            
            string ResultJson = File.ReadAllText(_TestFilePath);
           
            File.Delete(_TestFilePath);
            
            Assert.AreEqual(ExceptedJson, ResultJson);
        }
    }
}