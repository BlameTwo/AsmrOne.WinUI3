using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AsmrOne.Core.Common;
using AsmrOne.WinUI3.Common;
using AsmrOne.WinUI3.Common.Bases;
using AsmrOne.WinUI3.Contracts;
using AsmrOne.WinUI3.Contracts.Services;
using AsmrOne.WinUI3.Models;
using AsmrOne.WinUI3.Models.AsmrOne;
using AsmrOne.WinUI3.Models.ItemWrapper;
using AsmrOne.WinUI3.ViewModels.ItemViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.WinUI.Controls;
using Microsoft.UI.Xaml.Controls;

namespace AsmrOne.WinUI3.ViewModels;

public sealed partial class SearchViewModel : PageDetilyViewModelBase
{
    public SearchViewModel(IAsmrClient asmrClient, IDataFactory dataFactory)
    {
        AsmrClient = asmrClient;
        DataFactory = dataFactory;
        this.Orders = WorkOrderExtensions.GetWorkOrders();
    }

    public IAsmrClient AsmrClient { get; }
    public IDataFactory DataFactory { get; }
    public ObservableCollection<SearchTagWrapper> SourceTags { get; set; }

    [ObservableProperty]
    public partial bool IsLoading { get; set; }

    [ObservableProperty]
    public partial ObservableCollection<SearchTagWrapper> Tag { get; set; } = new();

    [ObservableProperty]
    public partial ObservableCollection<TagType> TagTypes { get; set; } = TagType.Default;

    [ObservableProperty]
    public partial ObservableCollection<QueryWorkOrderWrapper> Orders { get; set; }

    [ObservableProperty]
    public partial QueryWorkOrderWrapper SelectOrder { get; set; }

    [ObservableProperty]
    public partial TagType SelectTagType { get; set; }

    [ObservableProperty]
    public partial ObservableCollection<SearchTagWrapper> CacheTag { get; set; }

    [ObservableProperty]
    public partial bool? IsSubtitle { get; set; } = false;

    [ObservableProperty]
    public partial ObservableCollection<DetilyItemViewModel> Works { get; set; } = [];

    async partial void OnSelectTagTypeChanged(TagType value)
    {
        if (value == null)
            return;
        if (value.Memory == "durations")
        {
            ObservableCollection<SearchTagWrapper> values = TagType.GetDurationsTags();
            this.CacheTag = values;
            SourceTags = CacheTag;
        }
        else
        {
            var result = await AsmrClient.GetTagAsync(value, this.CTS.Token);
            this.CacheTag = result.ToTags(value.Memory).ToObservable();
            SourceTags = CacheTag;
        }
    }

    [RelayCommand]
    void Loaded()
    {
        this.SelectTagType = TagTypes[0];
        this.SelectOrder = Orders[0];
    }

    [RelayCommand]
    void TokenAdding(TokenItemAddingEventArgs args)
    {
        args.Item = this.CacheTag.FirstOrDefault(x => x.DisplayName.Contains(args.TokenText));
        if (args.Item == null)
        {
            args.Item = new SearchTagWrapper()
            {
                DisplayName = args.TokenText,
                Name = args.TokenText,
                Type = "keyword",
            };
        }
    }

    [RelayCommand]
    async Task Search()
    {
        await RefreshAsync();
    }

    [RelayCommand]
    async Task JumpToPageAsync(PagerControlSelectedIndexChangedEventArgs args)
    {
        this.Index = args.NewPageIndex + 1;
        if (this.SelectOrder == null)
            return;
        await RefreshAsync();
    }

    public override async Task Refreshing()
    {
        var result = await AsmrClient.SearchAsync(
            this.Tag,
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
            this.Works = data.ToObservable();
        }
        IsLoading = false;
        LoadingEnable = true;
    }
}
