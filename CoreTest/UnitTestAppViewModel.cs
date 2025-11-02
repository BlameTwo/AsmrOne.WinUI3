using AsmrOne.Core;
using AsmrOne.Downloader.Contracts;
using AsmrOne.Downloader.Services;
using AsmrOne.Downloader.ViewModels;
using AsmrOne.WinUI3.Models.AsmrOne;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CoreTest;

public partial class UnitTestAppViewModel:ObservableObject
{
    IAsmrClient _client = new AsmrClient();
    IDownloaderManager _manager;
    public UnitTestAppViewModel()
    {
        _client.RegisterClient("asmr.one");
        _manager = new DownloaderManager(_client);
        _manager.DownloadBasePath = "D:\\ASMRDownload";
        this.Downloads = [];
    }

    [ObservableProperty]
    public partial ObservableCollection<RidDetily> Works { get; set; }

    [ObservableProperty]
    public partial ObservableCollection<DownloadIItemViewModel> Downloads { get; set; }

    [RelayCommand]
    async Task Loaded()
    {
       
        this.Works =[..(await _client.GetWorksAsync(AsmrOne.WinUI3.Models.AsmrOne.WorkOrder.CreateNew, 1, 10, true)).Works];
        await this._manager.CreateDownloaderAsync(Works[0].Id.ToString(), AsmrOne.Models.Enums.DownloadType.RJ);
        
    }

    [RelayCommand]
    void RefreshList()
    {
        ClearList();
        foreach (var item in _manager.DownloadSource)
        {
            Downloads?.Add(new DownloadIItemViewModel(item.Value, UnitTestApp.Window.DispatcherQueue));
        }
    }

    [RelayCommand]
    void ClearList()
    {
        if(Downloads != null && Downloads.Count > 0)
        {
            foreach (DownloadIItemViewModel download in Downloads)
            {
                download.Dispose();
            }
        }
        Downloads?.Clear();
    }
}
