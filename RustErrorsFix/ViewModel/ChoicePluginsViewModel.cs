using Microsoft.Win32;
using Ookii.Dialogs.Wpf;
using Roslyn_test.Core;
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
        private bool _roslynReferenseHave = !string.IsNullOrEmpty(RustReferenseManager.Path);
        public string ChoiceButtonText => LangManager.GetLang("Select");

        public string RoslynButtonText => _roslynReferenseHave ? LangManager.GetLang("Roslyn") : LangManager.GetLang("SelectFolderManaged");

        public string SelectPluginText => LangManager.GetLang("SelectPlugin");

        public ICommand ChoicePluginCommand { get; private set; }
        public ICommand RoslynPageOpenCommand { get; private set; }

        PluginFixerAbstractFactory Legasy = new LegasyPluginFixerAbstractFactory();

        public ChoicePluginsViewModel()
        {
            ChoicePluginCommand = new RelayCommand(ChoicePluginCommandExecute);
            RoslynPageOpenCommand = new RelayCommand(RoslynPageOpenCommandExecute);

            LangManager.Subscribe(OnLangChanged);

            LangManager.OnLangChangedInvoke();
        }

        ~ChoicePluginsViewModel()
        {
            LangManager.UnSubscribe(OnLangChanged);
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
                MessageBox.Show(LangManager.GetLang("NotSelectFile"));
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

            PageManager.Instance.OpenRoslyn();
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
