using System.Windows;
using System.Windows.Controls;
using RustErrorsFix.Core;
using RustErrorsFix.Roslyn.Managers;
using RustErrorsFix.ViewModel;

namespace RustErrorsFix.View;

public partial class FriendsUserControl : UserControl
{
    public FriendsUserControl(LangManager langManager, PageManager pageManager)
    {
        InitializeComponent();
        DataContext = new FriendsViewModel(langManager, pageManager);
    }


    //public void UpdateLayout(bool en)
    //{
    //    TextBlockOurFriend.Text = _langManager.GetLang("OurFriends");
    //    BtnFriendsBack.Content = _langManager.GetLang("Back");
    //}

    private void BtnFriendsBack_Click(object sender, RoutedEventArgs e)
    {
        //MainWindow.Instance.FrameMain.Content = new ChoicePluginsUserControl()
        //{
        //    Width = Width,
        //    Height = Height
        //};
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        Process.Start("https://rustmods.ru/");
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        Process.Start("https://charaling-plugins.ru/");
    }

    private void Button_Click_2(object sender, RoutedEventArgs e)
    {
        Process.Start("https://rustplugins.top/");
    }

    private void Button_Click_3(object sender, RoutedEventArgs e)
    {
        Process.Start("https://whiteplugins.ru/");
    }
}