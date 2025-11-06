using System.Collections.Generic;
using System.Collections.ObjectModel;
using AsmrOne.WinUI3.Models.ItemWrapper;

namespace AsmrOne.WinUI3.Models.AsmrOne;

public class TagType
{
    public string Display { get; set; }

    public string Memory { get; set; }

    public static ObservableCollection<TagType> Default =>
        new ObservableCollection<TagType>()
        {
            new TagType() { Display = "标签", Memory = "tags" },
            new TagType() { Display = "社团", Memory = "circles" },
            new TagType() { Display = "声优", Memory = "vas" },
            new TagType() { Display = "时长", Memory = "durations" },
        };

    public static ObservableCollection<SearchTagWrapper> GetDurationsTags() =>
        new ObservableCollection<SearchTagWrapper>()
        {
            new SearchTagWrapper()
            {
                DisplayName = "20分钟",
                Type = "duration",
                Name = "20m",
            },
            new SearchTagWrapper()
            {
                DisplayName = "30分钟",
                Type = "duration",
                Name = "30m",
            },
            new SearchTagWrapper()
            {
                DisplayName = "40分钟",
                Type = "duration",
                Name = "40m",
            },
            new SearchTagWrapper()
            {
                DisplayName = "1小时",
                Type = "duration",
                Name = "1h",
            },
            new SearchTagWrapper()
            {
                DisplayName = "1小时20分钟",
                Type = "duration",
                Name = "1.2h",
            },
        };
}
