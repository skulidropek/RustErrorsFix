using Microsoft.Win32;
using RustErrorsFix.View;
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
    public static MainWindow Instance { get; private set; }

    public MainWindow()
    {
        InitializeComponent();

        Instance = this;

        new Lang().OnLangChanged += (lang) =>
        {
            if (!lang)
            {
                LangImageEn.Visibility = Visibility.Visible;
                LangImageRu.Visibility = Visibility.Collapsed;
            }
            else
            {
                LangImageEn.Visibility = Visibility.Collapsed;
                LangImageRu.Visibility = Visibility.Visible;

            }


            if(ChoicePluginsUserControl.Instanse != null && FrameMain.Content == ChoicePluginsUserControl.Instanse)
                ChoicePluginsUserControl.Instanse.UpdateLayout();

            if (FriendsUserControl.Instanse != null && FrameMain.Content == FriendsUserControl.Instanse)
                FriendsUserControl.Instanse.UpdateLayout();
        };

        Lang.Instance.OnLangChangedInvoke();

        FrameMain.Content = new ChoicePluginsUserControl()
        {
            Width = Width,
            Height = Height
        };
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

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        Process.Start(new ProcessStartInfo("https://discord.gg/nF6yZXQC7G") { UseShellExecute = true });
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        FrameMain.Content = new FriendsUserControl()
        {
            Width = Width,
            Height = Height
        };
    }

    private void Button_Click_2(object sender, RoutedEventArgs e)
    {
        Process.Start(new ProcessStartInfo("https://www.youtube.com/@skulidropek607") { UseShellExecute = true });
    }

    private void Button_Click_3(object sender, RoutedEventArgs e)
    {
        Process.Start(new ProcessStartInfo("https://discord.gg/CBqDuqDWvS") { UseShellExecute = true });
    }

    private void Button_Click_4(object sender, RoutedEventArgs e)
    {
        Process.Start(new ProcessStartInfo("https://boosty.to/skulidropek") { UseShellExecute = true });
    }

    private void Button_Click_5(object sender, RoutedEventArgs e)
    {
        Lang.Instance.ChangeLang();
    }
}