using AsmrOne.Core;
using AsmrOne.Models.Enums;

namespace AsmrOne.Downloader.Models;

public interface IDownload:IAsyncDisposable
{
    public event DownloadChangedDelegate DownloadChanged;
    public event DownloadCompletedDelegate DownloadCompleted;
    public string TaskName { get; }

    public string DownloadFolder { get; }
    public string Cover { get; }

    public string DownloadBase { get;  }
    public string ErrorMessage { get; }

    public bool IsCompleted { get; }
    public bool IsError { get; }
    public object Description { get; }


    public string DownloadKey { get;}

    public Task<bool> StartDownloadAsync();
    
    public Task<DownloadArgs> GetDownloadStatus();

    public DownloadType Type { get; }

    public Task<bool> DownloadAsync(object rjId, IAsmrClient asmrClient);

    public Task<bool> StopAsync();

    public Task<bool> ResumeAsync();

    public Task<bool> PauseAsync();
}
