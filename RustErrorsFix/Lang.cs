using RustErrorsFix.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustErrorsFix
{
    internal class Lang : Sigleton<Lang>
    {
        Dictionary<string, string> _langsRu = new Dictionary<string, string>()
        {
            { "Select", "ВЫБРАТЬ" },
            { "SelectPlugin", "ВЫБЕРИТЕ ПЛАГИН" },
            { "OurFriends", "НАШИ ДРУЗЬЯ" },
            { "Back", "НАЗАД" },
        };

        Dictionary<string, string> _langsEn = new Dictionary<string, string>()
        {
            { "Select", "CHOOSE" },
            { "SelectPlugin", "CHOOSE PLUGIN" },
            { "OurFriends", "OUR FRIENDS" },
            { "Back", "BACK" },
        };

        private bool _en;

        public Action<bool> OnLangChanged = delegate { };

        public void OnLangChangedInvoke()
        {
            OnLangChanged.Invoke(_en);
        }

        public Lang()
        {
            var lang = Registry.GetValue("Lang");

            if (string.IsNullOrEmpty(lang))
            {
                Registry.AddValue("Lang", "En");
            }

            lang = Registry.GetValue("Lang");

            _en = lang == "En";
        }

        public void ChangeLang()
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
