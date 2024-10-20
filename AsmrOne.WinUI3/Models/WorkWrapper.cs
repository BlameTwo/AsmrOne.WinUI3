using AsmrOne.WinUI3.Contracts;
using AsmrOne.WinUI3.Models.AsmrOne;
using AsmrOne.WinUI3.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace AsmrOne.WinUI3.Models;

public partial class WorkWrapper : ObservableObject
{
    [ObservableProperty]
    RidDetily data;

    [RelayCommand]
    void Invoke()
    {
        ProgramLife
            .ServiceProvider.GetKeyedService<INavigationService>(ProgramLife.ShellNavigationKey)
            .NavigationTo<RidDetilyViewModel>(this.Data.Id.ToString());
    }
}
