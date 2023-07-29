using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using RustErrorsFix.Roslyn.Managers;

namespace RustErrorsFix.View;

public partial class FriendsUserControl : UserControl
{
    public static FriendsUserControl Instanse;

    public FriendsUserControl()
    {
        InitializeComponent();
        Instanse = this;
        LangManager.Subscribe(UpdateLayout);
        UpdateLayout();
    }

    ~FriendsUserControl()
    {
        LangManager.UnSubscribe(UpdateLayout);
    }

    public void UpdateLayout(bool en)
    {
        TextBlockOurFriend.Text = LangManager.GetLang("OurFriends");
        BtnFriendsBack.Content = LangManager.GetLang("Back");
    }

    private void BtnFriendsBack_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.Instance.FrameMain.Content = new ChoicePluginsUserControl()
        {
            Width = Width,
            Height = Height
        };
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        Process.Start(new ProcessStartInfo("https://rustmods.ru/") { UseShellExecute = true });
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        Process.Start(new ProcessStartInfo("https://charaling-plugins.ru/") { UseShellExecute = true });
    }

    private void Button_Click_2(object sender, RoutedEventArgs e)
    {
        Process.Start(new ProcessStartInfo("https://rustplugins.top/") { UseShellExecute = true });
    }

    private void Button_Click_3(object sender, RoutedEventArgs e)
    {
        Process.Start(new ProcessStartInfo("https://whiteplugins.ru/") { UseShellExecute = true });
    }
}