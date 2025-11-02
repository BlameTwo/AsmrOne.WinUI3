using AsmrOne.Core;
using AsmrOne.Models.Enums;

namespace AsmrOne.Downloader.Models;

public interface IDownload
{
    public event DownloadChangedDelegate DownloadChanged;

    public string DownloadKey { get;}

    public Task<bool> StartDownloadAsync();
    
    public Task<DownloadArgs> GetDownloadStatus();

    public DownloadType Type { get; }

    internal Task<bool> DownloadAsync(string rjId, IAsmrClient asmrClient);

    internal Task<bool> StopAsync();

    internal Task<bool> ResumeAsync();

    internal Task<bool> PauseAsync();

}
