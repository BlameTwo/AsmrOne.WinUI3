using AsmrOne.Downloader.Models;
using AsmrOne.WinUI3.Models;
using AsmrOne.WinUI3.Models.AsmrOne;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Encodings.Web;
using System.Web;
using Windows.Security.Cryptography.Certificates;

namespace AsmrOne.Downloader;

public static class DownloadAudioSourceExtensions
{
    private static object size;

    public static long GetSourceSize(this List<Child> children)
    {
        long size = 0;
        GetAudioSizes(children, ref size);
        return size;
    }


    public static void GetAudioSizes(List<Child> child, ref long size)
    {
        foreach (var item in child)
        {
            if (item.Type == "folder")
            {
                GetAudioSizes(item.Children, ref size);
            }
            else if (item.Type == "audio" || item.Type == "text" || item.Type == "image")
            {
                size += item.Size;
            }
        }
    }

    public static List<DownloadItemSource> GetDownloadItemSource(this List<Child> child,string baseAddress)
    {
        try
        {
            List<DownloadItemSource> source = new List<DownloadItemSource>();
            foreach (var item in child)
            {
                if (item.Type == "folder")
                {
                    source.AddRange(GetDownloadItemSource(item.Children, baseAddress));
                }
                else if (item.Type == "audio" || item.Type == "audio" || item.Type == "text" || item.Type == "image")
                {
                    var downloadBase = item.MediaDownloadUrl;
                    var downloadPath = HttpUtility.UrlDecode((baseAddress + "\\" + downloadBase.Substring(downloadBase.IndexOf(item.Work.SourceId)).Replace("/", "\\")));
                    var uri = new Uri(downloadBase);
                    var fileName = Path.GetFileName(HttpUtility.UrlDecode(downloadPath));
                    source.Add(new DownloadItemSource()
                    {
                        ClientPath = downloadPath,
                        DownloadUrl = downloadBase,
                        DisplayName = fileName,
                        Size = item.Size,
                        MD5 = item.Hash,
                        FileType = item.Type,
                    });
                }
            }
            return source;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}
