using AsmrOne.WinUI3.Common;
using AsmrOne.WinUI3.Common.Bases;
using AsmrOne.WinUI3.Contracts;
using AsmrOne.WinUI3.Models;
using AsmrOne.WinUI3.Models.AsmrOne;
using AsmrOne.WinUI3.ViewModels.ItemViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace AsmrOne.WinUI3.ViewModels;

public sealed partial class PopularViewModel : PageDetilyViewModelBase
{
    public PopularViewModel(
        IDataAdaptiveService dataAdaptiveService,
        IDataFactory dataFactory,
        IAsmrClient asmrClient
    )
    {
        DataAdaptiveService = dataAdaptiveService;
        DataFactory = dataFactory;
        AsmrClient = asmrClient;
    }

    [ObservableProperty]
    public partial ObservableCollection<QueryWorkOrderWrapper> Orders { get; set; }

    [ObservableProperty]
    public partial QueryWorkOrderWrapper SelectOrder { get; set; }

    [ObservableProperty]
    public partial bool? IsSubtitle { get; set; } = false;

    async partial void OnIsSubtitleChanged(bool? value)
    {
        if (value == null)
            return;
        await this.RefreshAsync();
    }

    public IDataAdaptiveService DataAdaptiveService { get; }
    public IDataFactory DataFactory { get; }
    public IAsmrClient AsmrClient { get; }

    [ObservableProperty]
    public partial ObservableCollection<DetilyItemViewModel> Works { get; set; } = [];

    [RelayCommand]
    async Task Loaded()
    {
        Index = 1;
        await RefreshAsync();
    }

    [RelayCommand]
    async Task JumpToPageAsync(PagerControlSelectedIndexChangedEventArgs args)
    {
        this.Index = args.NewPageIndex + 1;
        await RefreshAsync();
    }

    internal void Disponse()
    {
        this.IsLoading = true;
        this.Works.Clear();
        this.Orders.Clear();
        this.IsLoading = false;
    }

    public override async Task Refreshing()
    {
        if (IsLoading)
            return;
        IsLoading = true;
        LoadingEnable = false;
        Works.Clear();
        var result = await AsmrClient.GetPopularAsync(
            IsSubtitle == null ? false : (bool)IsSubtitle,
            Index,
            PageSize,
            CTS.Token
        );
        if (result != null)
        {
            this.MaxPageSize = result.Pagination.TotalCount / PageSize;
            var data = DataFactory.CreateDetilyItemViewModels(result.Works);
            this.Works = data.ToObservable();
        }
        IsLoading = false;
        LoadingEnable = true;
    }
}
