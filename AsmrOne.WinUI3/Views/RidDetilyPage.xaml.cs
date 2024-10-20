using AsmrOne.WinUI3.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace AsmrOne.WinUI3.Views;

public sealed partial class RidDetilyPage : Page
{
    public RidDetilyPage()
    {
        this.InitializeComponent();
        this.ViewModel = ProgramLife.ServiceProvider.GetService<RidDetilyViewModel>();
    }

    public RidDetilyViewModel ViewModel { get; }

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
