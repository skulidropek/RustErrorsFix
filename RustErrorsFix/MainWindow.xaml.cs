using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.WebRequestMethods;

namespace RustErrorsFix
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<IErrorFixer> _errors = new List<IErrorFixer>()
        {
            new NetValueError(),
            new EntityListError(),
            new NetWrite(),
            new UpgradeError(),
            new CCTV_RCError(),
            new ItemContainerError()
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Open(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == false) 
            { 
                MessageBox.Show("Вы не выбрали файл");
                return; 
            }

            string path = openFileDialog.FileName;

            var plugin = System.IO.File.ReadAllText(path);

            foreach (var fixer in _errors)
            {
                plugin = fixer.Fix(plugin);
            }

            plugin = Regex.Replace(plugin, @"(\[Info\("".+"", "").+("", "".+""\)\])", "/*ПЛАГИН БЫЛ ПОФИКШЕН С ПОМОЩЬЮ ПРОГРАММЫ СКАЧАНОЙ С https://discord.gg/dNGbxafuJn */ $1https://discord.gg/dNGbxafuJn$2");

            System.IO.File.WriteAllText(path + "FIX.cs", plugin);
        }
        private void CloseUI(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        public List<string> dis = new List<string>()
        {
            "https://discord.gg/dNGbxafuJn",
        };

        public List<string> URL = new List<string>()
        {
            "https://boosty.to/skulidropek",
        };

        private void DS(object sender, RoutedEventArgs e)
        {
            foreach (var check in dis)
            {
                Process.Start(new ProcessStartInfo(check) { UseShellExecute = true });
            }   
        }

        private void Сайт(object sender, RoutedEventArgs e)
        {
            foreach (var check in URL)
            {
                Process.Start(new ProcessStartInfo(check) { UseShellExecute = true });
            }
        }
        
        private void BorderTopSection_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://discord.gg/nF6yZXQC7G") { UseShellExecute = true });
        }
    }
   
}
