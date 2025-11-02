using AsmrOne.Models.Enums;

namespace AsmrOne.Downloader.Models;

public delegate Task DownloadChangedDelegate(object sender, DownloadArgs args);

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
    public bool IsAction { get; set; }

    public long FileTotal { get; set; } 

    public long FileIndex { get; set; }

    public long FileSize { get; set; }

    public long FileIndexSize { get; set; }

    public long TotalSize { get; set; } 

    public long CurrentSize { get; set; }

    /// <summary>
    /// 下载速度
    /// </summary>
    public double DownloadSpeed { get; set; }

    /// <summary>
    /// 已经下载的时间
    /// </summary>
    public DateTime DownloadTime { get; set; }
}
