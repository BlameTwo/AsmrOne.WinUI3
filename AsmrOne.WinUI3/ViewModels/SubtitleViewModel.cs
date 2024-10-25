using System;
using AsmrOne.WinUI3.Common.Bases;
using AsmrOne.WinUI3.Models.Messagers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace AsmrOne.WinUI3.ViewModels;

public sealed partial class SubtitleViewModel : ViewModelBase
{
    public SubtitleViewModel()
    {
        RegisterMessenger();
    }

    [ObservableProperty]
    string subTitle = "等待播放";

    private void RegisterMessenger()
    {
        this.Messenger.Register<RefreshSubtitle>(this, RefreshSubTitleMethod);
    }

    private void RefreshSubTitleMethod(object recipient, RefreshSubtitle message)
    {
        if (message.Item != null)
        {
            SubTitle = message.Item.Text;
        }
    }
}
