using AsmrOne.Downloader.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.WinUI;
using Microsoft.UI.Dispatching;
using Windows.System;

namespace AsmrOne.Downloader.ViewModels;

public partial class DownloadIItemViewModel:ObservableObject,IDisposable
{

    private readonly IDownload _download = null;
    private readonly Microsoft.UI.Dispatching.DispatcherQueue dispatcherQueue;

    [RelayCommand]
    async Task Loaded()
    {
        var args = await _download.GetDownloadStatus();
        DownloadTotalSize = args.TotalSize;
        CurrentTotalSize = args.CurrentSize;
        DownloadFileTotalSize = args.FileSize;
        CurrentFileTotalSize = args.CurrentSize;
        TotalCount = args.FileTotal;
        CurrentIndex = args.FileIndex;
        DownloadKey = args.Title;
    }
    public DownloadIItemViewModel(IDownload download, Microsoft.UI.Dispatching.DispatcherQueue dispatcherQueue)
    {
        this._download = download;
        this.dispatcherQueue = dispatcherQueue;
        this.DownloadKey = download.DownloadKey;
        _download.DownloadChanged += _download_DownloadChanged;
        
    }

    private async Task _download_DownloadChanged(object sender, DownloadArgs args)
    {
        await dispatcherQueue.EnqueueAsync(() =>
        {
            DownloadTotalSize = args.TotalSize;
            CurrentTotalSize = args.CurrentSize;
            DownloadFileTotalSize = args.FileSize;
            CurrentFileTotalSize = args.CurrentSize;
            TotalCount = args.FileTotal;
            CurrentIndex = args.FileIndex;
            DownloadKey = args.Title;
        }).ConfigureAwait(false);
    }

    public void Dispose()
    {
        _download.DownloadChanged -= _download_DownloadChanged;
    }

    [RelayCommand]
    async Task StartDownload()
    {
        await _download.StartDownloadAsync();
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

    [ObservableProperty]
    public partial bool IsSelect { get; set; } = false;

    [ObservableProperty]
    public partial string DownloadKey { get; internal set; }


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
    public partial long DownloadFileTotalSize { get; set; }

    /// <summary>
    /// 当前文件下载下载量
    /// </summary>
    [ObservableProperty]
    public partial long CurrentFileTotalSize { get; set; }

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
}
