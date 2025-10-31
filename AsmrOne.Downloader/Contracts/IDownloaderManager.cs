using AsmrOne.WinUI3.Models;
using AsmrOne.WinUI3.Models.AsmrOne;

namespace AsmrOne.Downloader.Contracts;

public interface IDownloaderManager
{
    /// <summary>
    /// 下载RJ
    /// </summary>
    /// <param name="rj">RJ详细内容</param>
    /// <param name="audioDataWrapper">音频数据</param>
    /// <param name="downloadType"></param>
    /// <returns></returns>
    public Task CreateDownloader(RidDetily rj,IAudioDataWrapper audioDataWrapper, string downloadType);
}