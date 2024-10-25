using System;
using System.Diagnostics;
using AsmrOne.WinUI3.Common;
using AsmrOne.WinUI3.Common.NotifyIcon;
using AsmrOne.WinUI3.Contracts;
using AsmrOne.WinUI3.Views;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace AsmrOne.WinUI3;

public partial class App : AsmrApplication
{
    public App()
    {
        ProgramLife.InitService();
        this.InitializeComponent();
        this.UnhandledException += App_UnhandledException;
    }

    private void App_UnhandledException(
        object sender,
        Microsoft.UI.Xaml.UnhandledExceptionEventArgs e
    )
    {
        e.Handled = true;
    }

    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        ProgramLife.GetService<IAppSetup<App>>().RegisterApp(this);
        ProgramLife.GetService<IAppSetup<App>>().Start();
    }
}
