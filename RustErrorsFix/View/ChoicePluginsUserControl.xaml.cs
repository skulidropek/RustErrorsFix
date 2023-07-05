using Microsoft.Win32;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace RustErrorsFix.View;

public partial class ChoicePluginsUserControl : UserControl
{
    public static ChoicePluginsUserControl Instanse;

    private List<IErrorFixer> _errors = new List<IErrorFixer>()
    {
        new NetValueError(),
        new EntityListError(),
        new NetWrite(),
        new UpgradeError(),
        new CCTV_RCError(),
        new ItemContainerError(),
        new UsingError(),
        new UnexpectedTokenError()
    };

    public ChoicePluginsUserControl()
    {
        InitializeComponent();
        Instanse = this;
        UpdateLayout();
    }

    public void UpdateLayout()
    {
        BtnChoice.Content = Lang.Instance.GetLang("Select");
        SelectPluginTextBlock.Text = Lang.Instance.GetLang("SelectPlugin");
    }

    private void BtnChoice_Click(object sender, RoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();

        if (openFileDialog.ShowDialog() == false)
        {
            MessageBox.Show(Lang.Instance.GetLang("NotSelectFile"));
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
        MessageBox.Show(Lang.Instance.GetLang("PluginReady") + " Path - " + path + "FIX.cs");
    }
}