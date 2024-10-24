using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AsmrOne.WinUI3.Common.Bases;
using AsmrOne.WinUI3.Contracts;
using AsmrOne.WinUI3.Models;
using AsmrOne.WinUI3.Models.AsmrOne;
using AsmrOne.WinUI3.ViewModels.ItemViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace AsmrOne.WinUI3.ViewModels;

public sealed partial class HomeViewModel : ViewModelBase
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
        SelectOrder = Orders[0];
    }

    [ObservableProperty]
    ObservableCollection<QueryWorkOrderWrapper> orders;

    [ObservableProperty]
    QueryWorkOrderWrapper selectOrder;

    [ObservableProperty]
    bool? isSubtitle = false;

    [ObservableProperty]
    bool isLoading = false;

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
    ObservableCollection<DetilyItemViewModel> works = new();

    [ObservableProperty]
    int index = 1;

    [RelayCommand]
    async Task Loaded()
    {
        await this.AddItems();
    }

    [RelayCommand]
    async Task AddItems()
    {
        if (IsLoading)
            return;
        IsLoading = true;
        if (this.SelectOrder == null)
            return;
        var result = await AsmrClient.GetWorksAsync(
            this.SelectOrder.WordOrder,
            Index,
            IsSubtitle == null ? false : (bool)IsSubtitle,
            this.CTS.Token
        );
        foreach (var item in DataFactory.CreateDetilyItemViewModels(result.Works))
        {
            this.Works.Add(item);
        }
        this.Index++;
        IsLoading = false;
    }

    async Task RefreshAsync()
    {
        IsLoading = true;
        Index = 1;
        Works.Clear();
        var result = await AsmrClient.GetWorksAsync(
            this.SelectOrder.WordOrder,
            Index,
            IsSubtitle == null ? false : (bool)IsSubtitle,
            this.CTS.Token
        );
        foreach (var item in DataFactory.CreateDetilyItemViewModels(result.Works))
        {
            this.Works.Add(item);
        }
        this.Index++;
        IsLoading = false;
    }

    internal void Disponse()
    {
        this.IsLoading = true;
        this.Works.Clear();
        this.Orders.Clear();
        this.IsLoading = false;
    }
}
