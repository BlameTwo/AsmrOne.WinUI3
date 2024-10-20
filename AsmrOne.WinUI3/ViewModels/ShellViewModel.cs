using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AsmrOne.WinUI3.Common;
using AsmrOne.WinUI3.Contracts;
using AsmrOne.WinUI3.Contracts.Services;
using AsmrOne.WinUI3.Models;
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
    }

    private void AudioPlayerService_SetDataChanged(object sender, Models.AsmrOne.RidDetily child)
    {
        this.Cover = new BitmapImage(new System.Uri(child.ThumbnailCoverUrl));
    }

    [ObservableProperty]
    BitmapImage cover;

    [ObservableProperty]
    string _StartGlyph;

    private void AudioPlayerService_MediaPlayerStatus(
        Windows.Media.Playback.MediaPlayer player,
        Windows.Media.Playback.MediaPlaybackState status
    )
    {
        this.StartGlyph = status
            is MediaPlaybackState.None
                or MediaPlaybackState.Opening
                or MediaPlaybackState.Paused
            ? "\uE102"
            : "\uE103";
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

    public void Receive(RefreshToken message)
    {
        if (message.IsRefresh) { }
    }
}
