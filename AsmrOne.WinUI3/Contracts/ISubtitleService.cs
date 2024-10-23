using System;
using System.Collections.Generic;
using static AsmrOne.WinUI3.Common.WebVTTParse;

namespace AsmrOne.WinUI3.Contracts;

public interface ISubtitleService
{
    public SubtitleItem GetSubtitle(TimeSpan time);

    public void SetSubtitle(List<SubtitleItem> time);

    public void SetSubtitle(string text);
}
