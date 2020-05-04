using JsonConfigForNetCoreConsoleApp;
using System.Linq;

namespace JsonConfigForNetCoreConsoleApp.Example
{
    class Config : BaseJsonConfig
    {
        private static Config _Instanse;
        public static Config Instanse()
        {
            if (_Instanse == null)
                _Instanse = new Config();
            return _Instanse;
        }
        public static void InitConfig(string filename = null)
        {
            _Instanse = new Config(filename);
        }

        private Config(string filename = null) : base(filename)
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
