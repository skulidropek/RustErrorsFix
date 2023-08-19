using Microsoft.Win32;
using RustErrorsFix.Core;
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

public partial class ChoicePluginsUserControl : UserControl
{
    public ChoicePluginsUserControl(PageManager pageManager, LangManager langManager)
    {
        InitializeComponent();

        DataContext = new ChoicePluginsViewModel(pageManager, langManager);
    }
}