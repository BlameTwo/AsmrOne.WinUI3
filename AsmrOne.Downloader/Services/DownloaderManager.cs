using AsmrOne.Core;
using AsmrOne.Downloader.Common;
using AsmrOne.Downloader.Contracts;
using AsmrOne.Downloader.Models;
using AsmrOne.Models.Enums;

namespace AsmrOne.Downloader.Services;

public class DownloaderManager : IDownloaderManager
{
    public DownloaderManager(IAsmrClient asmrClient)
    {
        AsmrClient = asmrClient;
        DownloadSource = [];
    }
    public IList<IDownload> DownloadSource { get; private set; }
    public IAsmrClient AsmrClient { get; }

    public string DownloadBasePath { get; set; }

    public async Task<string> CreateDownloaderAsync(string downloadValue, DownloadType downloadType)
    {
        if(downloadType == DownloadType.File)
        {
            return await CreateFileAsync(downloadValue);
        }
        else
        {
            return await CreateRJAsync(downloadValue);
        }
    }

    private async Task<string> CreateRJAsync(string downloadValue)
    {
        RJDownload rj = new RJDownload()
        {
            DownloadKey = Guid.NewGuid().ToString("N"),
        };
        rj.DownloadBase = this.DownloadBasePath;
        await rj.DownloadAsync(downloadValue,this.AsmrClient);
        return rj.DownloadKey;
    }

    private async Task<string> CreateFileAsync(string downloadValue)
    {
        throw new NotImplementedException();
    }

    public Task PauseDownloadAsync(string downloadKey, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task StartDownloadAsync(string downloadKey, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task StopDownloadAsync(string downloadKey, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}
