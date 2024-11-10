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
    public IAsmrClient AsmrClient { get; }
    public IDataAdaptiveService DataAdaptiveService { get; }
    public IAudioPlayerService AudioPlayerService { get; }

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

    #region Progress
    [ObservableProperty]
    bool marked;

    [ObservableProperty]
    bool listening;

    [ObservableProperty]
    bool listened;

    [ObservableProperty]
    bool replay;

    [ObservableProperty]
    bool postponed;
    #endregion

    [RelayCommand]
    async Task SetReview(string review)
    {
        await AsmrClient.ReviewRidAsync(this.Detily.Id, review, this.CTS.Token);
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

    internal async Task SetDataAsync(string str)
    {
        var data = await AsmrClient.GetWorkAsync(str, this.CTS.Token);
        this.Detily = data.Item1;
        SetProgress();
        var track = await AsmrClient.GetWorkAudioAsync(str, this.CTS.Token);
        var result = DataAdaptiveService.GetAudioData(track.Item1, data.Item1);
        this.AudioDatas = result;
        this.Duration = TimeSpan.FromSeconds(Detily.Duration).ToString();
    }

    private void SetProgress()
    {
        if (this.Detily.Progress == null)
            return;
        if (Detily.Progress == "marked")
        {
            this.Marked = true;
            Listening = false;
            Listened = false;
            Replay = false;
            Postponed = false;
        }
        if (Detily.Progress == "listening")
        {
            this.Marked = false;
            Listening = true;
            Listened = false;
            Replay = false;
            Postponed = false;
        }
        if (Detily.Progress == "listened")
        {
            this.Marked = false;
            Listening = false;
            Listened = true;
            Replay = false;
            Postponed = false;
        }
        if (Detily.Progress == "replay")
        {
            this.Marked = false;
            Listening = false;
            Listened = false;
            Replay = true;
            Postponed = false;
        }
        if (Detily.Progress == "postponed")
        {
            this.Marked = false;
            Listening = false;
            Listened = false;
            Replay = false;
            Postponed = true;
        }
    }

    public override void Dispose()
    {
        if (AudioDatas != null)
        {
            foreach (var item in AudioDatas)
            {
                item.Dispose();
            }
        }
        this.AudioDatas.Clear();
        base.Dispose();
    }
}
