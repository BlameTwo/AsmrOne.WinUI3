using System;
using System.Runtime.InteropServices;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using WinRT.Interop;
using WinUIEx;
using static AsmrOne.WinUI3.Common.Win32;

namespace AsmrOne.WinUI3.Common;

public static class WindowExtension
{
    public enum CreateType
    {
        Subtitle,
    }

    public const uint WS_EX_LAYERED = 0x80000;
    public const uint WS_EX_TRANSPARENT = 0x20;
    public const int GWL_STYLE = (-16);
    public const int GWL_EXSTYLE = (-20);
    public const int LWA_ALPHA = 0;

    public static Window CreateTransparentWindow(CreateType type)
    {
        var window = new Window();
        var dpi = WindowExtension.GetScaleAdjustment(window);
        var workArea = WindowExtension.GetWorkarea();
        //window.SystemBackdrop = new TransparentTintBackdrop();
        window.SystemBackdrop = new MicaBackdrop()
        {
            Kind = Microsoft.UI.Composition.SystemBackdrops.MicaKind.BaseAlt,
        };
        window.AppWindow.IsShownInSwitchers = false;
        window.AppWindow.TitleBar.ExtendsContentIntoTitleBar = true;
        if (window.AppWindow.Presenter is OverlappedPresenter presenter)
        {
            presenter.IsMaximizable = false;
            presenter.IsMinimizable = false;
            presenter.IsResizable = false;
            presenter.IsAlwaysOnTop = true;
            presenter.SetBorderAndTitleBar(true, false);
        }
        if (type == CreateType.Subtitle)
        {
            double height = 100;
            int leftMargin = 200;
            int rightMargin = 200;
            double width = workArea.Value.Right - workArea.Value.Left - leftMargin - rightMargin;
            int left = workArea.Value.Left + leftMargin;
            int top = workArea.Value.Bottom - (int)height;
            window.SetWindowSize(width / dpi, height / dpi);
            window.AppWindow.Move(new Windows.Graphics.PointInt32() { X = left, Y = top });
        }

        return window;
    }

    public static double GetScaleAdjustment(Window window)
    {
        IntPtr hWnd = WindowNative.GetWindowHandle(window);
        WindowId wndId = Win32Interop.GetWindowIdFromWindow(hWnd);
        DisplayArea displayArea = DisplayArea.GetFromWindowId(wndId, DisplayAreaFallback.Primary);
        IntPtr hMonitor = Win32Interop.GetMonitorFromDisplayId(displayArea.DisplayId);

        // Get DPI.
        int result = GetDpiForMonitor(
            hMonitor,
            Monitor_DPI_Type.MDT_Default,
            out uint dpiX,
            out uint _
        );
        if (result != 0)
        {
            throw new Exception("Could not get DPI for monitor.");
        }

        uint scaleFactorPercent = (uint)(((long)dpiX * 100 + (96 >> 1)) / 96);
        return scaleFactorPercent / 100.0;
    }

    public static void Penetrate(Window window)
    {
        IntPtr hWnd = WindowNative.GetWindowHandle(window);
        GetWindowLong(hWnd, GWL_EXSTYLE);
        SetWindowLong(hWnd, GWL_EXSTYLE, (WS_EX_TRANSPARENT | WS_EX_LAYERED));
        SetLayeredWindowAttributes(hWnd, 0, 100, LWA_ALPHA);
    }

    [DllImport("Shcore.dll", SetLastError = true)]
    public static extern int GetDpiForMonitor(
        IntPtr hmonitor,
        Monitor_DPI_Type dpiType,
        out uint dpiX,
        out uint dpiY
    );

    [DllImport("user32.dll")]
    public static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

    [DllImport("user32", EntryPoint = "GetWindowLong")]
    public static extern uint GetWindowLong(IntPtr hwnd, int nIndex);

    [DllImport("user32", EntryPoint = "SetLayeredWindowAttributes")]
    public static extern int SetLayeredWindowAttributes(
        IntPtr hwnd,
        int crKey,
        int bAlpha,
        int dwFlags
    );

    public enum Monitor_DPI_Type : int
    {
        MDT_Effective_DPI = 0,
        MDT_Angular_DPI = 1,
        MDT_Raw_DPI = 2,
        MDT_Default = MDT_Effective_DPI,
    }

    public static RECT? GetWorkarea()
    {
        RECT workArea = new RECT();
        if (SystemParametersInfo(SPI_GETWORKAREA, 0, ref workArea, 0))
        {
            return workArea;
        }
        return null;
    }
}
