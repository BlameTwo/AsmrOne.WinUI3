using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using System.Threading.Tasks;

namespace AsmrOne.WinUI3.Common.Bases;

public abstract partial class PageDetilyViewModelBase:ViewModelBase
{
    [ObservableProperty]
    public partial bool IsLoading { get; set; } = false;


    [ObservableProperty]
    public partial int Index { get; set; }

    [ObservableProperty]
    public partial int PageSize { get; set; } = 20;

    [ObservableProperty]
    public partial int MaxPageSize { get; set; } = 2;

    [ObservableProperty]
    public partial bool LoadingEnable { get; set; } = true;

    public abstract Task Refreshing();

    [RelayCommand]
    public async Task RefreshAsync()
    {
        await Refreshing();
    }


    [RelayCommand]
    async Task NextItem()
    {
        Index++;
        await RefreshAsync();
    }

    [RelayCommand]
    async Task ForwardItem()
    {
        if (Index == 1)
            return;
        Index--;
        await RefreshAsync();
    }

}
