using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsonConfigForNetCoreConsoleApp.Tests.TestSource
{
    class BaseJsonConfigSource
    {
        private static List<ITestDataParam> ReadValueTestDataList = new List<ITestDataParam>();
        private static List<ITestDataParam> SetValueTestDataList = new List<ITestDataParam>();

        public static IEnumerable<TestCaseData> ReadValueTestData { get => ReadValueTestDataList.Select(x => new TestCaseData(x)); }
        public static IEnumerable<TestCaseData> SetValueTestData { get => SetValueTestDataList.Select(x => new TestCaseData(x)); }

		static BaseJsonConfigSource()
		{
			InitReadValueTestData();
			InitSetValueTestData();
		}

		private static void InitReadValueTestData() 
        {
            ReadValueTestDataList.Add(new ReadValueTestDataParam<string>(
                baseJson: "{\"mykey\": \"myvalue\"}",
                exceptedResult: "myvalue"));

            ReadValueTestDataList.Add(new ReadValueTestDataParam<string>(
                baseJson: "{\"mykey\": null}",
                exceptedResult: ""));

            ReadValueTestDataList.Add(new ReadValueTestDataParam<string>(
                baseJson: "{\"mykey\": \"\"}",
                exceptedResult: ""));

            ReadValueTestDataList.Add(new ReadValueTestDataParam<int>(
                baseJson: "{\"mykey\": 10 }",
                exceptedResult: 10));

            ReadValueTestDataList.Add(new ReadValueTestDataParam<int>(
                baseJson: "{\"mykey\": 10 }",
                exceptedResult: 10,
                methodType: eMethodType.Nullable));

            ReadValueTestDataList.Add(new ReadValueTestDataParam<int>(
                baseJson: "{\"mykey\": null }",
                exceptedResult: 0));

            ReadValueTestDataList.Add(new ReadValueTestDataParam<int>(
                baseJson: "{\"mykey\": null }",
                exceptedResult: null,
                methodType: eMethodType.Nullable));

            ReadValueTestDataList.Add(new ReadValueTestDataParam<bool>(
                baseJson: "{\"mykey\": true }",
                exceptedResult: true));

            ReadValueTestDataList.Add(new ReadValueTestDataParam<bool>(
                baseJson: "{\"mykey\": true }",
                exceptedResult: true,
                methodType: eMethodType.Nullable));

            ReadValueTestDataList.Add(new ReadValueTestDataParam<bool>(
                baseJson: "{\"mykey\": null }",
                exceptedResult: false));

            ReadValueTestDataList.Add(new ReadValueTestDataParam<bool>(
                baseJson: "{\"mykey\": null }",
                exceptedResult: null,
                methodType: eMethodType.Nullable));

            ReadValueTestDataList.Add(new ReadValueTestDataParam<int>(
                baseJson: "{\"mykey\": [1, 2, 3] }",
                exceptedResult: new int[] { 1, 2, 3 },
                methodType: eMethodType.Enumerable));

            ReadValueTestDataList.Add(new ReadValueTestDataParam<int>(
                baseJson: "{\"mykey\": [1] }",
                exceptedResult: new int[] { 1},
                methodType: eMethodType.Enumerable));

            ReadValueTestDataList.Add(new ReadValueTestDataParam<int>(
                baseJson: "{\"mykey\": [] }",
                exceptedResult: new int[] { },
                methodType: eMethodType.Enumerable));

            ReadValueTestDataList.Add(new ReadValueTestDataParam<int>(
                baseJson: "{\"mykey\": null }",
                exceptedResult: new int[] { },
                methodType: eMethodType.Enumerable));

            ReadValueTestDataList.Add(new ReadValueTestDataParam<string>(
                baseJson: "{\"mykey\": [\"one\", \"two\", \"three\"] }",
                exceptedResult: new string[] { "one", "two", "three" },
                methodType: eMethodType.Enumerable));

            ReadValueTestDataList.Add(new ReadValueTestDataParam<string>(
                baseJson: "{\"mykey\":  [\"one\"] }",
                exceptedResult: new string[] { "one" },
                methodType: eMethodType.Enumerable));

            ReadValueTestDataList.Add(new ReadValueTestDataParam<string>(
                baseJson: "{\"mykey\": [] }",
                exceptedResult: new string[] { },
                methodType: eMethodType.Enumerable));

            ReadValueTestDataList.Add(new ReadValueTestDataParam<string>(
                baseJson: "{\"mykey\": null }",
                exceptedResult: new string[] { },
                methodType: eMethodType.Enumerable));

			ReadValueTestDataList.Add(new ReadValueTestDataParam<float>(
				baseJson: "{\"mykey\": 4.5 }",
				exceptedResult: 4.5f));

			ReadValueTestDataList.Add(new ReadValueTestDataParam<float>(
				baseJson: "{\"mykey\": null }",
				exceptedResult: 0));

			ReadValueTestDataList.Add(new ReadValueTestDataParam<float>(
				baseJson: "{\"mykey\": 0 }",
				exceptedResult: 0));

			ReadValueTestDataList.Add(new ReadValueTestDataParam<float>(
				baseJson: "{\"mykey\": null }",
				exceptedResult: null,
				methodType: eMethodType.Nullable));
		}

        private static void InitSetValueTestData()
        {
			SetValueTestDataList.Add(new SetValueTestDataParam(
				baseJson: "{\"mykey\": \"myvalue\"}",
				value: "newvalue",
				exceptedJson: "{\"mykey\": \"newvalue\"}"));

			SetValueTestDataList.Add(new SetValueTestDataParam(
				baseJson: "{\"mykey\": \"myvalue\"}",
				value: "",
				exceptedJson: "{\"mykey\": \"\"}"));

			SetValueTestDataList.Add(new SetValueTestDataParam(
				baseJson: "{\"mykey\": \"myvalue\"}",
				value: null,
				exceptedJson: "{\"mykey\": null}"));

			SetValueTestDataList.Add(new SetValueTestDataParam(
				baseJson: "{\"mykey\": \"\"}",
				value: "newvalue",
				exceptedJson: "{\"mykey\": \"newvalue\"}"));

			SetValueTestDataList.Add(new SetValueTestDataParam(
				baseJson: "{\"mykey\": \"\"}",
				value: "",
				exceptedJson: "{\"mykey\": \"\"}"));

			SetValueTestDataList.Add(new SetValueTestDataParam(
				baseJson: "{\"mykey\": \"\"}",
				value: null,
				exceptedJson: "{\"mykey\": null}"));

			SetValueTestDataList.Add(new SetValueTestDataParam(
				baseJson: "{\"mykey\": null}",
				value: "newvalue",
				exceptedJson: "{\"mykey\": \"newvalue\"}"));

			SetValueTestDataList.Add(new SetValueTestDataParam(
				baseJson: "{\"mykey\": null}",
				value: "",
				exceptedJson: "{\"mykey\": \"\"}"));

			SetValueTestDataList.Add(new SetValueTestDataParam(
				baseJson: "{\"mykey\": null}",
				value: null,
				exceptedJson: "{\"mykey\": null}"));

			SetValueTestDataList.Add(new SetValueTestDataParam(
				baseJson: "{\"mykey\": 10}",
				value: 20,
				exceptedJson: "{\"mykey\": 20}"));

			SetValueTestDataList.Add(new SetValueTestDataParam(
				baseJson: "{\"mykey\": null}",
				value: 20,
				exceptedJson: "{\"mykey\": 20}"));

			SetValueTestDataList.Add(new SetValueTestDataParam(
				baseJson: "{\"mykey\": 10}",
				value: null,
				exceptedJson: "{\"mykey\": null}"));

			SetValueTestDataList.Add(new SetValueTestDataParam(
				baseJson: "{\"mykey\": null}",
				value: null,
				exceptedJson: "{\"mykey\": null}"));

			SetValueTestDataList.Add(new SetValueTestDataParam(
				baseJson: "{\"mykey\": true}",
				value: false,
				exceptedJson: "{\"mykey\": false}"));

			SetValueTestDataList.Add(new SetValueTestDataParam(
				baseJson: "{\"mykey\": true}",
				value: null,
				exceptedJson: "{\"mykey\": null}"));

			SetValueTestDataList.Add(new SetValueTestDataParam(
				baseJson: "{\"mykey\": null}",
				value: false,
				exceptedJson: "{\"mykey\": false}"));

			SetValueTestDataList.Add(new SetValueTestDataParam(
				baseJson: "{\"mykey\": null}",
				value: null,
				exceptedJson: "{\"mykey\": null}"));

			SetValueTestDataList.Add(new SetValueTestDataParam(
				baseJson: "{\"mykey\": [1,2]}",
				value: new int[] { 10, 20, 30 },
				exceptedJson: "{\"mykey\": [ 10, 20, 30 ]}"));

			SetValueTestDataList.Add(new SetValueTestDataParam(
				baseJson: "{\"mykey\": [1]}",
				value: new int[] { 10, 20, 30 },
				exceptedJson: "{\"mykey\": [ 10, 20, 30 ]}"));

			SetValueTestDataList.Add(new SetValueTestDataParam(
				baseJson: "{\"mykey\": []}",
				value: new int[] { 10, 20, 30 },
				exceptedJson: "{\"mykey\": [ 10, 20, 30 ]}"));

			SetValueTestDataList.Add(new SetValueTestDataParam(
				baseJson: "{\"mykey\": null}",
				value: new int[] { 10, 20, 30 },
				exceptedJson: "{\"mykey\": [ 10, 20, 30 ]}"));

			SetValueTestDataList.Add(new SetValueTestDataParam(
				baseJson: "{\"mykey\": [1,2]}",
				value: new int[] { },
				exceptedJson: "{\"mykey\": []}"));

			SetValueTestDataList.Add(new SetValueTestDataParam(
				baseJson: "{\"mykey\": [1]}",
				value: new int[] { },
				exceptedJson: "{\"mykey\": []}"));

			SetValueTestDataList.Add(new SetValueTestDataParam(
				baseJson: "{\"mykey\": []}",
				value: new int[] { },
				exceptedJson: "{\"mykey\": []}"));

			SetValueTestDataList.Add(new SetValueTestDataParam(
				baseJson: "{\"mykey\": null}",
				value: new int[] { },
				exceptedJson: "{\"mykey\": []}"));

			SetValueTestDataList.Add(new SetValueTestDataParam(
				baseJson: "{\"mykey\": [1,2]}",
				value: null,
				exceptedJson: "{\"mykey\": null}"));

			SetValueTestDataList.Add(new SetValueTestDataParam(
				baseJson: "{\"mykey\": [1]}",
				value: null,
				exceptedJson: "{\"mykey\": null}"));

			SetValueTestDataList.Add(new SetValueTestDataParam(
				baseJson: "{\"mykey\": []}",
				value: null,
				exceptedJson: "{\"mykey\": null}"));

			SetValueTestDataList.Add(new SetValueTestDataParam(
				baseJson: "{\"mykey\": null}",
				value: null,
				exceptedJson: "{\"mykey\": null}"));

			SetValueTestDataList.Add(new SetValueTestDataParam(
				baseJson: "{\"mykey\": [\"one\",\"two\"]}",
				value: new string[] { "one", "two", "three" },
				exceptedJson: "{\"mykey\": [ \"one\", \"two\", \"three\" ]}"));

			SetValueTestDataList.Add(new SetValueTestDataParam(
				baseJson: "{\"mykey\": [\"one\"]}",
				value: new string[] { "one", "two", "three" },
				exceptedJson: "{\"mykey\": [ \"one\", \"two\", \"three\" ]}"));

			SetValueTestDataList.Add(new SetValueTestDataParam(
				baseJson: "{\"mykey\": []}",
				value: new string[] { "one", "two", "three" },
				exceptedJson: "{\"mykey\": [ \"one\", \"two\", \"three\" ]}"));

			SetValueTestDataList.Add(new SetValueTestDataParam(
				baseJson: "{\"mykey\": null}",
				value: new string[] { "one", "two", "three" },
				exceptedJson: "{\"mykey\": [ \"one\", \"two\", \"three\" ]}"));

			SetValueTestDataList.Add(new SetValueTestDataParam(
				baseJson: "{\"mykey\": [\"one\",\"two\"]}",
				value: new string[] { },
				exceptedJson: "{\"mykey\": []}"));

			SetValueTestDataList.Add(new SetValueTestDataParam(
				baseJson: "{\"mykey\": [\"one\"]}",
				value: new string[] { },
				exceptedJson: "{\"mykey\": []}"));

			SetValueTestDataList.Add(new SetValueTestDataParam(
				baseJson: "{\"mykey\": []}",
				value: new string[] { },
				exceptedJson: "{\"mykey\": []}"));

			SetValueTestDataList.Add(new SetValueTestDataParam(
				baseJson: "{\"mykey\": null}",
				value: new string[] { },
				exceptedJson: "{\"mykey\": []}"));

			SetValueTestDataList.Add(new SetValueTestDataParam(
				baseJson: "{\"mykey\": [\"one\",\"two\"]}",
				value: null,
				exceptedJson: "{\"mykey\": null}"));

			SetValueTestDataList.Add(new SetValueTestDataParam(
				baseJson: "{\"mykey\": [\"one\"]}",
				value: null,
				exceptedJson: "{\"mykey\": null}"));

			SetValueTestDataList.Add(new SetValueTestDataParam(
				baseJson: "{\"mykey\": []}",
				value: null,
				exceptedJson: "{\"mykey\": null}"));

			SetValueTestDataList.Add(new SetValueTestDataParam(
				baseJson: "{\"mykey\": 3}",
				value: 3.54f,
				exceptedJson: "{\"mykey\": 3.54}"));

			SetValueTestDataList.Add(new SetValueTestDataParam(
				baseJson: "{\"mykey\": null}",
				value: null,
				exceptedJson: "{\"mykey\": null}"));
		}
	}
}