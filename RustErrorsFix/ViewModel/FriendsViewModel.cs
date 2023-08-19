using RustErrorsFix.Core;
using RustErrorsFix.Roslyn.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RustErrorsFix.ViewModel
{
    public class FriendsViewModel : ViewModelBase
    {
        private readonly LangManager _langManager;
        private readonly PageManager _pageManager;

        public string OurFriendsText => _langManager.GetLang("OurFriends");
        public string BackText => _langManager.GetLang("Back");

        public ICommand BackCommand { get; }

        public FriendsViewModel(LangManager langManager, PageManager pageManager)
        {
            _langManager = langManager;
            _pageManager = pageManager;

            _langManager.Subscribe(OnLangChanged);

            BackCommand = new RelayCommand(BackCommandExecute);
        }

        private void BackCommandExecute(object obj)
        {
            _pageManager.OpenChocePlugins();
        }

        ~FriendsViewModel()
        {
            _langManager.UnSubscribe(OnLangChanged);
        }

        private void OnLangChanged(bool obj)
        {
            OnPropertyChanged(nameof(OurFriendsText));
            OnPropertyChanged(nameof(BackText));
        }
    }
}
