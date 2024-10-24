using System;
using System.Collections.Generic;
using System.IO;
using AsmrOne.WinUI3.Common;
using AsmrOne.WinUI3.Contracts;
using AsmrOne.WinUI3.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using static AsmrOne.WinUI3.Common.Win32;

namespace AsmrOne.WinUI3;

public partial class App : Application
{
    public App()
    {
        ProgramLife.InitService();
        this.InitializeComponent();
    }

    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        InitSetting();
        MainWindow = new Window();
        var page = ProgramLife.ServiceProvider.GetService<ShellPage>();
        page.titlebar.Window = MainWindow;
        MainWindow.Content = page;
        MainWindow.SystemBackdrop = new MicaBackdrop();
        MainWindow.Activate();
        WindowExtension.CreateTransparentWindow(
            WindowExtension.CreateType.Subtitle,
            ProgramLife.ServiceProvider.GetService<SubtitleWindow>()
        );
    }

    private void InitSetting() { }

    public static Window MainWindow { get; private set; }

    public static Window SubtitleWindow { get; private set; }
}
