using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AsmrOne.WinUI3.Common;

public static class WebVTTParse
{
    public static List<SubtitleItem> ParseSubtitles(string filePath)
    {
        List<SubtitleItem> subtitles = new List<SubtitleItem>();

        string[] lines = filePath.Split("\n\n");
        string timePattern = @"\d{2}:\d{2}:\d{2}\.\d{3} --> \d{2}:\d{2}:\d{2}\.\d{3}";

        for (int i = 0; i < lines.Length; i++)
        {
            if (Regex.IsMatch(lines[i], timePattern))
            {
                string[] times = lines[i].Split(new string[] { " --> " }, StringSplitOptions.None);
                TimeSpan startTime = TimeSpan.Parse(times[0]);
                TimeSpan endTime = TimeSpan.Parse(times[1]);
                i++;
                string subtitleText = "";
                while (i < lines.Length && !string.IsNullOrWhiteSpace(lines[i]))
                {
                    subtitleText += lines[i] + "\n";
                    i++;
                }
                subtitles.Add(
                    new SubtitleItem
                    {
                        StartTime = startTime,
                        EndTime = endTime,
                        Text = subtitleText.Trim(),
                    }
                );
            }
        }
        return subtitles;
    }

    public class SubtitleItem
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Text { get; set; }
    }
}
