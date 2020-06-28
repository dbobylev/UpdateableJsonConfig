using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UpdateableConfig
{
    public class UpdateableJsonConfig
    {
        Serilog.Core.Logger log = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.Console()
            .CreateLogger();

        /// <summary>
        /// Представляет корень иерархии IConfiguration
        /// </summary>
        private IConfigurationRoot _Config;

        /// <summary>
        /// Путь до Json файла, по умолчанию в директории приложения
        /// </summary>
        private string _FilePath { get; set; }

        /// <summary>
        /// Объект Json с измененными параметрами, требует последующего сохранения
        /// </summary>
        private JObject _NotSavedJsonObject;

        /// <summary>
        /// Базовый класс с управлением Json файла
        /// </summary>
        /// <param name="jsonfileName">Название Json файла (файл должен находиться в той же директории)</param>
        protected UpdateableJsonConfig(IConfigurationRoot config, string filePath)
        {
            _Config = config;
            _FilePath = filePath;
        }

        /// <summary>
        /// Получить значение параметра
        /// </summary>
        /// <typeparam name="T">Тип ожидаемого значения</typeparam>
        /// <param name="key">Путь до параметра в объекте JSON (указывать через двоеточие)</param>
        /// <returns>Значение праметра, при отсутствии значение типа по умолчанию</returns>
        public T GetValue<T>([CallerMemberName] string key = "")
        {
            return GetValueWithCheckNull(out T value, key) ? default : value;
        }

        /// <summary>
        /// Получить значение параметра
        /// </summary>
        /// <typeparam name="T">Тип ожидаемого значения</typeparam>
        /// <param name="key">Путь до параметра в объекте JSON (указывать через двоеточие)</param>
        /// <returns>Значение праметра, при отсутствии - null</returns>
        public T? GetNullableValue<T>([CallerMemberName] string key = "") where T : struct
        {
            return GetValueWithCheckNull(out T value, key) ? default(T?) : value;
        }

        public IEnumerable<T> GetEnumerableValue<T>([CallerMemberName] string key = "")
        {
            return _Config.GetSection(key).GetChildren().Select(x=> (T)Convert.ChangeType(x.Value, typeof(T)));
        }

        /// <summary>
        /// Получить значение параметра из Json файла, так же проверить является ли оно null
        /// </summary>
        /// <typeparam name="T">Тип ожидаемого параметра</typeparam>
        /// <param name="key">Путь до параметра в объекте JSON (указывать через двоеточие)</param>
        /// <param name="value">Значение параметра</param>
        /// <returns>Является ли значение - Null - Y/N</returns>
        private bool GetValueWithCheckNull<T>(out T value, [CallerMemberName] string key = "")
        {
            string stringValue = _Config[key];
            log.Debug($"Запрошено значение параметра key=[{key}] stringValue=[{stringValue}]");
            if (string.IsNullOrEmpty(stringValue))
                log.Warning($"Для ключа {key} значение пустое");

            bool IsEmpty = string.IsNullOrEmpty(stringValue) && typeof(T) != typeof(string);

            value = IsEmpty ? default : (T)Convert.ChangeType(stringValue, typeof(T), new CultureInfo("en-US"));
            return IsEmpty;
        }

        /// <summary>
        /// Установить значение параметра
        /// </summary>
        /// <param name="key">Путь до параметра в объекте JSON (указывать через двоеточие)</param>
        /// <param name="value">Новое значение</param>
        public void SetValue(object value, [CallerMemberName] string key = "")
        {
            log.Debug($"Устанавливается новое значение: [{value}] для параметра: [{key}]");
            _Config[key] = value == null ? null : value.ToString();

            if (_NotSavedJsonObject == null)
                _NotSavedJsonObject = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(_FilePath));

            key = key.Replace(":", ".");
            _NotSavedJsonObject.SelectToken(key).Replace(value == null ? null : JToken.FromObject(value));
        }

        /// <summary>
        /// Сохранить изменненные параметры
        /// </summary>
        public void Save()
        {
            if (_NotSavedJsonObject != null)
            {
                string output = JsonConvert.SerializeObject(_NotSavedJsonObject, Formatting.Indented);
                File.WriteAllText(_FilePath, output);
                _NotSavedJsonObject = null;
            }
        }
    }
}
