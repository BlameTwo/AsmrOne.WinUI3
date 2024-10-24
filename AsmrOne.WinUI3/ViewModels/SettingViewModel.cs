using System;
using AsmrOne.WinUI3.Common.Bases;
using AsmrOne.WinUI3.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Windows.Storage;

namespace AsmrOne.WinUI3.ViewModels;

public sealed partial class SettingViewModel : ViewModelBase
{
    public SettingViewModel()
    {
        this.IsConver = GlobalUsing.IsHideCover;
        this.IsTag = GlobalUsing.IsHideR18Tag;
    }

    [ObservableProperty]
    bool isConver;

    [ObservableProperty]
    bool isTag;

    partial void OnIsConverChanged(bool value)
    {
        GlobalUsing.IsHideCover = value;
        SendConfig();
    }

    private void SendConfig()
    {
        WeakReferenceMessenger.Default.Send<RefreshSetting>(new() { HideCover = this.IsConver });
    }

    partial void OnIsTagChanged(bool value)
    {
        GlobalUsing.IsHideR18Tag = value;
    }
}
