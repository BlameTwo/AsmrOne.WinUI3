using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AsmrOne.WinUI3.Common.Bases;
using AsmrOne.WinUI3.Contracts;
using AsmrOne.WinUI3.Models;
using AsmrOne.WinUI3.Models.AsmrOne;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AsmrOne.WinUI3.ViewModels;

public sealed partial class RidDetilyViewModel : ViewModelBase
{
    public RidDetilyViewModel(IAsmrClient asmrClient, IDataAdaptiveService dataAdaptiveService)
    {
        AsmrClient = asmrClient;
        DataAdaptiveService = dataAdaptiveService;
    }

    [ObservableProperty]
    RidDetily detily;

    [ObservableProperty]
    string duration;

    [ObservableProperty]
    ObservableCollection<IAudioDataWrapper> audioDatas;

    public IAsmrClient AsmrClient { get; }
    public IDataAdaptiveService DataAdaptiveService { get; }

    internal async Task SetDataAsync(string str)
    {
        var data = await AsmrClient.GetWorkAsync(str);
        this.Detily = data.Item1;
        var track = await AsmrClient.GetWorkAudioAsync(str);
        var result = DataAdaptiveService.GetAudioData(track.Item1, data.Item1);
        this.AudioDatas = result;
        this.Duration = TimeSpan.FromSeconds(Detily.Duration).ToString();
    }
}
