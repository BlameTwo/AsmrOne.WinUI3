﻿using AsmrOne.WinUI3.Contracts;
using AsmrOne.WinUI3.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

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
        MainWindow = new Window();
        var page = ProgramLife.ServiceProvider.GetService<ShellPage>();
        page.titlebar.Window = MainWindow;
        MainWindow.Content = page;
        MainWindow.SystemBackdrop = new MicaBackdrop()
        {
            Kind = Microsoft.UI.Composition.SystemBackdrops.MicaKind.BaseAlt,
        };
        MainWindow.Activate();
    }

    public static Window MainWindow { get; private set; }
}
