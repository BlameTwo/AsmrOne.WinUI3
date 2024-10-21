using AsmrOne.WinUI3.Common;
using AsmrOne.WinUI3.Contracts;
using AsmrOne.WinUI3.Models.AsmrOne;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace AsmrOne.WinUI3.ViewModels.ItemViewModels;

public sealed partial class DetilyItemViewModel : ObservableObject, IItemViewModel<RidDetily>
{
    [ObservableProperty]
    string cover;

    [ObservableProperty]
    long id;

    [ObservableProperty]
    string title;

    public void SetData(RidDetily value)
    {
        this.Cover = value.MainCoverUrl;
        this.Id = value.Id;
        this.Title = value.Title;
    }

    [RelayCommand]
    void Invoke()
    {
        ProgramLife
            .ServiceProvider.GetKeyedService<INavigationService>(ProgramLife.ShellNavigationKey)
            .NavigationTo<RidDetilyViewModel>(this.Id.ToString());
    }
}
