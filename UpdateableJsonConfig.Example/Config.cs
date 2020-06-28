using UpdateableConfig;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace UpdateableConfig.Example
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
            get => GetValue<string>();
            set => SetValue(value);
        }
        
        public int Age 
        { 
            get => GetValue<int>(); 
            set => SetValue(value); 
        }

        public int? MaxTimeOut 
        { 
            get => GetNullableValue<int>();
            set => SetValue(value);
        }

        public bool bool1
        {
            get => GetValue<bool>("MySection:MyBool");
            set => SetValue(value, "MySection:MyBool");
        }

        public int[] MyArray
        {
            get => GetEnumerableValue<int>("MySection:MyArray").ToArray();
            set => SetValue(value, "MySection:MyArray");
        }
    }
}
