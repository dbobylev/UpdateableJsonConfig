using JsonConfigForNetCoreConsoleApp;

namespace JsonConfigForNetCoreConsoleApp.Example
{
    class Config : BaseJsonConfig
    {
        public string Url 
        {
            get => GetValue("Url"); 
        }
        
        public string Name 
        {
            get => GetValue("Name");
            set => SetValue("Name", value);
        }
        
        public int Age 
        { 
            get => GetIntValue("Age"); 
            set => SetValue("Age", value); 
        }

        public int? MaxTimeOut 
        { 
            get => GetNullableIntValue("MaxTimeOut");
            set => SetValue("MaxTimeOut", value);
        }

        public bool bool1
        {
            get => GetBoolValue("BoolSection:bool1");
            set => SetValue("BoolSection:bool1", value);
        }

        public bool? bool2
        {
            get => GetNullableBoolValue("BoolSection:bool2");
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
