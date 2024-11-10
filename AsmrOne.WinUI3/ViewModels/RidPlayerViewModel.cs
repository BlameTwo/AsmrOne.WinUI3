using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AsmrOne.WinUI3.Common;
using AsmrOne.WinUI3.Common.Bases;
using AsmrOne.WinUI3.Contracts;
using AsmrOne.WinUI3.Models;
using AsmrOne.WinUI3.Models.AsmrOne;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AsmrOne.WinUI3.ViewModels;

public sealed partial class RidPlayerViewModel : ViewModelBase
{
    public IAudioPlayerService AudioPlayerService => ProgramLife.GetService<IAudioPlayerService>();

    public IAsmrClient AsmrClient => ProgramLife.GetService<IAsmrClient>();

    public IDataAdaptiveService DataAdaptiveService =>
        ProgramLife.GetService<IDataAdaptiveService>();

    public RidPlayerViewModel() { }

    [ObservableProperty]
    RidDetily detily;

    [ObservableProperty]
    ObservableCollection<IAudioDataWrapper> tracks;

    public async Task RefreshAsync()
    {
        if (AudioPlayerService.AudioManager.Detily == null)
        {
            return;
        }
        this.Detily = AudioPlayerService.AudioManager.Detily;
        this.Tracks = DataAdaptiveService.GetAudioData(
            (await AsmrClient.GetWorkAudioAsync(Detily.Id.ToString(), this.CTS.Token)).Item1,
            Detily
        );
    }
}
