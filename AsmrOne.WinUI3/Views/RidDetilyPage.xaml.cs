using System;
using AsmrOne.WinUI3.Common;
using AsmrOne.WinUI3.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace AsmrOne.WinUI3.Views;

public sealed partial class RidDetilyPage : Page, IPage
{
    public RidDetilyPage()
    {
        this.InitializeComponent();
        this.ViewModel = ProgramLife.ServiceProvider.GetService<RidDetilyViewModel>();
    }

    public RidDetilyViewModel ViewModel { get; }

    public Type PageType => typeof(RidDetilyPage);

    protected override async void OnNavigatedTo(NavigationEventArgs e)
    {
        if (e.Parameter is string str)
        {
            await ViewModel.SetDataAsync(str);
        }
        base.OnNavigatedFrom(e);
    }

    private void TreeView_DragItemsStarting(
        TreeView sender,
        TreeViewDragItemsStartingEventArgs args
    )
    {
        args.Cancel = true;
    }
}
