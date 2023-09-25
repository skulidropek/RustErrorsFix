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

        public List<CompilationErrorConfigurationModel> YourItems => _configuration.Configuration;

        private string _pluginPath;
        public string ChoiceButtonText => _langManager.GetLang("Fix");
        public string ErrorsPluginText => _langManager.GetLang("ErrorsPlugin");
        public string BackText => _langManager.GetLang("Back");
        public string FixSelectionText => _langManager.GetLang("FixSelection");

        //public ObservableCollection<CompilationErrorConfigurationModel> YourItems { get; set; } => _configuration.Configuration;
            //= new ObservableCollection<CompilationErrorConfigurationModel> {
            //new Model.RoslynErrorModel()
            //{
            //    Text = @"не удается преобразовать из ""uint"" в ""ItemContainerId""",
            //    IsAnalise = false,
            //    Errors = new List<AbstractFactory>() { new FindContainerFactory() }
            //},

        //new Model.RoslynErrorModel()
        //{
        //    Text = @"не удается преобразовать из ""uint"" в ""(NetworkableId)|(ItemId)|(ItemContainerId)""",
        //    IsAnalise = false,
        //    Errors = new List<AbstractFactory>() { new ReplaceUInt32ToUInt64Factory() }
        //},

        //new Model.RoslynErrorModel()
        //{
        //    Text = @"не удается преобразовать из ""(NetworkableId)|(ItemId)|(ItemContainerId)"" в ""uint""",
        //    IsAnalise = false,
        //    Errors = new List<AbstractFactory>() { new ReplaceUInt32ToUInt64Factory(), new ReplaceIDToIDValueFactory(), new ReplaceUidToUidValueFactory() }
        //},
        //new Model.RoslynErrorModel()
        //{
        //    Text = @"Оператор ""\?\?"" невозможно применить к операнду типа ""(NetworkableId)|(ItemId)|(ItemContainerId)|\??"" и ""(int)|(uint)|(ulong)""",
        //    IsAnalise = true,
        //    Errors = new List<AbstractFactory>() { new ReplaceIDToIDValueFactory(), new ReplaceUidToUidValueFactory() }
        //},
        //new Model.RoslynErrorModel()
        //{
        //    Text = @"не удается преобразовать из ""(uint)|(ulong)"" в ""(NetworkableId)|(ItemId)|(ItemContainerId)""",
        //    IsAnalise = true,
        //    Errors = new List<AbstractFactory>() { new CanMoveItemFactory(), new ReplaceUidToUidValueFactory(), new UInt64ToNetworkabledIdFactory() }
        //},
        //new Model.RoslynErrorModel()
        //{
        //    Text = @"Не удается неявно преобразовать тип ""(NetworkableId)|(ItemId)|(ItemContainerId)"" в ""(uint)|(ulong)""",
        //    IsAnalise = true,
        //    Errors = new List<AbstractFactory>() { new ReplaceIDToIDValueFactory() }
        //},
        //new Model.RoslynErrorModel()
        //{
        //    Text = @"Ни одна из перегрузок метода ""(Factor)|(Test)|(GetWaterDepth)|(GetOverallWaterDepth)|(GetWaterInfo)"" не принимает \d аргументов",
        //    IsAnalise = true,
        //    Errors = new List<AbstractFactory>() { new WaterLevelFactory() }
        //},
        //new Model.RoslynErrorModel()
        //{
        //    Text = @"Отсутствует аргумент, соответствующий требуемому параметру ""waves"" из ""WaterLevel\.(Factor)|(Test)|(GetWaterDepth)|(GetOverallWaterDepth)|(GetWaterInfo)",
        //    IsAnalise = true,
        //    Errors = new List<AbstractFactory>() { new WaterLevelFactory() }
        //},
        //new Model.RoslynErrorModel()
        //{
        //    Text = @"Отсутствует аргумент, соответствующий требуемому параметру ""altMove"" из ""BasePlayer.GetIdealContainer\(BasePlayer, Item, bool\)""",
        //    IsAnalise = true,
        //    Errors = new List<AbstractFactory>() { new GetIdealContainerFactory() }
        //},
        //new Model.RoslynErrorModel()
        //{
        //    Text = @".+\(.+\)"": не все пути к коду возвращают значение.",
        //    IsAnalise = true,
        //    Errors = new List<AbstractFactory>() { new CrashCodeFactory() }
        //},
        //new Model.RoslynErrorModel()
        //{
        //    Text = @"""ItemCraftTask"" не содержит определения ""owner"".+",
        //    IsAnalise = true,
        //    Errors = new List<AbstractFactory>() { new ItemCraftTask() }
        //},
        //new Model.RoslynErrorModel()
        //{
        //    Text = @"""SpawnPopulationBase"" не содержит определения ""_targetDensity""",
        //    IsAnalise = true,
        //    Errors = new List<AbstractFactory>() { new TargetDensityReplace() }
        //},
        //new Model.RoslynErrorModel()
        //{
        //    Text = @"""BuildingBlock"" не содержит определения ""GetGrade"", и не удалось найти доступный метод расширения ""GetGrade"", принимающий тип ""BuildingBlock"" в качестве первого аргумента \(возможно, пропущена директива using или ссылка на сборку\)",
        //    IsAnalise = true,
        //    Errors = new List<AbstractFactory>() { new UpgradeFactory() }
        //},    
        //new Model.RoslynErrorModel()
        //{
        //    Text = @""".+.EquipWeapon\(\)"": не найден метод, пригодный для переопределения",
        //    IsAnalise = true,
        //    Errors = new List<AbstractFactory>() { new EquipWeaponFactory() }
        //},
        //new Model.RoslynErrorModel()
        //{
        //    Text = @"""ListHashSet<BasePlayer>"" не содержит определения ""ForEach"", и не удалось найти доступный метод расширения ""ForEach"", принимающий тип ""ListHashSet<BasePlayer>"" в качестве первого аргумента \(возможно, пропущена директива using или ссылка на сборку\)",
        //    IsAnalise = true,
        //    Errors = new List<AbstractFactory>() { new ListHashSetFactory() }
        //},  
        //new Model.RoslynErrorModel()
        //{
        //    Text = @"""Quaternion"" не содержит определение для ""ID""",
        //    IsAnalise = true,
        //    Errors = new List<AbstractFactory>() { new QuatrionIDFactory() }
        //},
        //};

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
