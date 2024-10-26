using AsmrOne.WinUI3.Models.Enums;

namespace AsmrOne.WinUI3.Models;

public class QueryOrderFavouritesWrapper
{
    public string DisplayName { get; set; }

    public QueryOrderFavouritesWrapper(string displayName, MyFavouritesOrder code)
    {
        DisplayName = displayName;
        Code = code;
    }

    public MyFavouritesOrder Code { get; set; }
}

public class QuerySortFavouritesWrapper
{
    public string DisplayName { get; set; }

    public QuerySortFavouritesWrapper(string displayName, MyFarouritesSort code)
    {
        DisplayName = displayName;
        Code = code;
    }

    public MyFarouritesSort Code { get; set; }
}

public class QueryFillterFavouritesWrapper
{
    public string DisplayName { get; set; }

    public QueryFillterFavouritesWrapper(string displayName, MyFarouritesFilter filter)
    {
        DisplayName = displayName;
        Filter = filter;
    }

    public MyFarouritesFilter Filter { get; set; }
}
