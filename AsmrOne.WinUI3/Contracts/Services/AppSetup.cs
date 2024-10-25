using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsmrOne.WinUI3.Common;
using AsmrOne.WinUI3.Common.NotifyIcon;
using AsmrOne.WinUI3.Views;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using WinUIEx;

namespace AsmrOne.WinUI3.Contracts.Services
{
    public class AppSetup<T> : IAppSetup<T>
        where T : AsmrApplication
    {
        public NotifyIconWinUI NotifyIcon { get; private set; }

        public Window MainWindow { get; private set; }
        public T App { get; private set; }
        public SubtitleWindowBase SubWindow { get; private set; }

        public void DisponseSubtitle()
        {
            SubWindow.Dispose();
            SubWindow.Close();
            this.SubWindow = null;
            GC.Collect();
        }

        public void RegisterApp(T app)
        {
            this.App = app;
        }

        public void RegisterSubtitleWindow(SubtitleWindowBase window)
        {
            this.SubWindow = window;
        }

        public void Start()
        {
            MainWindow = new Window();
            var page = ProgramLife.ServiceProvider.GetService<ShellPage>();
            page.titlebar.Window = MainWindow;
            MainWindow.Content = page;
            MainWindow.SystemBackdrop = new MicaBackdrop();
            MainWindow.AppWindow.Closing += AppWindow_Closing;
            MainWindow.Activate();
            NotifyIcon = new();
            NotifyIcon.CreateTrayIcon(
                AppDomain.CurrentDomain.BaseDirectory + "\\Assets\\icon_256x256.ico",
                this.MainWindow
            );
            NotifyIcon.RegisterWin(MainWindow);
            NotifyIcon.ContextMenu = new NotifyIconMenu()
            {
                Items = new System.Collections.Generic.List<NotifyIconMenuItem>()
                {
                    new NotifyIconMenuItem()
                    {
                        Header = "显示\\隐藏 主屏幕",
                        Command = new RelayCommand(() => SwitchMain()),
                    },
                    new NotifyIconMenuItem()
                    {
                        Header = "退出程序",
                        Command = new RelayCommand(() => Process.GetCurrentProcess().Kill()),
                    },
                },
            };
            NotifyIcon.LeftClick += NotifyIcon_LeftClick;
        }

        private void NotifyIcon_LeftClick(object sender, EventArgs args)
        {
            this.SwitchMain();
        }

        private void SwitchMain()
        {
            if (this.MainWindow.Visible)
            {
                this.MainWindow.Hide();
            }
            else
            {
                this.MainWindow.Show();
            }
        }

        private void AppWindow_Closing(
            Microsoft.UI.Windowing.AppWindow sender,
            Microsoft.UI.Windowing.AppWindowClosingEventArgs args
        )
        {
            args.Cancel = true;
            this.MainWindow.Hide();
        }
    }
}
