using System;
using AsmrOne.WinUI3.Common;
using AsmrOne.WinUI3.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace AsmrOne.WinUI3.Views;

public sealed partial class FavouritesPage : Page, IPage
{
    public FavouritesPage()
    {
        this.InitializeComponent();

        this.ViewModel = ProgramLife.GetService<FavouritesViewModel>();
    }

    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {
        this.Dispose();
        base.OnNavigatedFrom(e);
    }

    public void Dispose()
    {
        ViewModel.Dispose();
        GC.Collect();
    }

    public FavouritesViewModel ViewModel { get; private set; }

    public Type PageType => typeof(FavouritesPage);
}
