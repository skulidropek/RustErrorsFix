using RustErrorsFix.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RustErrorsFix.Core
{
    class PageManager : Sigleton<PageManager>
    {
        private Frame _frame;

        public UserControl ActivePage => _frame == null ? new UserControl() : (UserControl)_frame.Content;

        public PageManager(Frame frame)
        {
            _frame = frame;
        }

        public void OpenRoslyn()
        {
            _frame.Content = new RoslynUserControl();
        }

        public void OpenFriends()
        {
            _frame.Content = new FriendsUserControl();
        }

        public void OpenChocePlugins()
        {
            _frame.Content = new ChoicePluginsUserControl();
        }
    }
}
