using AsmrOne.Core;
using AsmrOne.Downloader.Common;
using AsmrOne.Downloader.Models;
using AsmrOne.Models.Enums;
using AsmrOne.WinUI3.Models.AsmrOne;
using System.Buffers;
using System.Web;

namespace AsmrOne.Downloader.Services;
internal partial class FileDownload : IDownload
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
    public event DownloadCompletedDelegate DownloadCompleted
    {
        add => downloadCompleted += value;
        remove => downloadCompleted -= value;
    }
    private DownloadChangedDelegate downloadChanged;
    private DownloadCompletedDelegate downloadCompleted;

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

    public bool IsCompleted { get; private set; }
    public bool IsError { get; private set; }
    public string TaskName { get; private set; }
    public object Description { get; private set; }
    public string Cover { get; private set; }

    public string DownloadFolder { get; private set; }

    public async Task<bool> DownloadAsync(object rjId, IAsmrClient asmrClient)
    {
        try
        {
            if (rjId is not Child child)
            {
                return false;
            }
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
            }
            _downloadState = new DownloadState();
            this.RJID = child.Title;
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            
            _httpClient = new HttpClient(httpClientHandler);
            var downloadBase = child.MediaDownloadUrl;
            var downloadPath = HttpUtility.UrlDecode((DownloadBase + "\\" + RJID));
            var uri = new Uri(downloadBase);
            var fileName = Path.GetFileName(HttpUtility.UrlDecode(downloadPath));
            var item =  new DownloadItemSource()
            {
                ClientPath = downloadPath,
                DownloadUrl = downloadBase,
                DisplayName = fileName,
                Size = child.Size,
                MD5 = child.Hash,
                FileType = child.Type,
            };
            this.AudioSource = [item];
            this.TotalCount = 1;
            this.TaskName = child.MediaDownloadUrl;
            this.Cover = child.MediaDownloadUrl;
            this.TaskName = child.Title;
            this.Description = child.MediaDownloadUrl;
            this.DownloadTotalSize = AudioSource.Sum(x => x.Size);
            this._token = asmrClient.GetToken();
            this.DownloadFolder = DownloadBase;
            this.Status = DownloadStatus.Create;
            await this._downloadState.PauseAsync();
            await StartDownloadAsync();
            return true;
        }
        catch (OperationCanceledException ex)
        {
            return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    private async Task UpdateProgress(long bytesRead)
    {
        if (bytesRead > 0)
        {
            CurrentFileTotalSize += bytesRead;
            CurrentTotalSize += bytesRead;
        }
        if (this.downloadChanged != null)
        {
            await downloadChanged
                .Invoke(
                    this,
                    new DownloadArgs()
                    {
                        Title = TaskName,
                        Status = this.Status,
                        DownloadTotalSize = this.DownloadTotalSize,
                        CurrentTotalSize = this.CurrentTotalSize,
                        CurrentIndex = this.CurrentIndex,
                        TotalCount = this.TotalCount,
                        DownloadKey = this.DownloadKey,
                        CurrentFileTotalSize = this.CurrentFileTotalSize,
                        DownloadFileTotalSize = this.DownloadFileTotalSize,
                    }
                )
                .ConfigureAwait(false);
        }
    }

    public async Task<bool> PauseAsync()
    {
        await UpdateProgress(0);
        return await _downloadState.PauseAsync();
    }

    public async Task<bool> ResumeAsync()
    {
        await UpdateProgress(0);
        return await _downloadState.ResumeAsync();
    }

    public async Task<bool> StopAsync()
    {
        try
        {
            await _cts.CancelAsync();
            _downloadState = new DownloadState();
            this.DownloadTotalSize = 0;
            this.CurrentTotalSize = 0;
            DownloadFileTotalSize = 0;
            CurrentFileTotalSize = 0;
            TotalCount = 0;
            CurrentIndex = 0;
            await UpdateProgress(0);
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
        _downloadState.CancelToken = _cts.Token;
        _ = Task.Run(async () =>
        {
            try
            {
                this.DownloadTotalSize = AudioSource.Sum(x => x.Size);
                CurrentTotalSize = 0;
                CurrentIndex = 0;
                await _downloadState.ResumeAsync();
                TotalCount = AudioSource.Count;
                DownloadFileTotalSize = 0;
                var memoryPool = ArrayPool<byte>.Shared;
                for (int i = 0; i < this.AudioSource.Count; i++)
                {
                    CurrentFileTotalSize = 0;
                    DownloadFileTotalSize = AudioSource[i].Size;
                    CurrentIndex = i + 1;
                    var baseFolder = Path.GetDirectoryName(AudioSource[i].ClientPath);
                    Directory.CreateDirectory(baseFolder);
                    if (this._cts.IsCancellationRequested)
                        throw new OperationCanceledException();
                    if (File.Exists(AudioSource[i].ClientPath))
                    {
                        File.Delete(AudioSource[i].ClientPath);
                    }
                    using var fs = new FileStream(
                        AudioSource[i].ClientPath,
                        FileMode.CreateNew,
                        FileAccess.ReadWrite,
                        FileShare.Read,
                        262144,
                        true
                    );
                    await this._downloadState.PauseToken.WaitIfPausedAsync();
                    var request = new HttpRequestMessage(
                        HttpMethod.Get,
                        AudioSource[i].DownloadUrl
                    );
                    using var response = await _httpClient
                        .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                        .ConfigureAwait(false); // 非UI上下文切换
                    response.EnsureSuccessStatusCode();
                    await UpdateProgress(0);
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
                        await _downloadState
                            .SpeedLimiter.LimitAsync(bytesRead)
                            .ConfigureAwait(false);
                        await fs.WriteAsync(buffer.AsMemory(0, bytesRead), _cts.Token)
                            .ConfigureAwait(false);
                        totalWritten += bytesRead;
                        accumulatedBytes += bytesRead;
                        if (accumulatedBytes >= UpdateThreshold)
                        {
                            await UpdateProgress(accumulatedBytes);
                            accumulatedBytes = 0;
                        }
                        memoryPool.Return(buffer);
                    }
                    if (accumulatedBytes > 0 && !isBreak)
                    {
                        await UpdateProgress(accumulatedBytes).ConfigureAwait(false);
                    }
                    if (totalWritten != AudioSource[i].Size)
                    {
                        throw new IOException(
                            $"文件写入不完整: {totalWritten}/{AudioSource[i].Size}，已经停止下载"
                        );
                    }
                    fs.SetLength(AudioSource[i].Size);
                    await fs.FlushAsync();
                }
                IsCompleted = true;
                if (this.downloadCompleted != null)
                {
                    await downloadCompleted
                        .Invoke(this, await GetDownloadStatus())
                        .ConfigureAwait(false);
                }
                return true;
            }
            catch (Exception ex)
            {
                IsCompleted = false;
                this.IsError = true;
                this.ErrorMessage = ex.Message;
                await StopAsync();
                if (downloadCompleted != null)
                {
                    await downloadCompleted
                        .Invoke(this, await GetDownloadStatus())
                        .ConfigureAwait(false);
                }
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
        return await Task.FromResult(
            new DownloadArgs()
            {
                Title = TaskName,
                Description = "",
                Status = this.Status,
                DownloadTotalSize = this.DownloadTotalSize,
                CurrentTotalSize = this.CurrentTotalSize,
                CurrentIndex = this.CurrentIndex,
                TotalCount = this.TotalCount,
                DownloadKey = this.DownloadKey,

                CurrentFileTotalSize = this.CurrentFileTotalSize,
                DownloadFileTotalSize = this.DownloadFileTotalSize,
                IsPause = this._downloadState?.IsPaused,
                IsAction = this._downloadState?.IsActive,
                IsCompleted = this.IsCompleted,
                IsError = this.IsError,
                ErrorMessage = this.ErrorMessage,
            }
        );
    }


    public async ValueTask DisposeAsync()
    {
        await StopAsync();
        AudioSource.Clear();
    }
}

