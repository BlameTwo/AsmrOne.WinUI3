using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using AsmrOne.WinUI3.Common;
using AsmrOne.WinUI3.Views;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using WinUIEx;

namespace AsmrOne.WinUI3;

public sealed partial class SubtitleWindowBase : Window, IDisposable
{
    public OverlappedPresenter OverlappedPresenter => (OverlappedPresenter)AppWindow.Presenter;

    public SubtitleWindowBase()
    {
        this.InitializeComponent();
        //SystemBackdrop = new TransparentTintBackdrop();
        LayerWindowHelper.SetLayerWindow(this);
        //var overlappedPresenter = OverlappedPresenter;
        //overlappedPresenter.IsResizable = false;

        //this.AppWindow.IsShownInSwitchers = false;
        //overlappedPresenter.IsMaximizable = false;
        //overlappedPresenter.IsMinimizable = false;
        //overlappedPresenter.SetBorderAndTitleBar(true, true);
        //ExtendsContentIntoTitleBar = true;
        //WindowExtension.Penetrate(this);
        //WindowExtension.UnPenetrate(this);
    }

    public void Dispose()
    {
        if (this.Content is SubtitleWindow subtitle)
        {
            subtitle.Dispose();
        }
        this.Content = null;
    }
}

public static partial class LayerWindowHelper
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("CodeQuality", "IDE0079:��ɾ������Ҫ�ĺ���")]
    private const int WS_EX_LAYERED = 0x80000;

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "IdentifierTypo")]
    [SuppressMessage("CodeQuality", "IDE0079:��ɾ������Ҫ�ĺ���")]
    private const int GWL_EXSTYLE = -20;

    [LibraryImport("user32.dll", SetLastError = true)]
    public static partial long GetWindowLongA(nint hWnd, int nIndex);

    [LibraryImport("user32.dll")]
    public static partial int SetWindowLongA(nint hWnd, int nIndex, long dwNewLong);

    public static void SetLayerWindow(Window window)
    {
        var hWnd = (nint)window.AppWindow.Id.Value;
        var exStyle = GetWindowLongA(hWnd, GWL_EXSTYLE);
        if ((exStyle & WS_EX_LAYERED) is 0)
            _ = SetWindowLongA(hWnd, GWL_EXSTYLE, exStyle | WS_EX_LAYERED);
        var style = GetWindowLongA(hWnd, GWL_STYLE);
        _ = SetWindowLongA(hWnd, GWL_STYLE, style & ~WS_OVERLAPPEDWINDOW);
    }

    public const int GWL_STYLE = -16;
    public const uint WS_CAPTION = 0x00C00000;
    public const uint WS_MAXIMIZEBOX = 0x00010000;
    public const uint WS_MINIMIZEBOX = 0x00020000;
    public const uint WS_OVERLAPPED = 0x00000000;

    public const uint WS_OVERLAPPEDWINDOW = (
        WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX
    );

    public const uint WS_SYSMENU = 0x00080000;
    public const uint WS_THICKFRAME = 0x00040000;
}
