## Getting started
Use your custom updateable the settings class like this:

```cs
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
        
        /* Your properties */
    }
```

See [Config.cs](https://github.com/dbobylev/UpdateableJsonConfig/blob/master/UpdateableJsonConfig.Example/Config.cs).