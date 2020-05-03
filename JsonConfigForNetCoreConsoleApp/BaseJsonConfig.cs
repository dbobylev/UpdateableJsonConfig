using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace JsonConfigForNetCoreConsoleApp
{
    public abstract class BaseJsonConfig
    {
        /// <summary>
        /// Представляет корень иерархии IConfiguration
        /// </summary>
        private IConfigurationRoot _Config;

        /// <summary>
        /// Имя Json файла с настройками
        /// </summary>
        private string _JsonFileName;

        /// <summary>
        /// Путь до Json файла, по умолчанию в директории приложения
        /// </summary>
        private string _FilePath { get => Path.Combine(AppContext.BaseDirectory, _JsonFileName); }

        /// <summary>
        /// Объект Json с измененными параметрами, требует последующего сохранения
        /// </summary>
        private JObject _NotSavedJsonObject;

        /// <summary>
        /// Базовый класс с управлением Json файла
        /// </summary>
        /// <param name="jsonfileName">Название Json файла (файл должен находиться в той же директории)</param>
        public BaseJsonConfig(string jsonfileName = "appsettings.json")
        {
            _JsonFileName = jsonfileName;
            _Config = new ConfigurationBuilder().AddJsonFile(jsonfileName).Build();
        }

        #region Getters
        /// <summary>
        /// Получить строковое значение параметра
        /// </summary>
        /// <param name="key">Путь до параметра в объекте JSON (указывать через двоеточие)</param>
        /// <returns>Значение параметра</returns>
        protected string GetValue(string key)
        {
            return _Config[key];
        }

        /// <summary>
        /// Получить числовое значение параметра
        /// </summary>
        /// <param name="key">Путь до параметра в объекте JSON (указывать через двоеточие)</param>
        /// <returns>Значение параметра</returns>
        protected int GetIntValue(string key)
        {
            string stringValue = _Config[key];
            if (string.IsNullOrEmpty(stringValue))
                throw new ArgumentNullException(key, $"Не найдено значение: {key}");
            else
                return int.Parse(stringValue);
        }

        /// <summary>
        /// Получить числовое обнуляемое значение параметра
        /// </summary>
        /// <param name="key">Путь до параметра в объекте JSON (указывать через двоеточие)</param>
        /// <returns>Значение параметра</returns>
        protected int? GetNullableIntValue(string key)
        {
            string s = _Config[key];
            if (string.IsNullOrEmpty(s))
                return null;
            else
                return int.Parse(s);
        }

        /// <summary>
        /// Получить булевое значение параметра
        /// </summary>
        /// <param name="key">Путь до параметра в объекте JSON (указывать через двоеточие)</param>
        /// <returns>Значение параметра</returns>
        protected bool GetBoolValue(string key)
        {
            return bool.Parse(_Config[key]);
        }

        /// <summary>
        /// Получить булевое обнуляемое значение параметра
        /// </summary>
        /// <param name="key">Путь до параметра в объекте JSON (указывать через двоеточие)</param>
        /// <returns>Значение параметра</returns>
        protected bool? GetNullableBoolValue(string key)
        {
            string s = _Config[key];
            if (string.IsNullOrEmpty(s))
                return null;
            else
                return bool.Parse(s);
        }
        #endregion

        #region Setters
        /// <summary>
        /// Установить строковое значение параметра
        /// </summary>
        /// <param name="key">Путь до параметра в объекте JSON (указывать через двоеточие)</param>
        /// <param name="value">Значение</param>
        protected void SetValue(string key, string value)
        {
            _Config[key] = value;
            GetToken(key).Replace(value);
        }

        /// <summary>
        /// Установить числовое значение параметра
        /// </summary>
        /// <param name="key">Путь до параметра в объекте JSON (указывать через двоеточие)</param>
        /// <param name="value">Значение</param>
        protected void SetValue(string key, int? value)
        {
            _Config[key] = value.ToString();
            GetToken(key).Replace(value);
        }

        /// <summary>
        /// Установить числовое значение параметра
        /// </summary>
        /// <param name="key">Путь до параметра в объекте JSON (указывать через двоеточие)</param>
        /// <param name="value">Значение</param>
        protected void SetValue(string key, bool? value)
        {
            _Config[key] = value.ToString();
            GetToken(key).Replace(value);
        }
        #endregion

        /// <summary>
        /// Получить токен объекта Json по указанному ключу
        /// </summary>
        /// <param name="key">Путь до параметра в объекте JSON (указывать через двоеточие)</param>
        /// <returns>токен Json</returns>
        private JToken GetToken(string key)
        {
            if (_NotSavedJsonObject == null)
                _NotSavedJsonObject = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(_FilePath));

            key = key.Replace(":", ".");
            return _NotSavedJsonObject.SelectToken(key);
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
