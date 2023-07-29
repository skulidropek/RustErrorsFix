using RustErrorsFix.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using RustErrorsFix.Core;
using RustErrorsFix.View;
using System.Windows;
using RustErrorsFix.Roslyn.Managers;

namespace RustErrorsFix.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
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

        public MainWindowViewModel()
        {
            BoostyCommand = new RelayCommand(BoostyCommandExecute);
            DiscordCommand = new RelayCommand(DiscordCommandExecute);
            YoutubeCommand = new RelayCommand(YoutubeCommandExecute);
            OpenFriendPageCommand = new RelayCommand(OpenFriendPageCommandExecute);
            QuitCommand = new RelayCommand(QuitCommandExecute);
            ChangeLanguageCommand = new RelayCommand(ChangeLanguageCommandExecute);

            LangManager.Subscribe((lang) =>
            {
                LangImagePath = lang ? LangManager.EnPathLang : LangManager.RuPathLang;
            });

            LangManager.OnLangChangedInvoke();
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
            if(PageManager.Instance.ActivePage is FriendsUserControl)
            {
                PageManager.Instance.OpenChocePlugins();
                return;
            }

            PageManager.Instance.OpenFriends();
        }

        public void QuitCommandExecute(object sender)
        {
            Environment.Exit(0);
        }

        public void ChangeLanguageCommandExecute(object sender)
        {
            LangManager.Change();
        }
    }
}
