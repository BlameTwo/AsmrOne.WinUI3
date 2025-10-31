using AsmrOne.WinUI3.Common;
using AsmrOne.WinUI3.Common.Bases;
using AsmrOne.WinUI3.Contracts;
using AsmrOne.WinUI3.Contracts.Services;
using AsmrOne.WinUI3.Models.AsmrOne;
using AsmrOne.WinUI3.Models.ItemWrapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.WinUI.Controls;
using System;
using AsmrOne.Core.Common;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace AsmrOne.WinUI3.ViewModels;

public sealed partial class SearchViewModel : ViewModelBase
{
    public SearchViewModel(IAsmrClient asmrClient)
    {
        AsmrClient = asmrClient;
    }

    public IAsmrClient AsmrClient { get; }

    public ObservableCollection<SearchTagWrapper> SourceTags { get; set; }

    [ObservableProperty]
    public partial bool IsLoading { get; set; }

    [ObservableProperty]
    public partial ObservableCollection<SearchTagWrapper> Tag { get; set; } = new();

    [ObservableProperty]
    public partial ObservableCollection<TagType> TagTypes { get; set; } = TagType.Default;

    [ObservableProperty]
    public partial TagType SelectTagType { get; set; }
    [ObservableProperty]
    public partial ObservableCollection<SearchTagWrapper> CacheTag { get; set; }


    async partial void OnSelectTagTypeChanged(TagType value)
    {
        var result = await AsmrClient.GetTagAsync(value, this.CTS.Token);
        this.CacheTag = result.ToTags(value.Memory).ToObservable();
        SourceTags = CacheTag.ToObservable();
    }

    [RelayCommand]
    void Loaded()
    {
        this.SelectTagType = TagTypes[0];
    }

    [RelayCommand]
    void TokenAdding(TokenItemAddingEventArgs args)
    {
        args.Item = this.CacheTag.FirstOrDefault(x => x.DisplayName.Contains(args.TokenText));
        if(args.Item == null)
        {
            args.Item = new SearchTagWrapper()
            {
                DisplayName = args.TokenText,
                Name = args.TokenText,
                Type = "keyword"
            };
        }
    }

    [RelayCommand]
    async Task Search()
    {
        var result = AsmrClient.SearchAsync(this.Tag,WorkOrder.CreateNew,1,20, false);
    }


    [RelayCommand]
    async Task AddItems()
    {
        
    }
}
