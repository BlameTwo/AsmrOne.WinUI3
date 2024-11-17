using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AsmrOne.WinUI3.Common;
using AsmrOne.WinUI3.Common.Bases;
using AsmrOne.WinUI3.Contracts;
using AsmrOne.WinUI3.Models.AsmrOne;
using AsmrOne.WinUI3.Models.ItemWrapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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
    ObservableCollection<SearchTagWrapper> tag = new();

    [ObservableProperty]
    ObservableCollection<SearchTagWrapper> cacheTag;


    [RelayCommand]
    async Task Loaded()
    {
        var result = await AsmrClient.GetTagAsync(this.CTS.Token);
        var circle = await AsmrClient.GetCirclesAsync(this.CTS.Token);
        var vas = await AsmrClient.GetVasAsync(this.CTS.Token);
        this.CacheTag = result.ToTags().Concat(circle.ToCircles()).Concat(vas.ToVas()).ToObservable();
        SourceTags = CacheTag.ToObservable();
    }

    [RelayCommand]
    async Task Search(string keyword)
    {
        var result = AsmrClient.SearchAsync(keyword, this.Tag, false);
    }

    internal void SearchTag(string queryText) { }
}
