using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ABI.System;
using AsmrOne.WinUI3.Common;
using AsmrOne.WinUI3.Common.Bases;
using AsmrOne.WinUI3.Contracts;
using AsmrOne.WinUI3.Contracts.Services;
using AsmrOne.WinUI3.Models;
using AsmrOne.WinUI3.Models.AsmrOne;
using AsmrOne.WinUI3.Models.Messagers;
using AsmrOne.WinUI3.Models.UI;
using AsmrOne.WinUI3.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.VisualBasic;
using Windows.Media.Playback;
using WinUIEx;

namespace AsmrOne.WinUI3.ViewModels;

public sealed partial class ShellViewModel : ViewModelBase
{
    public ShellViewModel(
        IAsmrClient asmrClient,
        IDialogManager dialogManager,
        [FromKeyedServices(ProgramLife.ShellNavigationKey)]
            INavigationViewService shellNavigationViewService,
        [FromKeyedServices(ProgramLife.ShellNavigationKey)]
            INavigationService shellNavigationService,
        IAudioPlayerService audioPlayerService,
        IAppSetup<App> appSetup
    )
    {
        AsmrClient = asmrClient;
        DialogManager = dialogManager;
        ShellNavigationViewService = shellNavigationViewService;
        ShellNavigationService = shellNavigationService;
        AudioPlayerService = audioPlayerService;
        AppSetup = appSetup;
        AudioPlayerService.MediaPlayerStatus += AudioPlayerService_MediaPlayerStatus;
        AudioPlayerService.SetDataChanged += AudioPlayerService_SetDataChanged;
        AudioPlayerService.PlayerOpened += AudioPlayerService_PlayerOpened;
        shellNavigationService.Navigated += ShellNavigationService_Navigated;
        RegisterMessager();
        this.IsAutosubtitle = GlobalUsing.IsAutoSubtitle;
        this.IsOpensubtitle = GlobalUsing.IsOpenDesktopSubtitle;
    }

    private void ShellNavigationService_Navigated(
        object sender,
        Microsoft.UI.Xaml.Navigation.NavigationEventArgs e
    )
    {
        this.IsBack = ShellNavigationService.CanGoBack;
        this.NavigtiaonSelectItem = ShellNavigationViewService.GetSelectItem(e.SourcePageType);
    }

    private void RegisterMessager()
    {
        this.Messenger.Register<RefreshAudio>(this, RefreshAudioMethod);
        this.Messenger.Register<RefreshToken>(this, RefreshTokenMethod);
        this.Messenger.Register<Loginout>(this, LoginoutMethod);
        this.Messenger.Register<RefreshSubtitle>(this, RefreshSubtitleMethod);
    }

    private void RefreshSubtitleMethod(object recipient, RefreshSubtitle message)
    {
        if (Subtitle != message.Item.Text)
        {
            this.Subtitle = message.Item.Text;
        }
    }

    [ObservableProperty]
    string subtitle = "字幕";

    [ObservableProperty]
    double maxDuration;

    [ObservableProperty]
    ObservableCollection<ShellSubtitleItem> subtitles = new();

    [ObservableProperty]
    ShellSubtitleItem selectSubtitle;

    [ObservableProperty]
    bool isBack = false;

    [ObservableProperty]
    bool isLogin = false;

    [RelayCommand]
    void Back()
    {
        ShellNavigationService.GoBack();
    }

    [ObservableProperty]
    object navigtiaonSelectItem;

    [ObservableProperty]
    Child child;

    [ObservableProperty]
    RidDetily detily;

    [ObservableProperty]
    BitmapImage cover;

    [ObservableProperty]
    string _StartGlyph;

    [ObservableProperty]
    string maxDurationString;

    [ObservableProperty]
    bool loading;

    [ObservableProperty]
    bool isAutosubtitle;

    [ObservableProperty]
    bool isOpensubtitle;

    partial void OnIsOpensubtitleChanged(bool value)
    {
        GlobalUsing.IsOpenDesktopSubtitle = value;
        if (value)
        {
            if (AppSetup.SubWindow == null)
            {
                var win = WindowExtension.CreateMicaWindow(WindowExtension.CreateType.Subtitle);
                AppSetup.RegisterSubtitleWindow(win);
            }
        }
        else
        {
            if (AppSetup.SubWindow != null)
            {
                AppSetup.DisponseSubtitle();
            }
        }
    }

    private void LoginoutMethod(object recipient, Loginout message)
    {
        if (message.IsRefresh)
        {
            ShellNavigationService.NavigationTo<HomeViewModel>(nameof(HomeViewModel));
            this.ShellNavigationService.ClearHistory();
            this.IsLogin = AsmrClient.IsLogin;
            this.IsBack = ShellNavigationService.CanGoBack;
        }
    }

