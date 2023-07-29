using RustErrorsFix.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustErrorsFix.Roslyn.Managers
{
    internal class LangManager : Sigleton<LangManager>
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

        private LangManager()
        {
            var lang = Registry.GetValue("Lang");

            if (string.IsNullOrEmpty(lang))
            {
                Registry.AddValue("Lang", "En");
            }

            lang = Registry.GetValue("Lang");

            _en = lang == "En";
        }

        public static void OnLangChangedInvoke()
        {
            Instance.OnLangChanged.Invoke(Instance._en);
        }

        public static void Subscribe(Action<bool> action)
        {
            if (Instance == null)
                new LangManager();

            Instance.OnLangChanged += action;
        }

        public static void UnSubscribe(Action<bool> action)
        {
            if (Instance == null)
                new LangManager();

            Instance.OnLangChanged -= action;
        }

        public static void Change()
        {
            if (Instance == null)
                new LangManager();

            Instance._en = !Instance._en;

            Registry.SetValue("Lang", Instance._en ? "En" : "Ru");

            Instance.OnLangChanged?.Invoke(Instance._en);
        }

        public static string GetLang(string lang)
        {
            if (Instance._en)
                return Instance._langsEn[lang];

            return Instance._langsRu[lang];
        }
    }
}
