using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AsmrOne.WinUI3.Common.Bases;
using AsmrOne.WinUI3.Contracts;
using AsmrOne.WinUI3.Models;
using AsmrOne.WinUI3.Models.AsmrOne;
using AsmrOne.WinUI3.Models.Messagers.ItemMessangers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace AsmrOne.WinUI3.ViewModels;

public sealed partial class RidDetilyViewModel : ViewModelBase
{
    public RidDetilyViewModel(
        IAsmrClient asmrClient,
        IDataAdaptiveService dataAdaptiveService,
        IAudioPlayerService audioPlayerService
    )
    {
        AsmrClient = asmrClient;
        DataAdaptiveService = dataAdaptiveService;
        AudioPlayerService = audioPlayerService;
        RegisterMessager();
    }

    private void RegisterMessager()
    {
        this.Messenger.Register<RidDetilySendPlayAudio>(this, RidDetilySendPlayAudioMethod);
    }

    private void RidDetilySendPlayAudioMethod(object recipient, RidDetilySendPlayAudio message)
    {
        AudioPlayerService.Player(message.Audio, this.Detily);
    }

    [ObservableProperty]
    RidDetily detily;

    [ObservableProperty]
    string duration;

    [ObservableProperty]
    ObservableCollection<IAudioDataWrapper> audioDatas;

    public IAsmrClient AsmrClient { get; }
    public IDataAdaptiveService DataAdaptiveService { get; }
    public IAudioPlayerService AudioPlayerService { get; }

    internal async Task SetDataAsync(string str)
    {
        var data = await AsmrClient.GetWorkAsync(str, this.CTS.Token);
        this.Detily = data.Item1;
        var track = await AsmrClient.GetWorkAudioAsync(str, this.CTS.Token);
        var result = DataAdaptiveService.GetAudioData(track.Item1, data.Item1);
        this.AudioDatas = result;
        this.Duration = TimeSpan.FromSeconds(Detily.Duration).ToString();
    }

    public override void Dispose()
    {
        foreach (var item in AudioDatas)
        {
            item.Dispose();
        }
        this.AudioDatas.Clear();
        base.Dispose();
    }
}
