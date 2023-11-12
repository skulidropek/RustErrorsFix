using RustErrorsFix.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustErrorsFix.Roslyn.Managers
{
    public class LangManager
    {
        private Dictionary<string, string> _langsRu = new Dictionary<string, string>()
        {
            { "Select", "ВЫБРАТЬ" },
            { "SelectPlugin", "ВЫБЕРИТЕ ПЛАГИН" },
            { "SelectFolderManaged", "ВЫБЕРИТЕ ПАПКУ MANAGED" },
            { "OurFriends", "НАШИ ДРУЗЬЯ" },
            { "Back", "НАЗАД" },
            { "NotSelectFile", "Вы не выбрали файл" },
            { "PluginReady", "Плагин готов!" },
            { "ErrorsPlugin", "ОШИБКИ В ПЛАГИНЕ" },
            { "FixSelection", "ВЫБОР ФИКСА" },
            { "Fix", "ФИКС" },
            { "Roslyn", "Рослин" }
        };

        private Dictionary<string, string> _langsEn = new Dictionary<string, string>()
        {
            { "Select", "CHOOSE" },
            { "SelectPlugin", "CHOOSE PLUGIN" },
            { "SelectFolderManaged", "CHOOSE FOLDER MANAGED" },
            { "OurFriends", "OUR FRIENDS" },
            { "Back", "BACK" },
            { "NotSelectFile", "You have not selected a file" },
            { "PluginReady", "Plugin is ready!" },
            { "ErrorsPlugin", "ERRORS IN THE PLUGIN" },
            { "FixSelection", "FIX SELECTION" },
            { "Fix", "FIX" },
            { "Roslyn", "Roslyn" }
        };

        private bool _en;

        private Action<bool> OnLangChanged = delegate { };

        public const string RuPathLang = "/wwwroot/Images/BtnLanguage.png";
        public const string EnPathLang = "/wwwroot/Images/eng.png";

        public LangManager()
        {
            var lang = Registry.GetValue("Lang");

            if (string.IsNullOrEmpty(lang))
            {
                Registry.AddValue("Lang", "En");
            }

            lang = Registry.GetValue("Lang");

            _en = lang == "En";
        }

        public void OnLangChangedInvoke()
        {
            OnLangChanged.Invoke(_en);
        }

        public void Subscribe(Action<bool> action)
        {
            OnLangChanged += action;
        }

        public void UnSubscribe(Action<bool> action)
        {
            OnLangChanged -= action;
        }

        public void Change()
        {
            _en = !_en;

            Registry.SetValue("Lang", _en ? "En" : "Ru");
            OnLangChanged?.Invoke(_en);
        }

        public string GetLang(string lang)
        {
            if (_en)
                return _langsEn[lang];

            return _langsRu[lang];
        }
    }
}
