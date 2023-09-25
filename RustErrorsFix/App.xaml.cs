using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RustErrorsFix.Core;
using RustErrorsFix.Roslyn.Managers;
using RustErrorsFix.View;
using RustErrorsFix.ViewModel;
using RustErrorsFixLibrary.Core;
using RustErrorsFixLibrary.Core.CodeFixStrategys;
using RustErrorsFixLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RustErrorsFix
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;


        public App()
        {
            // Настройка контейнера DI
            var services = RustErrorsFixLibrary.Program.CreateBuilder()
                .AddSingleton<PageManager>()
                .AddSingleton<LangManager>()
                .AddSingleton<MainWindow>()
                .AddTransient<ChoicePluginsUserControl>()
                .AddTransient<RoslynUserControl>()
                .AddTransient<FriendsUserControl>()
            ;

           _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _serviceProvider.GetService<MainWindow>()?.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // Освобождение ресурсов
            if (_serviceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }

            base.OnExit(e);
        }
    }
}
