using System.Collections.ObjectModel;
using System.Runtime.InteropServices.Marshalling;
using System.Threading.Tasks;
using AsmrOne.WinUI3.Contracts;
using AsmrOne.WinUI3.Models;
using AsmrOne.WinUI3.Models.AsmrOne;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace AsmrOne.WinUI3.ViewModels;

public sealed partial class HomeViewModel : ObservableObject
{
    public HomeViewModel(IDataAdaptiveService dataAdaptiveService)
    {
        DataAdaptiveService = dataAdaptiveService;
        this.Orders = WorkOrderExtensions.GetWorkOrders();
        SelectOrder = Orders[0];
    }

    [ObservableProperty]
    ObservableCollection<QueryWorkOrderWrapper> orders;

    [ObservableProperty]
    QueryWorkOrderWrapper selectOrder;

    [ObservableProperty]
    bool? isSubtitle = false;

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

    [ObservableProperty]
    int index = 1;

    [RelayCommand]
    async Task Loaded()
    {
        var result = await ProgramLife
            .ServiceProvider.GetService<IAsmrClient>()
            .GetWorksAsync(
                this.SelectOrder.WordOrder,
                Index,
                this.IsSubtitle == null ? false : (bool)IsSubtitle
            );
        foreach (var item in result.Works)
        {
            this.Works.Add(new WorkWrapper() { Data = item });
        }
        this.Index++;
    }

    [RelayCommand]
    async Task AddItems()
    {
        var result = await ProgramLife
            .ServiceProvider.GetService<IAsmrClient>()
            .GetWorksAsync(
                this.SelectOrder.WordOrder,
                Index,
                IsSubtitle == null ? false : (bool)IsSubtitle
            );
        foreach (var item in result.Works)
        {
            this.Works.Add(new WorkWrapper() { Data = item });
        }
        this.Index++;
    }

    async Task RefreshAsync()
    {
        Index = 1;
        Works.Clear();
        var result = await ProgramLife
            .ServiceProvider.GetService<IAsmrClient>()
            .GetWorksAsync(
                this.SelectOrder.WordOrder,
                Index,
                IsSubtitle == null ? false : (bool)IsSubtitle
            );
        foreach (var item in result.Works)
        {
            this.Works.Add(new WorkWrapper() { Data = item });
        }
        this.Index++;
    }

    [ObservableProperty]
    ObservableCollection<WorkWrapper> works = new();
}
