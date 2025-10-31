using CommunityToolkit.Mvvm.ComponentModel;

namespace AsmrOne.Downloader.Models;

public partial class DownloadTask:ObservableObject
{
    /// <summary>
    /// RJ号全部下载
    /// </summary>
    public static string DownloadTaskFolder => "rj";

    /// <summary>
    /// 单文件下载
    /// </summary>
    public static string DownloadTaskFile => "file";

    /// <summary>
    /// 下载类型
    /// </summary>
    [ObservableProperty]
    public partial string DownloadType { get; set; }


}
