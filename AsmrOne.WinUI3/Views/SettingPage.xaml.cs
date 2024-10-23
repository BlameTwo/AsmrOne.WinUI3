using System;
using AsmrOne.WinUI3.Common;
using AsmrOne.WinUI3.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace AsmrOne.WinUI3.Views;

public sealed partial class SettingPage : Page, IPage
{
    public SettingPage()
    {
        this.InitializeComponent();
        this.ViewModel = ProgramLife.ServiceProvider.GetService<SettingViewModel>();
    }

    public SettingViewModel ViewModel { get; }

    public Type PageType => typeof(SettingPage);
}
