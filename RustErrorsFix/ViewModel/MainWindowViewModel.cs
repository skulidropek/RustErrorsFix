using RustErrorsFix.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using RustErrorsFix.View;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using RustErrorsFix.Roslyn.Managers;

namespace RustErrorsFix.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private PageManager _pageManager;
        private LangManager _langManager;
        private string _langImagePath;

        public ICommand BoostyCommand { get; private set; }
        public ICommand DiscordCommand { get; private set; }
        public ICommand YoutubeCommand { get; private set; }
        public ICommand OpenFriendPageCommand { get; private set; }
        public ICommand QuitCommand { get; private set; }
        public ICommand ChangeLanguageCommand { get; private set; }

        public string LangImagePath 
        {
            get => _langImagePath;
            set 
            {
                _langImagePath = value;
                OnPropertyChanged("LangImagePath"); 
            }
        }

        public MainWindowViewModel(PageManager pageManager, LangManager langManager)
        {
            _pageManager = pageManager;
            _langManager = langManager;

            _pageManager.OpenChocePlugins();

            BoostyCommand = new RelayCommand(BoostyCommandExecute);
            DiscordCommand = new RelayCommand(DiscordCommandExecute);
            YoutubeCommand = new RelayCommand(YoutubeCommandExecute);
            OpenFriendPageCommand = new RelayCommand(OpenFriendPageCommandExecute);
            QuitCommand = new RelayCommand(QuitCommandExecute);
            ChangeLanguageCommand = new RelayCommand(ChangeLanguageCommandExecute);

            _langManager.Subscribe((lang) =>
            {
                LangImagePath = lang ? LangManager.EnPathLang : LangManager.RuPathLang;
            });

            _langManager.OnLangChangedInvoke();
            _langManager = langManager;
        }

        public void BoostyCommandExecute(object sender)
        {
            Process.Start("https://boosty.to/skulidropek");
        }

        public void DiscordCommandExecute(object sender)
        {
            Process.Start("https://discord.gg/CBqDuqDWvS");
        }

        public void YoutubeCommandExecute(object sender) 
        {
            Process.Start("https://www.youtube.com/@skulidropek607");
        }

        public void OpenFriendPageCommandExecute(object sender)
        {
            if (_pageManager.ActivePage is FriendsUserControl)
            {
                _pageManager.OpenChocePlugins();
                return;
            }

            _pageManager.OpenFriends();
        }

        public void QuitCommandExecute(object sender)
        {
            Environment.Exit(0);
        }

        public void ChangeLanguageCommandExecute(object sender)
        {
            _langManager.Change();
        }
    }
}
