using AsmrOne.Downloader.Models;
using AsmrOne.WinUI3.Common;
using AsmrOne.WinUI3.Common.Bases;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.WinUI;
using Microsoft.UI.Xaml;
using System;
using System.Threading.Tasks;

namespace AsmrOne.WinUI3.ViewModels.ItemViewModels;

public partial class DownloadIItemViewModel:ViewModelBase,IDisposable
{

    private readonly IDownload _download = null;
    private readonly Microsoft.UI.Dispatching.DispatcherQueue dispatcherQueue;

    [RelayCommand]
    async Task Loaded()
    {
        var args = await _download.GetDownloadStatus();
        SetArgs(args);
        if (args.IsAction == true && args.IsPause == false)
        {
            this.ActionIcon = PlayIcon;
        }
        else if (args.IsAction == false && args.IsPause == true)
        {
            this.ActionIcon = PauseIcon;
        }
    }
    public DownloadIItemViewModel(IDownload download, Microsoft.UI.Dispatching.DispatcherQueue dispatcherQueue)
    {
        _download = download;
        this.dispatcherQueue = dispatcherQueue;
        DownloadKey = download.DownloadKey;
        _download.DownloadChanged += _download_DownloadChanged;

        _download.DownloadCompleted += _download_DownloadCompleted;
        
    }

    private async Task _download_DownloadCompleted(object sender, DownloadArgs args)
    {
        await dispatcherQueue.EnqueueAsync(() =>
        {
            SetArgs(args);
        }).ConfigureAwait(false);
    }

    private async Task _download_DownloadChanged(object sender, DownloadArgs args)
    {
        await dispatcherQueue.EnqueueAsync(() =>
        {
            SetArgs(args);
        }).ConfigureAwait(false);
    }

    private void SetArgs(DownloadArgs args)
    {
        DownloadTotalSize = args.DownloadTotalSize;
        CurrentTotalSize = args.CurrentTotalSize;
        DownloadFileTotalSize = args.DownloadFileTotalSize;
        CurrentFileTotalSize = args.CurrentFileTotalSize;
        TotalCount = args.TotalCount;
        CurrentIndex = args.CurrentIndex;
        DownloadKey = args.DownloadKey;
        if (args.CoverImage != this.Cover)
        {
            this.Cover = args.CoverImage;
        }
        Title = args.Title;
        this.Progress = Math.Round((CurrentTotalSize / DownloadTotalSize) * 100,2);

        ShowCompletedBth(args.IsCompleted);
        ShowActiveButton(args);
    }

    private void ShowCompletedBth(bool isCompleted)
    {
        if (isCompleted)
        {
            this.CompletedVisibility = Visibility.Visible;
            this.DownloadingVisibility = Visibility.Collapsed;
        }
        else
        {
            this.CompletedVisibility = Visibility.Collapsed;
            this.DownloadingVisibility = Visibility.Visible;
        }
    }

    [RelayCommand]
    void ShowFolder()
    {
        WindowExtension.ShellExecute(IntPtr.Zero,"open",this._download.DownloadFolder,null,null,WindowExtension.SW_SHOWNORMAL);
    }

    private void ShowActiveButton(DownloadArgs args)
    {
        if (args.IsAction == true && args.IsPause == false)
        {
            this.ActionIcon = PauseIcon;
        }
        else if(args.IsAction == false && args.IsPause == true)
        {
            this.ActionIcon = PlayIcon;
        }
    }

    public void Dispose()
    {
        _download.DownloadChanged -= _download_DownloadChanged;

        _download.DownloadCompleted -= _download_DownloadCompleted;
    }

    [RelayCommand]
    async Task StartDownload()
    {
        var status = await _download.GetDownloadStatus();
        if(status.IsPause == false && status.IsAction == false)
        {
            await _download.StartDownloadAsync();
        }
        if(status.IsPause == true && status.IsAction == false)
        {
            await _download.ResumeAsync();
        }
        if(status.IsPause == false && status.IsAction == true)
        {
            await _download.PauseAsync();
            
        }
        SetArgs(status);
    }

    [RelayCommand]
    async Task Pause()
    {
        await _download.PauseAsync();
    }

    [RelayCommand]
    async Task Reset()
    {
        await _download.ResumeAsync();
    }

    [RelayCommand]
    async Task Stop()
    {
        await _download.StopAsync();
    }

    public const string PauseIcon = "\uE768";

    public const string PlayIcon = "\uE769";

    [ObservableProperty]
    public partial string ActionIcon { get; set; }

    [ObservableProperty]
    public partial bool IsSelect { get; set; } = false;

    [ObservableProperty]
    public partial double Progress { get; set; } = 0.0d;

    [ObservableProperty]
    public partial string DownloadKey { get; internal set; }

    [ObservableProperty]
    public partial Visibility DownloadingVisibility { get; internal set; }


    [ObservableProperty]
    public partial Visibility CompletedVisibility { get; internal set; }
    [ObservableProperty]
    public partial string Title { get; set; }

    /// <summary>
    /// 整个下载字节总量大小
    /// </summary>
    [ObservableProperty]
    public partial double DownloadTotalSize { get; set; }

    /// <summary>
    /// 整个当前字节下载总和
    /// </summary>
    [ObservableProperty]
    public partial double CurrentTotalSize { get; set; }


    /// <summary>
    /// 文件下载总量
    /// </summary>
    [ObservableProperty]
    public partial double DownloadFileTotalSize { get; set; }

    /// <summary>
    /// 当前文件下载下载量
    /// </summary>
    [ObservableProperty]
    public partial double CurrentFileTotalSize { get; set; }

    /// <summary>
    /// 当前文件数量
    /// </summary>
    [ObservableProperty]
    public partial long TotalCount { get; set; }

    /// <summary>
    /// 当前文件位置
    /// </summary>
    [ObservableProperty]
    public partial long CurrentIndex { get; set; }


    [ObservableProperty]
    public partial string Cover { get;  set; }
}
