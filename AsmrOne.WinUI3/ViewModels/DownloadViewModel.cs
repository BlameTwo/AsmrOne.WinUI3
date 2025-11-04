using AsmrOne.Downloader.Contracts;
using AsmrOne.WinUI3.Common.Bases;
using AsmrOne.WinUI3.Contracts;
using AsmrOne.WinUI3.Contracts.Services;
using AsmrOne.WinUI3.ViewModels.ItemViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AsmrOne.WinUI3.ViewModels;

public sealed partial class DownloadViewModel:ViewModelBase
{
    public DownloadViewModel(IDownloaderManager downloaderManager,IAppSetup<App> appSetup)
    {
        DownloaderManager = downloaderManager;
        AppSetup = appSetup;
        Downloads = [];
    }

    public IDownloaderManager DownloaderManager { get; }
    public IAppSetup<App> AppSetup { get; }
    [ObservableProperty]
    public partial ObservableCollection<DownloadIItemViewModel> Downloads { get; set; }

    [RelayCommand]
    void Loaded()
    {
        RefreshData();
    }

    [RelayCommand]
    async Task DeleteSelect()
    {
        foreach (var item in this.Downloads)
        {
            if (item.IsSelect)
            {
                item.Dispose();
                await DownloaderManager.DeleteTaskAsync(item.DownloadKey);
            }
        }
        RefreshData();
    }

    [RelayCommand]
    private  void RefreshData()
    {
        if (Downloads.Count > 0)
        {
            foreach (var item in Downloads)
            {
                item.Dispose();
            }
        }
        Downloads.Clear();
        foreach (var item in this.DownloaderManager.DownloadSource)
        {
            this.Downloads.Add(new(item.Value,AppSetup.MainWindow.DispatcherQueue));
        }
        GC.Collect();
    }

    public override void Dispose()
    {
        if (Downloads.Count > 0)
        {
            foreach (var item in Downloads)
            {
                item.Dispose();
            }
        }
        Downloads.Clear();
        base.Dispose();
    }
}
