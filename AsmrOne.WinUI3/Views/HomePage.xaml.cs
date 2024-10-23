using System;
using AsmrOne.WinUI3.Common;
using AsmrOne.WinUI3.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace AsmrOne.WinUI3.Views;

public sealed partial class HomePage : Page, IPage
{
    public HomePage()
    {
        this.InitializeComponent();
        this.ViewModel = ProgramLife.ServiceProvider.GetService<HomeViewModel>();
    }

    public HomeViewModel ViewModel { get; }

    public Type PageType => typeof(HomePage);
}
