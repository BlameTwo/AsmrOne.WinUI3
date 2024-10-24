using System;
using System.Threading;
using AsmrOne.WinUI3.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace AsmrOne.WinUI3.Common.Bases;

public partial class ViewModelBase : ObservableRecipient, IDisposable
{
    public CancellationTokenSource CTS { get; private set; }

    public ViewModelBase()
    {
        CTS = new();
        this.HideCover = GlobalUsing.IsHideCover;
        this.Messenger.Register<RefreshSetting>(this, RefreshSettingMethod);
    }

    private void RefreshSettingMethod(object recipient, RefreshSetting message)
    {
        this.HideCover = message.HideCover;
    }

    public virtual void Dispose()
    {
        this.CTS.Cancel();
        this.Messenger.UnregisterAll(this);
    }

    [ObservableProperty]
    bool hideCover;
}
