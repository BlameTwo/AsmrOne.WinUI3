using AsmrOne.Core;
using AsmrOne.Core.Common;
using AsmrOne.Downloader.Models;
using AsmrOne.Models.Enums;
using AsmrOne.WinUI3.Models.AsmrOne;
using CommunityToolkit.Mvvm.Input;
using System.Buffers;
using System.Diagnostics;
using System.Formats.Asn1;
using System.IO;
using System.IO.Pipes;
using System.Net.Http.Headers;

namespace AsmrOne.Downloader.Common;

internal partial class RJDownload : IDownload
{

    const long UpdateThreshold = 1048576; // 1MB进度更新阈值

    private CancellationTokenSource _cts = null;
    private HttpClient _httpClient;
    private string _token = null;
    public DownloadType Type => DownloadType.RJ;
    public DownloadState _downloadState;

    public event DownloadChangedDelegate DownloadChanged
    {
        add => downloadChanged += value;
        remove => downloadChanged -= value;
    }

    private DownloadChangedDelegate downloadChanged;

    public string DownloadKey { get; internal set; }

    public DownloadStatus Status { get; internal set; }

    public string RJID { get; set; }

    /// <summary>
    /// 整个下载字节总量大小
    /// </summary>
    public long DownloadTotalSize { get; set; }

    /// <summary>
    /// 整个当前字节下载总和
    /// </summary>
    public long CurrentTotalSize { get; set; }


    /// <summary>
    /// 文件下载总量
    /// </summary>
    public long DownloadFileTotalSize { get; set; }

    /// <summary>
    /// 当前文件下载下载量
    /// </summary>
    public long CurrentFileTotalSize { get; set; }

    /// <summary>
    /// 当前文件数量
    /// </summary>
    public long TotalCount { get; set; }

    /// <summary>
    /// 当前文件位置
    /// </summary>
    public long CurrentIndex { get; set; }

    public List<DownloadItemSource> AudioSource { get; private set; }
    public string DownloadBase { get; internal set; }
    public string ErrorMessage { get; private set; }

    public async Task<bool> DownloadAsync(string rjId,IAsmrClient asmrClient)
    {
        try
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
            }
            this.RJID = rjId;
            _httpClient = new HttpClient();
            var work = await asmrClient.GetWorkAsync(this.RJID);
            var audios = await asmrClient.GetWorkAudioAsync(this.RJID);
            if (audios.Item1 == null || work.Item1 == null)
            {
                return false;
            }
            this.AudioSource = audios.Item1.GetDownloadItemSource(this.DownloadBase);
            this.TotalCount = AudioSource.Count;
            this.DownloadTotalSize = AudioSource.Sum(x => x.Size);
            this._token = asmrClient.GetToken();
            this.Status = DownloadStatus.Create;
            await UpdateProgress();
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

    private async Task UpdateProgress(long bytesRead = 0)
    {
        if(bytesRead > 0)
        {
            CurrentFileTotalSize += bytesRead;
            CurrentTotalSize += bytesRead;
        }
        if(this.downloadChanged!= null)
        {
            await downloadChanged.Invoke(this, new DownloadArgs()
            {
                Title = RJID,
                Description = $"正在下载{RJID}",
                Status = this.Status,
                TotalSize = this.DownloadTotalSize,
                CurrentSize = this.CurrentTotalSize,
                FileIndex = this.CurrentIndex,
                FileTotal = this.TotalCount,
                FileIndexSize = this.CurrentFileTotalSize,
                FileSize = this.DownloadFileTotalSize
            }).ConfigureAwait(false);
        }
    }

    public async Task<bool> PauseAsync()
    {
        await UpdateProgress();
        return await _downloadState.PauseAsync();
    }

    public async Task<bool> ResumeAsync()
    {
        await UpdateProgress();
        return await _downloadState.ResumeAsync();
    }

    public async Task<bool> StopAsync()
    {
        try
        {
            await _cts.CancelAsync();
            await UpdateProgress();
            return true;
        }
        catch (Exception)
        {
            return true;
        }
    }


