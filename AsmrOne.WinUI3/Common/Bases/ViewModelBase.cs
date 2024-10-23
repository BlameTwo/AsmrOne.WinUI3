using System;
using AsmrOne.WinUI3.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace AsmrOne.WinUI3.Common.Bases;

public partial class ViewModelBase : ObservableRecipient
{
    public ViewModelBase()
    {
        this.HideCover = GlobalUsing.IsHideCover;
        this.Messenger.Register<RefreshSetting>(this, RefreshSettingMethod);
    }

    private void RefreshSettingMethod(object recipient, RefreshSetting message)
    {
        this.HideCover = message.HideCover;
    }

    [ObservableProperty]
    bool hideCover;
}
