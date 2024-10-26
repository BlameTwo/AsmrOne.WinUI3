using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AsmrOne.WinUI3.Models.Enums;

public enum MyFavouritesOrder
{
    /// <summary>
    /// 标记时间
    /// </summary>
    UpdateAt,

    /// <summary>
    /// 评价
    /// </summary>
    UserRating,

    /// <summary>
    /// 发布时间
    /// </summary>
    Release,

    /// <summary>
    /// 评价数量
    /// </summary>
    ReviewCount,

    /// <summary>
    /// 售出数量
    /// </summary>
    DlCount,

    /// <summary>
    /// 全年龄新作，只有ASC倒序
    /// </summary>
    NotR18,

    /// <summary>
    /// 18禁新作,只有desc倒序
    /// </summary>
    R18,
}

public enum MyFarouritesSort
{
    Asc,
    Desc,
}

public enum MyFarouritesFilter
{
    /// <summary>
    /// 想听
    /// </summary>
    Marked,

    /// <summary>
    /// 在听
    /// </summary>
    Listening,

    /// <summary>
    /// 听过
    /// </summary>
    Listened,

    /// <summary>
    /// 重听
    /// </summary>
    Replay,

    /// <summary>
    /// 搁置
    /// </summary>
    Postponed,

    /// <summary>
    /// 无
    /// </summary>
    None,
}

public static class FarouritesExtension
{
    public static Dictionary<string, object> GetOrderBy(
        MyFavouritesOrder order,
        MyFarouritesSort sort
    )
    {
        string orderStr = null,
            sortStr = null;
        switch (order)
        {
            case MyFavouritesOrder.UpdateAt:
                orderStr = "updated_at";
                sortStr = sort == MyFarouritesSort.Asc ? "asc" : "desc";
                break;
            case MyFavouritesOrder.UserRating:

                orderStr = "userRating";
                sortStr = sort == MyFarouritesSort.Asc ? "asc" : "desc";
                break;
            case MyFavouritesOrder.Release:
                orderStr = "release";
                sortStr = sort == MyFarouritesSort.Asc ? "asc" : "desc";
                break;
            case MyFavouritesOrder.ReviewCount:
                orderStr = "review_count";
                sortStr = sort == MyFarouritesSort.Asc ? "asc" : "desc";
                break;
            case MyFavouritesOrder.DlCount:
                orderStr = "dl_count";
                sortStr = sort == MyFarouritesSort.Asc ? "asc" : "desc";
                break;
            case MyFavouritesOrder.NotR18:
                orderStr = "nsfw";
                sortStr = "asc";
                break;
            case MyFavouritesOrder.R18:
                orderStr = "nsfw";
                sortStr = "desc";
                break;
        }
        return new Dictionary<string, object>() { { "order", orderStr }, { "sort", sortStr } };
    }

    public static ObservableCollection<QuerySortFavouritesWrapper> GetSort() =>
        new ObservableCollection<QuerySortFavouritesWrapper>()
        {
            new QuerySortFavouritesWrapper("正序", MyFarouritesSort.Asc),
            new QuerySortFavouritesWrapper("倒序", MyFarouritesSort.Desc),
        };

    public static ObservableCollection<QueryOrderFavouritesWrapper> GetOrder() =>
        new()
        {
            new("标记时间", MyFavouritesOrder.UpdateAt),
            new("评价", MyFavouritesOrder.UserRating),
            new("发布时间", MyFavouritesOrder.Release),
            new("发布数量", MyFavouritesOrder.ReviewCount),
            new("售出数量", MyFavouritesOrder.DlCount),
            new("全年龄新作", MyFavouritesOrder.NotR18),
            new("R18新作", MyFavouritesOrder.R18),
        };

    public static ObservableCollection<QueryFillterFavouritesWrapper> GetFillters() =>
        new()
        {
            new("想听", MyFarouritesFilter.Marked),
            new("在听", MyFarouritesFilter.Listening),
            new("听过", MyFarouritesFilter.Listened),
            new("重听", MyFarouritesFilter.Replay),
            new("搁置", MyFarouritesFilter.Postponed),
            new("无排序", MyFarouritesFilter.None),
        };

    public static Dictionary<string, object> GetFillter(MyFarouritesFilter fillter)
    {
        switch (fillter)
        {
            case MyFarouritesFilter.Marked:
                return new Dictionary<string, object>() { { "filter", "marked" } };
            case MyFarouritesFilter.Listening:
                return new Dictionary<string, object>() { { "filter", "listening" } };
            case MyFarouritesFilter.Listened:
                return new Dictionary<string, object>() { { "filter", "listened" } };
            case MyFarouritesFilter.Replay:
                return new Dictionary<string, object>() { { "filter", "replay" } };
            case MyFarouritesFilter.Postponed:
                return new Dictionary<string, object>() { { "filter", "postponed" } };
            case MyFarouritesFilter.None:
                return new();
            default:
                return new();
        }
    }
}
