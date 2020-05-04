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

            // ��������� Json � ����
            File.WriteAllText(_TestFilePath, param.BaseJson);

            // ��������� ������
            var config = new MyConfig(_TestFileName);
            File.Delete(_TestFilePath);

            //�������� ����� ����� ������� ����� ������������ ��������
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

            // �������� ��������
            var value = typeof(BaseJsonConfig).GetMethod(methodName).MakeGenericMethod(typeof(T)).Invoke(config, new[] { "mykey" });

            Console.WriteLine($"���������� ��������: [{(value == null ? "null" : value.ToString())}]");
            Console.WriteLine($"��������� ��������: [{(value == null ? "null" : value.ToString())}]");

            Assert.AreEqual(param.ExceptedResult, value);
        }

        [TestCaseSource(typeof(BaseJsonConfigSource), "SetValueTestData")]
        public static void SetValueTest(SetValueTestDataParam param)
        {
            Console.WriteLine("##### TEST BEGIN #####");
            Console.WriteLine(param);
            Console.WriteLine("######################");

            // ��������� Json � ����
            File.WriteAllText(_TestFilePath, param.BaseJson);

            // ��������� ������
            var config = new MyConfig(_TestFileName);

            // ��������� ��������
            config.SetValue("mykey", param.Value);
            config.Save();

            string ResultJsonString = File.ReadAllText(_TestFilePath);
            File.Delete(_TestFilePath);

            JObject ResultJson = JObject.Parse(ResultJsonString);
            JObject ExceptedJson = JObject.Parse(param.ExceptedJson);

            Console.WriteLine($"���������� JSON:\r\n{ResultJson.ToString()}");
            Console.WriteLine($"��������� JSON:\r\n{ExceptedJson.ToString()}");

            Assert.IsTrue(JToken.DeepEquals(ExceptedJson, ResultJson));
        }
    }
}