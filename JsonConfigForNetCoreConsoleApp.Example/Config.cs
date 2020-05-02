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
        }
        
        public int Age 
        { 
            get => GetIntValue("Age"); 
            set => SetValue("Age", value); 
        }

        public int? MaxTimeOut 
        { 
            get => GetNullableIntValue("MySection:MaxTimeOut");
            set => SetValue("MySection:MaxTimeOut", value);
        }

        public int? MaxPing 
        { 
            get => GetNullableIntValue("MySection:MaxPing");
            set => SetValue("MySection:MaxPing", value);
        }

        public string foo 
        { 
            get => GetValue("MySection:foo");
            set => SetValue("MySection:foo", value);
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
