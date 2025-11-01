using AsmrOne.Core;
using AsmrOne.Models.Enums;

namespace AsmrOne.Downloader.Models;

public interface IDownload
{

    public string DownloadKey { get;}

    public DownloadType Type { get; }

    public Task<bool> DownloadAsync(string rjId, IAsmrClient asmrClient);

    public Task<bool> StopAsync();

    public Task<bool> PauseAsync();

}
