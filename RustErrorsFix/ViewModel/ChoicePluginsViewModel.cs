using Microsoft.Win32;
using Ookii.Dialogs.Wpf;
using RustErrorsFix.Core;
using RustErrorsFix.Core.Abstract;
using RustErrorsFix.Core.Factory;
using RustErrorsFix.Roslyn.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RustErrorsFix.ViewModel
{
    internal class ChoicePluginsViewModel : ViewModelBase
    {
        private readonly LangManager _langManager;

        private bool _roslynReferenseHave = !string.IsNullOrEmpty(RustReferenseManager.Path);
        public string ChoiceButtonText => _langManager.GetLang("Select");

        public string RoslynButtonText => _roslynReferenseHave ? _langManager.GetLang("Roslyn") : _langManager.GetLang("SelectFolderManaged");

        public string SelectPluginText => _langManager.GetLang("SelectPlugin");

        public ICommand ChoicePluginCommand { get; private set; }
        public ICommand RoslynPageOpenCommand { get; private set; }
        public ICommand ResetManagedFolderCommand { get; private set; }

        PluginFixerAbstractFactory Legasy = new LegasyPluginFixerAbstractFactory();

        private PageManager _pageManager;

        public ChoicePluginsViewModel(PageManager pageManager, LangManager langManager)
        {
            _pageManager = pageManager;
            _langManager = langManager;
            ChoicePluginCommand = new RelayCommand(ChoicePluginCommandExecute);
            RoslynPageOpenCommand = new RelayCommand(RoslynPageOpenCommandExecute);
            ResetManagedFolderCommand = new RelayCommand(ResetManagedFolderCommandExecute, (obj) => !string.IsNullOrEmpty(RustReferenseManager.Path));

            _langManager.Subscribe(OnLangChanged);

            _langManager.OnLangChangedInvoke();
        }

        private void ResetManagedFolderCommandExecute(object obj)
        {
            RustReferenseManager.Path = "";
            _roslynReferenseHave = false;
            OnPropertyChanged("RoslynButtonText");
        }

        ~ChoicePluginsViewModel()
        {
            _langManager.UnSubscribe(OnLangChanged);
        }

        public void OnLangChanged(bool en)
        {
            OnPropertyChanged("ChoiceButtonText");
            OnPropertyChanged("SelectPluginText");
            OnPropertyChanged("RoslynButtonText");
        }

        public void ChoicePluginCommandExecute(object obj)
        {
            string path = GetPathOpenFileDialog();

            if (string.IsNullOrEmpty(path))
            {
                MessageBox.Show(_langManager.GetLang("NotSelectFile"));
                return;
            }

            var plugin = System.IO.File.ReadAllText(path);

            plugin = Legasy.FixPlugin(plugin);

            plugin = Regex.Replace(plugin, @"(\[Info\("".+"", "").+("", "".+""\)\])", "/*ПЛАГИН БЫЛ ПОФИКШЕН С ПОМОЩЬЮ ПРОГРАММЫ СКАЧАНОЙ С https://discord.gg/dNGbxafuJn */ $1https://discord.gg/dNGbxafuJn$2");

            System.IO.File.WriteAllText(path + "FIX.cs", plugin);
        }

        public void RoslynPageOpenCommandExecute(object obj)
        {
            var path = RustReferenseManager.Path;
            if (string.IsNullOrEmpty(path))
            {
                path = GetPathOpenFolderDialog();

                if(string.IsNullOrEmpty(path))
                {
                    MessageBox.Show("Need select Managed folder");
                    return;
                }

                RustReferenseManager.Path = path;
                _roslynReferenseHave = true;
                OnPropertyChanged("RoslynButtonText");
                return;
            }

            _pageManager.OpenRoslyn();
        }

        private string GetPathOpenFolderDialog()
        {
            var folderDialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();

            if(folderDialog.ShowDialog() == false)
                return "";

            return folderDialog.SelectedPath;
        }

        private string GetPathOpenFileDialog()
        {
            VistaOpenFileDialog openFileDialog = new VistaOpenFileDialog(); 

            openFileDialog.Filter = "Плагины раст (*.cs)|*.cs";

            if (openFileDialog.ShowDialog() == false)
            {
                return "";
            }

            return openFileDialog.FileName;
        }
    }
}
