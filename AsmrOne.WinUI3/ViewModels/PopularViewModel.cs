using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AsmrOne.WinUI3.Common;
using AsmrOne.WinUI3.Common.Bases;
using AsmrOne.WinUI3.Contracts;
using AsmrOne.WinUI3.ViewModels.ItemViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AsmrOne.WinUI3.ViewModels;

public sealed partial class PopularViewModel : ViewModelBase
{
    public PopularViewModel(IAsmrClient asmrClient, IDataFactory dataFactory)
    {
        AsmrClient = asmrClient;
        DataFactory = dataFactory;
        Index = 1;
        this.IsSubtitle = false;
    }

    [ObservableProperty]
    int index;
    public IAsmrClient AsmrClient { get; }
    public IDataFactory DataFactory { get; }

    [ObservableProperty]
    bool isLoading;

    [ObservableProperty]
    ObservableCollection<DetilyItemViewModel> works = new();

    [ObservableProperty]
    bool isSubtitle;

    [RelayCommand]
    async Task Loaded()
    {
        await RefreshAsync();
    }

    [RelayCommand]
    async Task AddItems()
    {
        IsLoading = true;
        var result = await AsmrClient.GetPopularAsync(
            this.IsSubtitle,
            this.Index,
            token: this.CTS.Token
        );
        var data = DataFactory
            .CreateDetilyItemViewModels(result.Works)
            .Where(x => x.IsNTFS == GlobalUsing.IsHideR18 == true ? false : true)
            .ToObservable();
        foreach (var item in data)
        {
            this.Works.Add(item);
        }
        this.Index++;
        IsLoading = false;
    }

    async partial void OnIsSubtitleChanged(bool value)
    {
        await RefreshAsync();
    }

    async Task RefreshAsync()
    {
        IsLoading = true;
        Index = 1;
        Works.Clear();
        var result = await AsmrClient.GetPopularAsync(
            this.IsSubtitle,
            this.Index,
            token: this.CTS.Token
        );
        var data = DataFactory.CreateDetilyItemViewModels(result.Works);
        this.Works = data.Where(x => x.IsNTFS == GlobalUsing.IsHideR18 == true ? false : true)
            .ToObservable();
        this.Index++;
        IsLoading = false;
    }
}
