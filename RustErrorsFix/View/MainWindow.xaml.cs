using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using RustErrorsFix.Core;
using RustErrorsFix.Roslyn.Managers;
using RustErrorsFix.View;
using RustErrorsFix.ViewModel;
using RustErrorsFixLibrary.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RustErrorsFix;

public partial class MainWindow : Window
{
    public MainWindow(PageManager pageManager, LangManager langManager)
    {
        InitializeComponent();
        pageManager.Initilization(FrameMain);
        DataContext = new MainWindowViewModel(pageManager, langManager);
    }

    private void CloseUI(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void BorderTopSection_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
            this.DragMove();
    }
}