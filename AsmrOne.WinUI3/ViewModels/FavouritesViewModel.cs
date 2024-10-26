using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AsmrOne.WinUI3.Common;
using AsmrOne.WinUI3.Common.Bases;
using AsmrOne.WinUI3.Contracts;
using AsmrOne.WinUI3.Models;
using AsmrOne.WinUI3.Models.Enums;
using AsmrOne.WinUI3.ViewModels.ItemViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace AsmrOne.WinUI3.ViewModels;

public sealed partial class FavouritesViewModel : ViewModelBase
{
    public FavouritesViewModel(IAsmrClient asmrClient, IDataFactory dataFactory)
    {
        AsmrClient = asmrClient;
        DataFactory = dataFactory;
        this.Sorts = FarouritesExtension.GetSort();
        this.SelectSort = Sorts[0];
        this.Orders = FarouritesExtension.GetOrder();
        this.SelectOrder = Orders[0];
        this.Fillters = FarouritesExtension.GetFillters();
        this.SelectFillter = Fillters[0];
    }

    [ObservableProperty]
    SelectorBarItem selectTabItem;

    private int index = 1;

    async partial void OnSelectTabItemChanged(SelectorBarItem value)
    {
        switch (value.Tag.ToString())
        {
            case "Comment":
                this.SelectFillter = Fillters.Last();
                FillterVisibility = Visibility.Collapsed;

                break;
            case "Progress":
                this.SelectFillter = Fillters.First();
                FillterVisibility = Visibility.Visible;
                break;
        }
        await RefreshAsync();
    }

    [ObservableProperty]
    ObservableCollection<QuerySortFavouritesWrapper> sorts;

    [ObservableProperty]
    QuerySortFavouritesWrapper selectSort;

    [ObservableProperty]
    ObservableCollection<QueryOrderFavouritesWrapper> orders;

    [ObservableProperty]
    QueryOrderFavouritesWrapper selectOrder;

    [ObservableProperty]
    ObservableCollection<QueryFillterFavouritesWrapper> fillters;

    [ObservableProperty]
    QueryFillterFavouritesWrapper selectFillter;

    [ObservableProperty]
    Visibility fillterVisibility;

    [ObservableProperty]
    ObservableCollection<DetilyItemViewModel> works = new();

    [ObservableProperty]
    bool isLoading = false;

    public IAsmrClient AsmrClient { get; }
    public IDataFactory DataFactory { get; }

    async partial void OnSelectOrderChanged(QueryOrderFavouritesWrapper value)
    {
        if (value == null)
            return;
        await RefreshAsync();
    }

    async partial void OnSelectSortChanged(QuerySortFavouritesWrapper value)
    {
        if (value == null)
            return;
        await RefreshAsync();
    }

    async partial void OnSelectFillterChanged(QueryFillterFavouritesWrapper value)
    {
        if (value == null)
            return;
        if (value.Filter == MyFarouritesFilter.None)
            return;
        await RefreshAsync();
    }

    [RelayCommand]
    async Task AddItems()
    {
        if (IsLoading)
            return;
        IsLoading = true;
        if (
            this.SelectTabItem == null
            || this.SelectOrder == null
            || this.SelectOrder == null
            || this.SelectFillter == null
        )
            return;
        var result = await AsmrClient.GetMyFavouritesAsync(
            this.SelectTabItem.Tag.ToString() == "Comment"
                ? FavouritesType.Comment
                : FavouritesType.Progress,
            this.SelectOrder.Code,
            this.SelectSort.Code,
            this.SelectFillter.Filter,
            this.index
        );
        foreach (var item in DataFactory.CreateDetilyItemViewModels(result.Works))
        {
            this.Works.Add(item);
        }
        this.IsLoading = false;
    }

    async Task RefreshAsync()
    {
        IsLoading = true;
        this.index = 1;
        Works.Clear();
        if (
            this.SelectTabItem == null
            || this.SelectOrder == null
            || this.SelectOrder == null
            || this.SelectFillter == null
        )
            return;
        var result = await AsmrClient.GetMyFavouritesAsync(
            this.SelectTabItem.Tag.ToString() == "Comment"
                ? FavouritesType.Comment
                : FavouritesType.Progress,
            this.SelectOrder.Code,
            this.SelectSort.Code,
            this.SelectFillter.Filter,
            this.index
        );
        var data = DataFactory.CreateDetilyItemViewModels(result.Works);
        this.Works = data.Where(x => x.IsNTFS == GlobalUsing.IsHideR18 == true ? false : true)
            .ToObservable();
        this.index++;
        IsLoading = false;
    }

    public override void Dispose()
    {
        this.IsLoading = true;
        this.Works.Clear();
        this.Sorts.Clear();
        this.Orders.Clear();
        this.Fillters.Clear();
        this.Works = null;
        this.IsLoading = true;
        base.Dispose();
    }
}
