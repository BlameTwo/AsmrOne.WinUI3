using System.Collections.ObjectModel;
using System.Linq;
using AsmrOne.WinUI3.Common;
using AsmrOne.WinUI3.Common.Bases;
using AsmrOne.WinUI3.Contracts;
using AsmrOne.WinUI3.Models.AsmrOne;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace AsmrOne.WinUI3.ViewModels.ItemViewModels;

public sealed partial class DetilyItemViewModel : ViewModelBase, IItemViewModel<RidDetily>
{
    [ObservableProperty]
    string cover;

    [ObservableProperty]
    long id;

    [ObservableProperty]
    string title;

    [ObservableProperty]
    string _name;

    [ObservableProperty]
    string money;

    [ObservableProperty]
    ObservableCollection<Tag> tags;

    public void SetData(RidDetily value)
    {
        this.Cover = value.MainCoverUrl;
        this.Id = value.Id;
        this.Title = value.Title;
        this.Name = value.Name;
        this.Money = value.Price.ToString();
        if (value.Tags.Count < 4)
        {
            this.Tags = value.Tags.ToObservable();
        }
        else
        {
            this.Tags = value.Tags.Take(4).ToObservable();
        }
    }

    [RelayCommand]
    void Invoke()
    {
        ProgramLife
            .ServiceProvider.GetKeyedService<INavigationService>(ProgramLife.ShellNavigationKey)
            .NavigationTo<RidDetilyViewModel>(this.Id.ToString());
    }
}
