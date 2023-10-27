using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Win32;
using Ookii.Dialogs.Wpf;
using RustErrorsFix.Core;
using RustErrorsFix.Core.Factory;
using RustErrorsFix.Roslyn.Factory;
using RustErrorsFix.Roslyn.Managers;
using RustErrorsFixLibrary.Core;
using RustErrorsFixLibrary.Core.Interface;
using RustErrorsFixLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RustErrorsFix.ViewModel
{
    internal class RoslynViewModel : ViewModelBase
    {
        private readonly LangManager _langManager;
        private readonly PageManager _pageManager;
        private readonly CodeFixManager _codeFixManager;
        private readonly CodeFixStrategyConfiguration _configuration;
        
        public IEnumerable<CompilationErrorConfigurationModel> YourItems => _configuration.Configuration.OrderByDescending(s => s.IsActive);

        private string _pluginPath;
        public string ChoiceButtonText => _langManager.GetLang("Fix");
        public string ErrorsPluginText => _langManager.GetLang("ErrorsPlugin");
        public string BackText => _langManager.GetLang("Back");
        public string FixSelectionText => _langManager.GetLang("FixSelection");

        public ICommand ChoicePluginCommand { get; private set; }
        public ICommand RoslynPageOpenCommand { get; private set; }
        public ICommand BackCommand { get; private set; }

        private ObservableCollection<string> _errors = new ObservableCollection<string>();

        public ObservableCollection<string> Errors
        {
            get { return _errors; }
            set
            {
                if (_errors != value)
                {
                    _errors = value;
                    OnPropertyChanged("Errors");
                }
            }
        }

        public RoslynViewModel(PageManager pageManager, LangManager langManager, CodeFixManager codeFixManager, CodeFixStrategyConfiguration configuration)
        {
            _pageManager = pageManager;
            _langManager = langManager;
            _codeFixManager = codeFixManager;
            _configuration = configuration;

            ChoicePluginCommand = new RelayCommand(ChoicePluginCommandExecute);
            RoslynPageOpenCommand = new RelayCommand(RoslynPageOpenCommandExecute);
            BackCommand = new RelayCommand(BackCommandExecute);

            _langManager.Subscribe(OnLangChanged);

            _langManager.OnLangChangedInvoke();

            string path = GetPathOpenFileDialog();

            if (string.IsNullOrEmpty(path))
            {
                MessageBox.Show(_langManager.GetLang("NotSelectFile"));
                Task.Run(async () =>
                {
                    await Task.Delay(10);
                    TheardForm.Call(() => _pageManager.OpenChocePlugins());
                });
                return;
            }

            _pluginPath = path;

            var plugin = System.IO.File.ReadAllText(path);

            var syntaxTree = CSharpSyntaxTree.ParseText(plugin);

            _codeFixManager.Compilation(syntaxTree, RustReferenseManager.Path);

            Errors = new ObservableCollection<string>(_codeFixManager.GetErrors());

            foreach (var error in Errors)
            {
                foreach (var roslynError in _configuration.Configuration)
                {
                    if(Regex.IsMatch(error, roslynError.ErrorText))
                        roslynError.IsActive = true;
                }
            }
        }

        ~RoslynViewModel()
        {
            _langManager.UnSubscribe(OnLangChanged);
        }

        public void OnLangChanged(bool en)
        {
            OnPropertyChanged("ChoiceButtonText");
            OnPropertyChanged("ErrorsPluginText");
            OnPropertyChanged("BackText");
            OnPropertyChanged("FixSelectionText");
        }

        public void ChoicePluginCommandExecute(object obj)
        {
            var plugin = _codeFixManager.RunFix().ToString();

            var syntaxTree = CSharpSyntaxTree.ParseText(plugin);

            _codeFixManager.Compilation(syntaxTree, RustReferenseManager.Path);

            Errors = new ObservableCollection<string>(_codeFixManager.GetErrors());

            plugin = Regex.Replace(plugin, @"(\[Info\("".*"", "").*("", "".*""\)\])", "/*ПЛАГИН БЫЛ ПОФИКШЕН С ПОМОЩЬЮ ПРОГРАММЫ СКАЧАНОЙ С https://discord.gg/dNGbxafuJn */ $1https://discord.gg/dNGbxafuJn$2");

            plugin += "/* Boosty - https://boosty.to/skulidropek \n" +
                    "Discord - https://discord.gg/k3hXsVua7Q \n" +
                    "Discord The Rust Bay - https://discord.gg/Zq3TVjxKWk  */";

            foreach (var roslynError in _configuration.Configuration)
                roslynError.IsActive = false;

            foreach (var error in Errors)
            {
                foreach (var roslynError in _configuration.Configuration)
                {
                    if (Regex.IsMatch(error, roslynError.ErrorText))
                        roslynError.IsActive = true;
                }
            }

            OnPropertyChanged(nameof(YourItems));

            System.IO.File.WriteAllText(_pluginPath + "FIX.cs", plugin);
        }

        public void RoslynPageOpenCommandExecute(object obj)
        {
            _pageManager.OpenRoslyn();
        }

        private void BackCommandExecute(object obj)
        {
            _pageManager.OpenChocePlugins();
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
