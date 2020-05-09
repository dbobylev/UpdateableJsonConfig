using System;
using System.Collections.Generic;
using System.Text;

namespace JsonConfigForNetCoreConsoleApp.Tests.TestSource
{
    public class SetValueTestDataParam: ITestDataParam
    {
        public string BaseJson { get; private set; }
        public string ExceptedJson { get; private set; }
        public object Value { get; private set; }

        public SetValueTestDataParam(string baseJson, string exceptedJson, object value)
        {
            BaseJson = baseJson;
            ExceptedJson = exceptedJson;
            Value = value;
        }

        public override string ToString()
        {
            return $"BaseJson: {BaseJson}\r\nValue: [{Value}]\r\nExceptedJson: {ExceptedJson}";
        }
    }
}