    private void RefreshTokenMethod(object recipient, RefreshToken message)
    {
        if (message.IsRefresh && AsmrClient.IsLogin)
        {
            this.IsLogin = true;
            this.DisPlayName = "账户：" + AsmrClient.UserName.Substring(0, 1);
        }
    }

    private void RefreshAudioMethod(object recipient, RefreshAudio message)
    {
        Child = message.Child;
        Detily = message.Detily;
        SetSubtitle(message);
    }

    private void AudioPlayerService_PlayerOpened(object sender, MediaPlaybackSession data) { }

    async partial void OnSelectSubtitleChanged(ShellSubtitleItem value)
    {
        if (value == null)
        {
            return;
        }
        var subTitle = await AsmrClient.Client.GetStringAsync(value.DownloadUrl);
        AudioPlayerService.SubtitleService.SetSubtitle(subTitle);
    }

    partial void OnIsAutosubtitleChanged(bool value)
    {
        GlobalUsing.IsAutoSubtitle = value;
    }

    private void SetSubtitle(RefreshAudio message)
    {
        Subtitles.Clear();
        foreach (var item in message.AudioWrapper.SubTitles)
        {
            if (item is TextWrapper text)
            {
                this.Subtitles.Add(
                    new()
                    {
                        DownloadUrl = text.DownloadPath,
                        FileName = text.FileName,
                        Name = text.FileName.Substring(0, text.FileName.IndexOf('.')),
                    }
                );
            }
            if (item is SubtitleWrapper subtitle)
            {
                this.Subtitles.Add(
                    new()
                    {
                        DownloadUrl = subtitle.DownloadPath,
                        FileName = subtitle.FileName,
                        Name = subtitle.FileName.Substring(0, subtitle.FileName.IndexOf('.')),
                    }
                );
            }
        }
        if (GlobalUsing.IsAutoSubtitle)
        {
            foreach (var item in this.Subtitles)
            {
                if (
                    item.Name
                    == message.AudioWrapper.FileName.Substring(
                        0,
                        message.AudioWrapper.FileName.IndexOf('.')
                    )
                )
                {
                    this.SelectSubtitle = item;
                }
            }
        }
    }

    private void AudioPlayerService_SetDataChanged(object sender, Models.AsmrOne.RidDetily child)
    {
        this.Cover = new BitmapImage(new System.Uri(child.ThumbnailCoverUrl));
    }

    private void AudioPlayerService_MediaPlayerStatus(
        Windows.Media.Playback.MediaPlayer player,
        Windows.Media.Playback.MediaPlaybackState status
    )
    {
        AppSetup.MainWindow.DispatcherQueue.TryEnqueue(() =>
        {
            this.StartGlyph = status
                is MediaPlaybackState.None
                    or MediaPlaybackState.Opening
                    or MediaPlaybackState.Paused
                ? "\uE102"
                : "\uE103";
            this.Loading = status == MediaPlaybackState.Buffering ? true : false;
            MaxDuration = player.NaturalDuration.TotalSeconds;
            MaxDurationString = player.NaturalDuration.ToString("hh\\:mm\\:ss");
        });
    }

    [ObservableProperty]
    ObservableCollection<PingResult> ips;

    [ObservableProperty]
    PingResult selectPing;

    [ObservableProperty]
    string disPlayName = "Login";

    public IAsmrClient AsmrClient { get; }
    public IDialogManager DialogManager { get; }
    public INavigationViewService ShellNavigationViewService { get; }
    public INavigationService ShellNavigationService { get; }
    public IAppSetup<App> AppSetup { get; }

    [ObservableProperty]
    public IAudioPlayerService _AudioPlayerService;

    partial void OnSelectPingChanged(PingResult value)
    {
        AsmrClient.RegisterClient(value.HostName);
    }

    [RelayCommand]
    void Loginout()
    {
        AsmrClient.Loginout();
        this.LoginoutMethod(this, new(true));
        this.DisPlayName = "Login";
    }

    [RelayCommand()]
    async Task ShowRegister()
    {
        if (!AsmrClient.IsLogin)
        {
            await DialogManager.ShowRegisterAsync();
        }
    }

    [RelayCommand]
    async Task Loaded()
    {
        var token = TokenInstance.GetToken();
        if (token != null)
        {
            AsmrClient.SetToken(token);
            this.AsmrClient.UserName = token.User.Name;
            this.DisPlayName = "账户：" + token.User.Name.Substring(0, 1);
            this.IsLogin = true;
        }
        this.Ips = await AsmrOne.WinUI3.Contracts.Services.AsmrClient.GetPingAsync();
        this.SelectPing = Ips.Where(x => x.Time > 0).First();
        if (SelectPing == null) { }
        else
        {
            this.ShellNavigationService.NavigationTo<HomeViewModel>(nameof(HomeViewModel));
        }
    }

    [RelayCommand]
    void Switch()
    {
        AudioPlayerService.Switch();
    }
}
