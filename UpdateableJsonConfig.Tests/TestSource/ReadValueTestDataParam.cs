using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace UpdateableConfig.Tests.TestSource
{
    public class ReadValueTestDataParam<T>: ITestDataParam
    {
        public string BaseJson { get; private set; }
        public object ExceptedResult { get; private set; }
        public eMethodType MethodType { get; private set; }

        public ReadValueTestDataParam(string baseJson, object exceptedResult, eMethodType methodType = eMethodType.General)
        {
            BaseJson = baseJson;
            ExceptedResult = exceptedResult;
            MethodType = methodType;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"TestCase: ReadValueTestDataParam<{typeof(T).Name}>");
            sb.AppendLine($"BaseJson: {BaseJson}");
            sb.AppendLine($"ExceptedResult: [{ExceptedResult}]");
            sb.Append($"MethodType: {MethodType}");
            return sb.ToString();
        }

    }
}
