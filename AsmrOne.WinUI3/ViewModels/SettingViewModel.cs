using AsmrOne.WinUI3.Common.Bases;
using AsmrOne.WinUI3.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace AsmrOne.WinUI3.ViewModels;

public sealed partial class SettingViewModel : ViewModelBase
{
    public SettingViewModel()
    {
        this.IsConver = GlobalUsing.IsHideCover;
        this.IsHideR18 = GlobalUsing.IsHideR18;
    }

    [ObservableProperty]
    bool isConver;

    [ObservableProperty]
    bool isHideR18;

    partial void OnIsConverChanged(bool value)
    {
        GlobalUsing.IsHideCover = value;
        SendConfig();
    }

    private void SendConfig()
    {
        WeakReferenceMessenger.Default.Send<RefreshSetting>(new() { HideCover = this.IsConver });
    }

    partial void OnIsHideR18Changed(bool value)
    {
        GlobalUsing.IsHideR18 = value;
    }
}
