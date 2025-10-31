using AsmrOne.WinUI3.Models.AsmrOne;
using AsmrOne.WinUI3.Models.ItemWrapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AsmrOne.Core.Common;

public static class DataCreate
{
    public static List<SearchTagWrapper> ToTags(this IEnumerable<TagList> tag, string tagName)
    {
        IList<SearchTagWrapper> searchWrapper = new List<SearchTagWrapper>();
        foreach (var item in tag)
        {
            searchWrapper.Add(new SearchTagWrapper()
            {
                DisplayName = $"{tagName}:{item.Name}({item.Count})",
                Name = item.Name,
                Type = GetSearchType(tagName)
            });
        }
        return (List<SearchTagWrapper>)searchWrapper;
    }


    public static string GetSearchType(string tag)
    {
        switch (tag)
        {
            case "tags":
                return "tag";
            case "vas":
                return "va";
            case "circles":
                return "circle";
            default:
                return "";
        }
    }
}
