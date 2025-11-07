using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AsmrOne.Downloader.Contracts;
using AsmrOne.Downloader.Models;
using AsmrOne.WinUI3.Common;
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
    public IDownloaderManager DownloaderManager { get; }
    public ITipShow TipShow { get; }
    public IAudioPlayerService AudioPlayerService { get; }

    public RidDetilyViewModel(
        IAsmrClient asmrClient,
        IDataAdaptiveService dataAdaptiveService,
        IDownloaderManager downloaderManager,
        ITipShow tipShow,
        IAudioPlayerService audioPlayerService
    )
    {
        AsmrClient = asmrClient;
        DataAdaptiveService = dataAdaptiveService;
        DownloaderManager = downloaderManager;
        TipShow = tipShow;
        AudioPlayerService = audioPlayerService;
        RegisterMessager();
    }

    #region Progress
    [ObservableProperty]
    public partial bool Marked { get; set; }

    [ObservableProperty]
    public partial bool Listening { get; set; }

    [ObservableProperty]
    public partial bool Listened { get; set; }

    [ObservableProperty]
    public partial bool Replay { get; set; }

    [ObservableProperty]
    public partial bool Postponed { get; set; }
    #endregion

    [RelayCommand]
    async Task SetReview(string review)
    {
        await AsmrClient.ReviewRidAsync(this.Detily.Id, review, this.CTS.Token);
    }

    private void RegisterMessager()
    {
        this.Messenger.Register<RidDetilySendPlayAudio>(this, RidDetilySendPlayAudioMethod);
        this.Messenger.Register<DownloadSingleFile>(this, DownloadSingleFileMethod);
    }

    private async void DownloadSingleFileMethod(object recipient, DownloadSingleFile message)
    {
        var downloadKey = await DownloaderManager.CreateDownloaderAsync(
            message.DownloadFile,
            AsmrOne.Models.Enums.DownloadType.File
        );
        switch (downloadKey)
        {
            case DownloadErrorCode.Success:
                TipShow.ShowMessage(
                    $"任务创建成功，请在下载页面查看",
                    Microsoft.UI.Xaml.Controls.Symbol.Accept
                );
                break;
            case DownloadErrorCode.MaxTaskError:
                TipShow.ShowMessage(
                    "最多可以创建2个同时下载任务",
                    Microsoft.UI.Xaml.Controls.Symbol.Clear
                );
                break;
            case DownloadErrorCode.OwnerError:
                TipShow.ShowMessage("其他错误", Microsoft.UI.Xaml.Controls.Symbol.Clear);
                break;
        }
    }

    private void RidDetilySendPlayAudioMethod(object recipient, RidDetilySendPlayAudio message) { }

    [ObservableProperty]
    RidDetily detily;

    [ObservableProperty]
    public partial string Duration { get; set; }

    [ObservableProperty]
    public partial ObservableCollection<IAudioDataWrapper> AudioDatas { get; set; }

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

    [RelayCommand]
    void SetPlayerList()
    {
        AudioPlayerService.SetDetily(this.Detily,GetAudios());
        AudioPlayerService.OnPlayerInit();
    }

    public List<AudioWrapper> GetAudios()
    {
        var audios = new List<AudioWrapper>();
        foreach (var item in this.AudioDatas)
        {
            if (item.Type == "Folder")
            {
                audios.AddRange(FindFolder(item));
            }
            if (item.Type == "Audio")
            {
                audios.Add((AudioWrapper)item);
            }
        }
        return audios;
    }

    public List<AudioWrapper> FindFolder(IAudioDataWrapper child)
    {
        List<AudioWrapper> audios = new List<AudioWrapper>();
        if (child is FolderWrapper folder) 
        {
            foreach (var item in folder.Datas)
            {
                if(child.Type == "Folder")
                {
                    audios.AddRange(FindFolder(item));
                }
                if(child.Type == "Audio")
                {
                    audios.Add((AudioWrapper)item);
                }
            }
        }
        else if (child is AudioWrapper audioWrapper)
        {
            audios.Add(audioWrapper);
        }
        return audios;
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
        if (AudioDatas != null && AudioDatas.Count > 0)
        {
            foreach (var item in AudioDatas)
            {
                item.Dispose();
            }
            this.AudioDatas.Clear();
        }
        base.Dispose();
    }

    [RelayCommand]
    public async Task CreateDownload()
    {
        var downloadKey = await DownloaderManager.CreateDownloaderAsync(
            this.Detily.Id.ToString(),
            AsmrOne.Models.Enums.DownloadType.RJ
        );
        switch (downloadKey)
        {
            case DownloadErrorCode.Success:
                TipShow.ShowMessage(
                    $"任务创建成功，请在下载页面查看",
                    Microsoft.UI.Xaml.Controls.Symbol.Accept
                );
                break;
            case DownloadErrorCode.MaxTaskError:
                TipShow.ShowMessage(
                    "最多可以创建2个同时下载任务",
                    Microsoft.UI.Xaml.Controls.Symbol.Clear
                );
                break;
            case DownloadErrorCode.OwnerError:
                TipShow.ShowMessage("其他错误", Microsoft.UI.Xaml.Controls.Symbol.Clear);
                break;
        }
    }
}
