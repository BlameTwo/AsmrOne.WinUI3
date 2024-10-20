using System.Text.Json;
using System.Threading.Tasks;
using AsmrOne.WinUI3.Common;
using AsmrOne.WinUI3.Common.Bases;
using AsmrOne.WinUI3.Contracts;
using AsmrOne.WinUI3.Models;
using AsmrOne.WinUI3.Models.AsmrOne;
using AsmrOne.WinUI3.Models.Messagers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Windows.Storage;

namespace AsmrOne.WinUI3.ViewModels.DialogViewModels;

public sealed partial class RegisterViewModel : DialogViewModelBase
{
    [ObservableProperty]
    string hostName;

    [ObservableProperty]
    string password;

    [ObservableProperty]
    string tipText;

    public RegisterViewModel(IDialogManager dialogManager)
        : base(dialogManager) { }

    [RelayCommand]
    async Task Regsiter()
    {
        var result = await ProgramLife
            .ServiceProvider.GetService<IAsmrClient>()
            .RegisterAsync(HostName, Password);
        if (result.Item1 != null)
        {
            ProgramLife.ServiceProvider.GetService<IAsmrClient>().SetToken(result.Item1);
            WeakReferenceMessenger.Default.Send<RefreshToken>(new(true));
            TokenInstance.SaveToken(result.Item1);
            this.Close();
        }
        else
        {
            result = await ProgramLife
                .ServiceProvider.GetService<IAsmrClient>()
                .LoginAsync(HostName, Password);
            ProgramLife.ServiceProvider.GetService<IAsmrClient>().SetToken(result.Item1);
            WeakReferenceMessenger.Default.Send<RefreshToken>(new(true));
            TokenInstance.SaveToken(result.Item1);
            this.Close();
        }
    }
}
