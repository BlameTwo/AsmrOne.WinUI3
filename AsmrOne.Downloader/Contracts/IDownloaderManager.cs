using AsmrOne.Downloader.Models;
using AsmrOne.Models.Enums;

namespace AsmrOne.Downloader.Contracts;

public interface IDownloaderManager
{
    public string DownloadBasePath { get; set; }

    /// <summary>
    /// 下载所有源，不要用于UI界面
    /// </summary>
    public IList<IDownload> DownloadSource { get; }


    /// <summary>
    /// 创建下载任务
    /// </summary>
    /// <param name="downloadValue">下载参数</param>
    /// <param name="downloadType">下载类型</param>
    /// <returns></returns>
    public Task<string> CreateDownloaderAsync(string downloadValue, DownloadType downloadType);

    public Task StartDownloadAsync(string downloadKey, CancellationToken token = default);

    public Task PauseDownloadAsync(string downloadKey, CancellationToken token = default);


    public Task StopDownloadAsync(string downloadKey,CancellationToken token = default);
}