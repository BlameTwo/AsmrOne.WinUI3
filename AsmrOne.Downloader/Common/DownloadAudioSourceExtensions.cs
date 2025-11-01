using AsmrOne.WinUI3.Models;
using AsmrOne.WinUI3.Models.AsmrOne;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.Security.Cryptography.Certificates;

namespace AsmrOne.Core.Common;

public static class DownloadAudioSourceExtensions
{

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
}