    public async Task<bool> StartDownloadAsync()
    {
        _cts = new CancellationTokenSource();
        _ = Task.Run(async () =>
        {

            try
            {
                _downloadState = new DownloadState();
                this.DownloadTotalSize = AudioSource.Sum(x => x.Size);
                CurrentFileTotalSize = 0;
                CurrentTotalSize = 0;
                CurrentIndex = 0;
                TotalCount = AudioSource.Count;
                DownloadFileTotalSize = 0;
                var memoryPool = ArrayPool<byte>.Shared;
                for (int i = 0; i < this.AudioSource.Count; i++)
                {
                    CurrentFileTotalSize = 0;
                    var baseFolder = Path.GetDirectoryName(AudioSource[i].ClientPath);
                    Directory.CreateDirectory(baseFolder);

                    if (this._cts.IsCancellationRequested)
                        throw new OperationCanceledException();
                    CurrentIndex = i + 1;
                    if (File.Exists(AudioSource[i].ClientPath))
                    {
                        File.Delete(AudioSource[i].ClientPath);
                    }
                    using var fs = new FileStream(AudioSource[i].ClientPath, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.Read, 262144, true);
                    await this._downloadState.PauseToken.WaitIfPausedAsync();
                    var request = new HttpRequestMessage(HttpMethod.Get, AudioSource[i].DownloadUrl);
                    using var response = await _httpClient
                        .SendAsync(
                            request,
                            HttpCompletionOption.ResponseHeadersRead
                        )
                        .ConfigureAwait(false); // 非UI上下文切换
                    response.EnsureSuccessStatusCode();
                    var stream = await response
                        .Content.ReadAsStreamAsync(_cts.Token)
                        .ConfigureAwait(false);
                    long totalWritten = 0;
                    var isBreak = false;
                    long accumulatedBytes = 0;
                    while (totalWritten < AudioSource[i].Size)
                    {
                        if (_cts.IsCancellationRequested)
                        {
                            throw new OperationCanceledException();
                        }
                        DownloadFileTotalSize = AudioSource[i].Size;
                        CurrentIndex = i;
                        await _downloadState.PauseToken.WaitIfPausedAsync().ConfigureAwait(false);
                        int bytesToRead = (int)Math.Min(65536, AudioSource[i].Size - totalWritten);
                        byte[] buffer = memoryPool.Rent(bytesToRead);
                        int bytesRead = await stream
                            .ReadAsync(buffer.AsMemory(0, bytesToRead), _cts.Token)
                            .ConfigureAwait(false);
                        if (bytesRead == 0)
                        {
                            isBreak = true;
                        }
                        await _downloadState.SpeedLimiter.LimitAsync(bytesRead).ConfigureAwait(false);
                        await fs
                            .WriteAsync(buffer.AsMemory(0, bytesRead), _cts.Token)
                            .ConfigureAwait(false);
                        totalWritten += bytesRead;
                        accumulatedBytes += bytesRead;
                        if (accumulatedBytes >= UpdateThreshold)
                        {
                            await UpdateProgress(bytesRead);
                            accumulatedBytes = 0;
                        }
                        Debug.WriteLine($"正在下载文件{AudioSource[i].DisplayName}");
                        memoryPool.Return(buffer);
                    }
                    if (totalWritten != AudioSource[i].Size)
                    {
                        throw new IOException($"分片写入不完整: {totalWritten}/{AudioSource[i].Size}");
                    }
                    fs.SetLength(AudioSource[i].Size);
                    await fs.FlushAsync();
                    Debug.WriteLine($"文件{AudioSource[i].DisplayName}下载完毕");
                }
                return true;
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
                return false;
            }
        });
        return await Task.FromResult(true);
    }


    /// <summary>
    /// 主动获取下载状态
    /// </summary>
    /// <returns></returns>
    public async Task<DownloadArgs> GetDownloadStatus()
    {
        return await Task.FromResult(new DownloadArgs()
        {
             Title = RJID,
             Description = $"正在下载{RJID}",
             Status = this.Status,
             TotalSize = this.DownloadTotalSize,
             CurrentSize = this.CurrentTotalSize,
             FileIndex = this.CurrentIndex,
             FileTotal = this.TotalCount,
             FileIndexSize = this.CurrentFileTotalSize,
             FileSize = this.DownloadFileTotalSize
        });
    }
}
