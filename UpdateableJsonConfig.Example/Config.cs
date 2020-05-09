using UpdateableJsonConfig;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace UpdateableJsonConfig.Example
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

        public string Url 
        {
            get => GetValue<string>("Url"); 
        }
        
        public string Name 
        {
            get => GetValue<string>("Name");
            set => SetValue("Name", value);
        }
        
        public int Age 
        { 
            get => GetValue<int>("Age"); 
            set => SetValue("Age", value); 
        }

        public int? MaxTimeOut 
        { 
            get => GetNullableValue<int>("MaxTimeOut");
            set => SetValue("MaxTimeOut", value);
        }

        public bool bool1
        {
            get => GetValue<bool>("MySection:MyBool");
            set => SetValue("MySection:MyBool", value);
        }

        public int[] MyArray
        {
            get => GetEnumerableValue<int>("MySection:MyArray").ToArray();
            set => SetValue("MySection:MyArray", value);
        }
    }
}
