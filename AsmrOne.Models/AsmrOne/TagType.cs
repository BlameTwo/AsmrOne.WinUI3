using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AsmrOne.WinUI3.Models.AsmrOne;

public class TagType
{
    public string Display { get; set; }

    public string Memory { get; set; }

    public static ObservableCollection<TagType> Default => new ObservableCollection<TagType>()
    {
        new TagType()
        {
            Display = "标签",
            Memory = "tags"
        },
        new TagType()
        {
            Display = "社团",
            Memory = "circles"
        },
        new TagType()
        {
            Display = "声优",
            Memory = "vas"
        }
    };
}
