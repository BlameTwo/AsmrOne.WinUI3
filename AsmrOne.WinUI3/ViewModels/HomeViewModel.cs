
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

public sealed partial class HomeViewModel : PageDetilyViewModelBase
{
    public HomeViewModel(
        IDataAdaptiveService dataAdaptiveService,
        IDataFactory dataFactory,
        IAsmrClient asmrClient
    )
    {
        DataAdaptiveService = dataAdaptiveService;
        DataFactory = dataFactory;
        AsmrClient = asmrClient;
        this.Orders = WorkOrderExtensions.GetWorkOrders();
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

    async partial void OnSelectOrderChanged(QueryWorkOrderWrapper value)
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
    void Loaded()
    {
        Index = 1;
        SelectOrder = Orders[0];
    }

    [RelayCommand]
    async Task JumpToPageAsync(PagerControlSelectedIndexChangedEventArgs args)
    {
        this.Index = args.NewPageIndex + 1;
        if (this.SelectOrder == null)
            return;
        await RefreshAsync();
    }

    internal void Disponse()
    {
        this.IsLoading = true;
        this.Works.Clear();
        this.Orders.Clear();
        this.IsLoading = false;
    }

    public async override Task Refreshing()
    {
        if (IsLoading)
            return;
        IsLoading = true;
        LoadingEnable = false;
        Works.Clear();
        var result = await AsmrClient.GetWorksAsync(
            this.SelectOrder.WordOrder,
            Index,
            PageSize,
            IsSubtitle == null ? false : (bool)IsSubtitle,
            this.CTS.Token
        );
        if (result != null)
        {
            this.MaxPageSize = result.Pagination.TotalCount / PageSize;
            var data = DataFactory.CreateDetilyItemViewModels(result.Works);
            this.Works = data.Where(x => x.IsNTFS == GlobalUsing.IsHideR18 == true ? false : true)
                .ToObservable();
        }
        IsLoading = false;
        LoadingEnable = true;
    }
}
