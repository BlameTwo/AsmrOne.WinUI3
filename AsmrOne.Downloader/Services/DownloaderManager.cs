using AsmrOne.Core;
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
    public Dictionary<string,IDownload> DownloadSource { get; private set; }
    public IAsmrClient AsmrClient { get; }

    public string DownloadBasePath { get; set; }

    public async Task<int> CreateDownloaderAsync(object downloadValue, DownloadType downloadType)
    {
        try
        {
            if (this.DownloadSource.Where(x=>x.Value.IsCompleted == false).Count() >= 2)
            {
                return DownloadErrorCode.MaxTaskError;
            }
            if (downloadType == DownloadType.File)
            {
                await CreateFileAsync(downloadValue);
            }
            else
            {
                await CreateRJAsync(downloadValue);
            }
            return DownloadErrorCode.Success;
        }
        catch (Exception)
        {
            return DownloadErrorCode.OwnerError;
        }
    }

    private async Task<string> CreateRJAsync(object downloadValue)
    {
        RJDownload rj = new RJDownload()
        {
            DownloadKey = Guid.NewGuid().ToString("N"),
        };
        rj.DownloadBase = this.DownloadBasePath;
        await rj.DownloadAsync(downloadValue,this.AsmrClient);
        this.DownloadSource.Add(rj.DownloadKey,rj);
        return rj.DownloadKey;
    }

    private async Task<string> CreateFileAsync(object downloadValue)
    {
        FileDownload fj = new FileDownload()
        {
            DownloadKey = Guid.NewGuid().ToString("N")
        };
        fj.DownloadBase = this.DownloadBasePath;
        await fj.DownloadAsync(downloadValue, this.AsmrClient);
        this.DownloadSource.Add(fj.DownloadKey, fj);
        return fj.DownloadKey;
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

    public async Task DeleteTaskAsync(string downloadKey)
    {
        if(DownloadSource.TryGetValue(downloadKey, out var task))
        {
            await task.StopAsync();
            await task.DisposeAsync();
            DownloadSource.Remove(downloadKey);
        }
        
    }
}
