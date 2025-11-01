using AsmrOne.Models.Enums;

namespace AsmrOne.Downloader;

/// <summary>
/// 
/// </summary>
public class Handler
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

    /// <summary>
    /// 进度
    /// </summary>
    public double Progress { get; set; }

    /// <summary>
    /// 下载速度
    /// </summary>
    public double DownloadSpeed { get; set; }

    /// <summary>
    /// 已经下载的时间
    /// </summary>
    public DateTime DownloadTime { get; set; }
}
