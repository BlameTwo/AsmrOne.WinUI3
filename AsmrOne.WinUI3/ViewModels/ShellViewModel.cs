using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AsmrOne.WinUI3.Common;
using AsmrOne.WinUI3.Contracts;
using AsmrOne.WinUI3.Contracts.Services;
using AsmrOne.WinUI3.Models;
using AsmrOne.WinUI3.Models.AsmrOne;
using AsmrOne.WinUI3.Models.Messagers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Media.Playback;

namespace AsmrOne.WinUI3.ViewModels;

public sealed partial class ShellViewModel : ObservableRecipient, IRecipient<RefreshToken>
{
    public ShellViewModel(
        IAsmrClient asmrClient,
        IDialogManager dialogManager,
        [FromKeyedServices(ProgramLife.ShellNavigationKey)]
            INavigationViewService shellNavigationViewService,
        [FromKeyedServices(ProgramLife.ShellNavigationKey)]
            INavigationService shellNavigationService,
        IAudioPlayerService audioPlayerService
    )
    {
        AsmrClient = asmrClient;
        DialogManager = dialogManager;
        ShellNavigationViewService = shellNavigationViewService;
        ShellNavigationService = shellNavigationService;
        AudioPlayerService = audioPlayerService;
        AudioPlayerService.MediaPlayerStatus += AudioPlayerService_MediaPlayerStatus;
        AudioPlayerService.SetDataChanged += AudioPlayerService_SetDataChanged;
        AudioPlayerService.PlayerOpened += AudioPlayerService_PlayerOpened;
        shellNavigationService.Navigated += ShellNavigationService_Navigated;
        RegisterMessager();
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
    }

    private void RefreshAudioMethod(object recipient, RefreshAudio message)
    {
        Child = message.Child;
        Detily = message.Detily;
    }

    private void AudioPlayerService_PlayerOpened(object sender, MediaPlaybackSession data) { }

    [ObservableProperty]
    double maxDuration;

    [ObservableProperty]
    bool isBack = false;

    [RelayCommand]
    void Back()
    {
        ShellNavigationService.GoBack();
    }

    [ObservableProperty]
    object navigtiaonSelectItem;

    private void AudioPlayerService_SetDataChanged(object sender, Models.AsmrOne.RidDetily child)
    {
        this.Cover = new BitmapImage(new System.Uri(child.ThumbnailCoverUrl));
    }

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

    private void AudioPlayerService_MediaPlayerStatus(
        Windows.Media.Playback.MediaPlayer player,
        Windows.Media.Playback.MediaPlaybackState status
    )
    {
        App.MainWindow.DispatcherQueue.TryEnqueue(() =>
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
    string disPlayName = "登录";

    public IAsmrClient AsmrClient { get; }
    public IDialogManager DialogManager { get; }
    public INavigationViewService ShellNavigationViewService { get; }
    public INavigationService ShellNavigationService { get; }

    [ObservableProperty]
    public IAudioPlayerService _AudioPlayerService;

    partial void OnSelectPingChanged(PingResult value)
    {
        AsmrClient.RegisterClient(value.HostName);
    }

    [RelayCommand]
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
            this.DisPlayName = token.User.Name.Substring(0, 1);
        }
        this.Ips = await AsmrOne.WinUI3.Contracts.Services.AsmrClient.GetPingAsync();
    }

    [RelayCommand]
    void Switch()
    {
        AudioPlayerService.Switch();
    }

    public void Receive(RefreshToken message)
    {
        if (message.IsRefresh) { }
    }
}
