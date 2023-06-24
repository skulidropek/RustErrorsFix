using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        };
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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

            System.IO.File.WriteAllText(path + "FIX.cs", plugin);
        }
    }
}
