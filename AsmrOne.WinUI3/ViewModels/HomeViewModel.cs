using System.Collections.ObjectModel;
using System.Runtime.InteropServices.Marshalling;
using System.Threading.Tasks;
using AsmrOne.WinUI3.Common;
using AsmrOne.WinUI3.Contracts;
using AsmrOne.WinUI3.Models;
using AsmrOne.WinUI3.Models.AsmrOne;
using AsmrOne.WinUI3.ViewModels.ItemViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace AsmrOne.WinUI3.ViewModels;

public sealed partial class HomeViewModel : ObservableObject
{
    public HomeViewModel(IDataAdaptiveService dataAdaptiveService, IDataFactory dataFactory)
    {
        DataAdaptiveService = dataAdaptiveService;
        DataFactory = dataFactory;
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
        //IsLoading = true;
        //var result = await ProgramLife
        //    .ServiceProvider.GetService<IAsmrClient>()
        //    .GetWorksAsync(
        //        this.SelectOrder.WordOrder,
        //        Index,
        //        IsSubtitle == null ? false : (bool)IsSubtitle
        //    );
        //foreach (var item in DataFactory.CreateDetilyItemViewModels(result.Works))
        //{
        //    this.Works.Add(item);
        //}
        //this.Index++;
        //IsLoading = false;
    }

    async Task RefreshAsync()
    {
        IsLoading = true;
        Index = 1;
        Works.Clear();
        var result = await ProgramLife
            .ServiceProvider.GetService<IAsmrClient>()
            .GetWorksAsync(
                this.SelectOrder.WordOrder,
                Index,
                IsSubtitle == null ? false : (bool)IsSubtitle
            );
        foreach (var item in DataFactory.CreateDetilyItemViewModels(result.Works))
        {
            this.Works.Add(item);
        }
        this.Index++;
        IsLoading = false;
    }
}
