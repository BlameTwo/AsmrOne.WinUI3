
global using AsmrOne.Core;

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
                return true;
            return (bool)result;
        }
        set => LocalSettings.Values[nameof(IsHideCover)] = value;
    }

    /// <summary>
    /// 是否隐藏R18标签
    /// </summary>
    public static bool IsHideR18
    {
        get
        {
            var result = LocalSettings.Values[nameof(IsHideR18)];
            if (result == null)
                return true;
            return (bool)result;
        }
        set => LocalSettings.Values[nameof(IsHideR18)] = value;
    }

    public static bool IsAutoSubtitle
    {
        get
        {
            var result = LocalSettings.Values[nameof(IsAutoSubtitle)];
            if (result == null)
                return true;
            return (bool)result;
        }
        set => LocalSettings.Values[nameof(IsAutoSubtitle)] = value;
    }

    public static bool IsOpenDesktopSubtitle
    {
        get
        {
            var result = LocalSettings.Values[nameof(IsOpenDesktopSubtitle)];
            if (result == null)
                return true;
            return (bool)result;
        }
        set => LocalSettings.Values[nameof(IsOpenDesktopSubtitle)] = value;
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
        set => LocalSettings.Values[nameof(Token)] = value;
    }

    public static ApplicationDataContainer LocalSettings =>
        Windows.Storage.ApplicationData.Current.LocalSettings;
}
