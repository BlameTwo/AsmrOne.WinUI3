using AsmrOne.Core;
using AsmrOne.Core.Common;
using AsmrOne.Downloader.Models;
using AsmrOne.Models.Enums;
using AsmrOne.WinUI3.Models.AsmrOne;

namespace AsmrOne.Downloader.Common;

public class RJDownload : IDownload
{
    private CancellationTokenSource _cts = null;
    private string _token = null;
    public DownloadType Type => DownloadType.RJ;

    public string DownloadKey { get; internal set; }

    public string RJID { get; set; }
    public List<DownloadItemSource> AudioSource { get; private set; }

    public async Task<bool> DownloadAsync(string rjId,IAsmrClient asmrClient)
    {
        try
        {
            // 如果已经有下载任务在进行，先取消之前的
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
            }
            _cts = new CancellationTokenSource();
            this.RJID = rjId;
            var work = await asmrClient.GetWorkAsync(this.RJID);
            var audios = await asmrClient.GetWorkAudioAsync(this.RJID);
            if (audios.Item1 == null || work.Item1 == null)
            {
                return false;
            }
            this.AudioSource = GetDownloadSource(audios.Item1);
            this._token = asmrClient.GetToken();
            Task.Run(async () => await StartDownloadAsync());
            return true;
        }
        catch (OperationCanceledException ex)
        {
            return false;
        }
        catch(Exception ex)
        {
            return false;
        }
    }

    private async Task StartDownloadAsync()
    {
        var allSize = AudioSource.Sum(x=>x.Size);
    }


    private List<DownloadItemSource> GetDownloadSource(List<Child> children)
    {
        return new();
    }

    public Task<bool> PauseAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> StopAsync()
    {
        throw new NotImplementedException();
    }
}
