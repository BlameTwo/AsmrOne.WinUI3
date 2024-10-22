using CommunityToolkit.Mvvm.ComponentModel;
using Windows.Storage;

namespace AsmrOne.WinUI3.ViewModels;

public sealed partial class SettingViewModel : ObservableRecipient
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
    }

    partial void OnIsTagChanged(bool value)
    {
        GlobalUsing.IsHideR18Tag = value;
    }
}
