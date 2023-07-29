using Microsoft.Win32;
using Ookii.Dialogs.Wpf;
using Roslyn_test.Abstract;
using Roslyn_test.Factory;
using RustErrorsFix.Core;
using RustErrorsFix.Core.Factory;
using RustErrorsFix.Model;
using RustErrorsFix.Roslyn.Managers;
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
        private string _pluginPath;
        public string ChoiceButtonText => LangManager.GetLang("Fix");
        public string ErrorsPluginText => LangManager.GetLang("ErrorsPlugin");
        public string BackText => LangManager.GetLang("Back");
        public string FixSelectionText => LangManager.GetLang("FixSelection");

        public ObservableCollection<RoslynErrorModel> YourItems { get; set; } = new ObservableCollection<RoslynErrorModel> {
            new Model.RoslynErrorModel()
            {
                Text = @"не удается преобразовать из ""uint"" в ""ItemContainerId""",
                IsAnalise = false,
                Errors = new List<AbstractFactory>() { new FindContainerFactory() }
            },
            new Model.RoslynErrorModel()
            {
                Text = @"не удается преобразовать из "".+"" в ""uint""",
                IsAnalise = false,
                Errors = new List<AbstractFactory>() { new ReplaceUInt32ToUInt64Factory(), new ReplaceIDToIDValueFactory(), new ReplaceUidToUidValueFactory() }
            },
            new Model.RoslynErrorModel()
            {
                Text = @"Не удается неявно преобразовать тип ""(NetworkableId)?(ItemId)?(ItemContainerId)?"" в ""(uint)?(ulong)?""",
                IsAnalise = true,
                Errors = new List<AbstractFactory>() { new ReplaceIDToIDValueFactory() }
            },
            new Model.RoslynErrorModel()
            {
                Text = @"Ни одна из перегрузок метода ""(Factor)?(Test)?(GetWaterDepth)?(GetOverallWaterDepth)?(GetWaterInfo)?"" не принимает \d аргументов",
                IsAnalise = true,
                Errors = new List<AbstractFactory>() { new WaterLevelFactory() }
            },
            new Model.RoslynErrorModel()
            {
                Text = @".+\(.+\)"": не все пути к коду возвращают значение.",
                IsAnalise = true,
                Errors = new List<AbstractFactory>() { new CrashCodeFactory() }
            }
        };

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

        private RoslynPluginFixerAbstractFactory Roslyn;


        public RoslynViewModel()
        {
            ChoicePluginCommand = new RelayCommand(ChoicePluginCommandExecute);
            RoslynPageOpenCommand = new RelayCommand(RoslynPageOpenCommandExecute);
            BackCommand = new RelayCommand(BackCommandExecute);

            LangManager.Subscribe(OnLangChanged);

            LangManager.OnLangChangedInvoke();

            string path = GetPathOpenFileDialog();

            if (string.IsNullOrEmpty(path))
            {
                MessageBox.Show(LangManager.GetLang("NotSelectFile"));
                Task.Run(async () =>
                {
                    await Task.Delay(10);
                    TheardForm.Call(() => PageManager.Instance.OpenChocePlugins());
                });
                return;
            }

            _pluginPath = path;

            var plugin = System.IO.File.ReadAllText(path);

            Roslyn = new RoslynPluginFixerAbstractFactory(YourItems.ToList());
            Errors = new ObservableCollection<string>(Roslyn.GetErrors(plugin));

            foreach (var error in Errors)
            {
                foreach (var roslynError in YourItems)
                {
                    if (Regex.IsMatch(error, roslynError.Text))
                    {
                        roslynError.IsActive = true;
                    }
                }
            }
        }

        // = new RoslynPluginFixerAbstractFactory(YourItems.ToList());

        ~RoslynViewModel()
        {
            LangManager.UnSubscribe(OnLangChanged);
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
            Roslyn = new RoslynPluginFixerAbstractFactory(YourItems.ToList());
            var plugin = System.IO.File.ReadAllText(_pluginPath);

            plugin = Roslyn.FixPlugin(plugin);

            plugin = Regex.Replace(plugin, @"(\[Info\("".+"", "").+("", "".+""\)\])", "/*ПЛАГИН БЫЛ ПОФИКШЕН С ПОМОЩЬЮ ПРОГРАММЫ СКАЧАНОЙ С https://discord.gg/dNGbxafuJn */ $1https://discord.gg/dNGbxafuJn$2");

            System.IO.File.WriteAllText(_pluginPath + "FIX.cs", plugin);

            Errors = new ObservableCollection<string>(Roslyn.GetErrors(plugin));
        }

        public void RoslynPageOpenCommandExecute(object obj)
        {
            PageManager.Instance.OpenRoslyn();
        }

        private void BackCommandExecute(object obj)
        {
            PageManager.Instance.OpenChocePlugins();
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
