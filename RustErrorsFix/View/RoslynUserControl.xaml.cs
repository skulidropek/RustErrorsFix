using Microsoft.Win32;
using RustErrorsFix.Legasy;
using RustErrorsFix.Roslyn.Managers;
using RustErrorsFix.ViewModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace RustErrorsFix.View;

public partial class RoslynUserControl : UserControl
{
    public RoslynUserControl()
    {
        InitializeComponent();
        DataContext = new RoslynViewModel();
    }
}