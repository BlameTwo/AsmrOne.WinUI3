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
        this.ViewModel = ProgramLife.GetService<RidDetilyViewModel>();
        this.treeView.DragItemsStarting += TreeView_DragItemsStarting;
    }

    public RidDetilyViewModel ViewModel { get; private set; }

    public Type PageType => typeof(RidDetilyPage);

    public void Dispose()
    {
        this.ViewModel.Dispose();
    }

    protected override async void OnNavigatedTo(NavigationEventArgs e)
    {
        if (e.Parameter is string str)
        {
            await ViewModel.SetDataAsync(str);
        }
        base.OnNavigatedFrom(e);
    }

    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {
        this.Dispose();
        this.treeView.DragItemsStarting -= TreeView_DragItemsStarting;
        this.ViewModel = null;
        GC.Collect();
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
