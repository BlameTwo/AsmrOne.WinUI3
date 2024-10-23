using System;
using System.Collections.Generic;
using System.Linq;
using AsmrOne.WinUI3.Common;
using static AsmrOne.WinUI3.Common.WebVTTParse;

namespace AsmrOne.WinUI3.Contracts.Services;

public class SubtitleService : ISubtitleService
{
    public List<WebVTTParse.SubtitleItem> SubTitles { get; private set; }

    public SubtitleItem GetSubtitle(TimeSpan time)
    {
        if (SubTitles == null)
            return default;
        return SubTitles
            .Where(subtitle => time >= subtitle.StartTime && time <= subtitle.EndTime)
            .ToList()
            .FirstOrDefault();
    }

    public void SetSubtitle(List<WebVTTParse.SubtitleItem> time)
    {
        this.SubTitles = time;
    }

    public void SetSubtitle(string text)
    {
        this.SubTitles = WebVTTParse.ParseSubtitles(text);
    }
}
