using AsmrOne.Models.Enums;

namespace AsmrOne.Downloader.Models;

public delegate Task DownloadChangedDelegate(object sender, DownloadArgs args);

public delegate Task DownloadCompletedDelegate(object sender, DownloadArgs args);

/// <summary>
/// 下载事件
/// </summary>
public class DownloadArgs
{
    /// <summary>
    /// 下载标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 封面
    /// </summary>
    public string CoverImage { get; set; }

    /// <summary>
    /// 简介
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// 下载状态
    /// </summary>
    public DownloadStatus Status { get; set; }

    /// <summary>
    /// 进度循环
    /// </summary>
    public bool? IsAction { get; set; }

    public string ErrorMessage { get; set; }

    public bool IsError { get; set; }

    public bool IsCompleted { get; set; }

    /// <summary>
    /// 下载速度
    /// </summary>
    public double DownloadSpeed { get; set; }

    public string DownloadKey { get; set; }

    /// <summary>
    /// 已经下载的时间
    /// </summary>
    public DateTime DownloadTime { get; set; }
    public long DownloadTotalSize       { get; internal set; }
    public long CurrentTotalSize        { get; internal set; }
    public long CurrentIndex            { get; internal set; }
    public long TotalCount              { get; internal set; }
    public long CurrentFileTotalSize { get; internal set; }
    public long DownloadFileTotalSize { get; internal set; }

    public bool? IsPause { get; set; }

}
