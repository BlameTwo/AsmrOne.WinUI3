using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using AsmrOne.Models;
using Microsoft.UI.Xaml.Shapes;

namespace AsmrOne.WinUI3.Common;

public static class WebVTTParse
{
    public static List<SubtitleItem> ParseSubtitles(string filePath)
    {
        var subtitles = new List<SubtitleItem>();
        string[] blocks = filePath.Split(new[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries);

        string timePattern = @"\d{2}:\d{2}:\d{2}\.\d{3} --> \d{2}:\d{2}:\d{2}\.\d{3}";

        foreach (var block in blocks)
        {
            var lines = block.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            SubtitleItem currentSubtitle = null;

            if (lines.Length == 3)
            {
                var times = lines[1].Split(new[] { " --> " }, StringSplitOptions.None);
                if (times.Length == 2)
                {
                    var start = TimeSpan.Parse(times[0]);
                    var end = TimeSpan.Parse(times[1]);
                    currentSubtitle = new SubtitleItem
                    {
                        StartTime = start,
                        EndTime = end,
                        Text = lines[2],
                    };
                }
            }

            // 添加当前块的字幕（如果有）
            if (currentSubtitle != null)
            {
                subtitles.Add(currentSubtitle);
            }
        }

        return subtitles;
    }

}
