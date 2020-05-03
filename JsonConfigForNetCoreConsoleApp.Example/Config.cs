using JsonConfigForNetCoreConsoleApp;

namespace JsonConfigForNetCoreConsoleApp.Example
{
    class Config : BaseJsonConfig
    {
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
            get => GetValue<bool>("BoolSection:bool1");
            set => SetValue("BoolSection:bool1", value);
        }

        public bool? bool2
        {
            get => GetNullableValue<bool>("BoolSection:bool2");
            set => SetValue("BoolSection:bool2", value);
        }

        #region Singleton
        private static Config _Instanse;
        public static Config Instanse()
        {
            if (_Instanse == null)
                _Instanse = new Config();
            return _Instanse;
        }
        private Config() : base()
        {

        }
        #endregion
    }
}
