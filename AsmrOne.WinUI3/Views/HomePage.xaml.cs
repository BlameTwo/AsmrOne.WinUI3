using System;
using AsmrOne.WinUI3.Common;
using AsmrOne.WinUI3.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace AsmrOne.WinUI3.Views;

public sealed partial class HomePage : Page, IPage
{
    public HomePage()
    {
        this.InitializeComponent();
        this.ViewModel = ProgramLife.GetService<HomeViewModel>();
    }

    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {
        this.Dispose();
    }

    public HomeViewModel ViewModel { get; private set; }

    public Type PageType => typeof(HomePage);

    public void Dispose()
    {
        this.ViewModel.Disponse();
        this.ViewModel = null;
        GC.Collect();
    }
}
