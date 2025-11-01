namespace AsmrOne.Downloader.Models;

public class DownloadItemSource
{
    public string DownloadUrl { get; set; } 

    public string MD5 { get; set; }

    public string ClientPath { get; set; }

    public string DisplayName { get; set; }

    public long Size { get; set; }

    public string FileType { get; set; }

}
