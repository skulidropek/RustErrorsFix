using Microsoft.Extensions.DependencyInjection;
using RustErrorsFix.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RustErrorsFix.Core
{
    public class PageManager
    {
        private Frame _frame;
        private IServiceProvider _serviceProvider;

        public UserControl ActivePage => _frame == null ? new UserControl() : (UserControl)_frame.Content;

        public PageManager(IServiceProvider serviceProvider)
        {
           //_frame = mainWindow.FrameMain;
            _serviceProvider = serviceProvider;
        }

        public void Initilization(Frame frame)
        {
            if (_frame != null) return;

            _frame = frame;
        }

        public void OpenRoslyn()
        {
            _frame.Content = _serviceProvider.GetRequiredService<RoslynUserControl>();// new RoslynUserControl(_serviceProvider);
        }

        public void OpenFriends()
        {
            _frame.Content = _serviceProvider.GetRequiredService<FriendsUserControl>();// new FriendsUserControl();
        }

        public void OpenChocePlugins()
        {
            _frame.Content = _serviceProvider.GetRequiredService<ChoicePluginsUserControl>(); //new ChoicePluginsUserControl(this);
        }
    }
}
