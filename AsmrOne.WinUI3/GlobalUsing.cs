using System.Collections.Generic;
using Windows.Storage;

namespace AsmrOne.WinUI3;

public static class GlobalUsing
{
    /// <summary>
    /// 是否隐藏封面
    /// </summary>
    public static bool IsHideCover
    {
        get
        {
            var result = LocalSettings.Values[nameof(IsHideCover)];
            if (result == null)
                return false;
            return (bool)result;
        }
        set => LocalSettings.Values[nameof(IsHideCover)] = value;
    }

    /// <summary>
    /// 是否隐藏R18标签
    /// </summary>
    public static bool IsHideR18Tag
    {
        get
        {
            var result = LocalSettings.Values[nameof(IsHideR18Tag)];
            if (result == null)
                return false;
            return (bool)result;
        }
        set => LocalSettings.Values[nameof(IsHideR18Tag)] = value;
    }

    public static string Token
    {
        get
        {
            var result = LocalSettings.Values[nameof(Token)];
            if (result == null)
                return "";
            return (string)result;
        }
        set => LocalSettings.Values[nameof(IsHideR18Tag)] = value;
    }

    public static ApplicationDataContainer LocalSettings =>
        Windows.Storage.ApplicationData.Current.LocalSettings;
}
