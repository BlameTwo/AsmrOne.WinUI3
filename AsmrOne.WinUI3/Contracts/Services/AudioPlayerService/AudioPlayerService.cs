using System;
using System.Diagnostics;
using System.Timers;
using AsmrOne.WinUI3.Models;
using AsmrOne.WinUI3.Models.AsmrOne;
using AsmrOne.WinUI3.Models.Messagers;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.UI.Xaml.Controls;
using Windows.Media.Playback;
using Windows.Media.Streaming.Adaptive;

namespace AsmrOne.WinUI3.Contracts.Services;

public partial class AudioPlayerService : IAudioPlayerService
{
    public AudioPlayerService(
        ISubtitleService subtitleService,
        IAudioManager audioManager,
        IAppSetup<App> appSetup
    )
    {
        SubtitleService = subtitleService;
        AudioManager = audioManager;
        AppSetup = appSetup;
        timer = new Timer();
        timer.Interval = 100;
        timer.Elapsed += Timer_Elapsed;
        timer.Start();
    }

    private void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
        if (AppSetup.MainWindow.DispatcherQueue == null)
            return;
        AppSetup.MainWindow.DispatcherQueue.TryEnqueue(
            Microsoft.UI.Dispatching.DispatcherQueuePriority.High,
            () =>
            {
                try
                {
                    if (OpenSubtitle == false)
                        return;
                    if (Element == null || Element.MediaPlayer == null)
                        return;
                    var subItem = SubtitleService.GetSubtitle(this.Element.MediaPlayer.Position);
                    if (subItem == default)
                        return;
                    WeakReferenceMessenger.Default.Send<RefreshSubtitle>(new(subItem));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        );
    }

    public bool OpenSubtitle { get; set; } = true;

    Timer timer = null;

    public bool IsDrag { get; set; }

    public Child ChildData { get; private set; }
    public string NowFileName { get; private set; }
    public MediaPlayerPresenter Element { get; private set; }

    private AdaptiveMediaSource msource;
    public ISubtitleService SubtitleService { get; }
    public IAudioManager AudioManager { get; }
    public IAppSetup<App> AppSetup { get; }

    public void Pause()
    {
        if (Element.MediaPlayer == null || !Element.MediaPlayer.CanPause)
            return;
        Element.MediaPlayer.Pause();
    }

    public void Play()
    {
        if (this.Element.MediaPlayer == null)
            return;
        this.Element.MediaPlayer.Play();
    }

    public void Player(AudioWrapper url, RidDetily data)
    {
        if (url.Child.Title == NowFileName)
            return;
        this.NowFileName = url.Child.Title;
        this.AudioManager.SetDetily(data);
        this.ChildData = url.Child;
        UnRegister();
        Stop();
        //var medSource = MediaSource.CreateFromUri(new Uri(url.MediaStreamUrl));
        this.Element.MediaPlayer.SetUriSource(new(url.MediaStreamUrl));
        Register();
        this.setDataHandler?.Invoke(this, data);
        WeakReferenceMessenger.Default.Send(new RefreshAudio(data, url.Child, url));
    }

    private void Player_MediaOpened(MediaPlayer sender, object args)
    {
        this.openedHandler?.Invoke(this, sender.PlaybackSession);
    }

    private void PlaybackSession_PlaybackStateChanged(MediaPlaybackSession sender, object args)
    {
        AppSetup.MainWindow.DispatcherQueue.TryEnqueue(() =>
        {
            this.mediaplayerStatus?.Invoke(this.Element.MediaPlayer, sender.PlaybackState);
        });
    }

    public void SetPostion(double postion)
    {
        if (Element.MediaPlayer == null)
            return;
        Element.MediaPlayer.Position = TimeSpan.FromSeconds(postion);
    }

    public void SetVolumn(double value)
    {
        if (Element.MediaPlayer == null)
            return;
        Element.MediaPlayer.Volume = value;
    }

    public void UnRegister()
    {
        if (Element.MediaPlayer is null)
        {
            return;
        }
        this.Element.MediaPlayer.PlaybackSession.PlaybackStateChanged -=
            PlaybackSession_PlaybackStateChanged;
        this.Element.MediaPlayer.MediaOpened -= Player_MediaOpened;
    }

    public void Register()
    {
        this.Element.MediaPlayer.PlaybackSession.PlaybackStateChanged +=
            PlaybackSession_PlaybackStateChanged;
        this.Element.MediaPlayer.MediaOpened += Player_MediaOpened;
    }

    public void Stop()
    {
        if (Element.MediaPlayer == null)
            return;
        UnRegister();
        Element.MediaPlayer.Pause();
        GC.Collect();
    }

    public void RegisterElement(MediaPlayerPresenter element)
    {
        this.Element = element;
        this.Element.MediaPlayer = new();
        Element.MediaPlayer.AutoPlay = true;
    }

    public void Switch()
    {
        AppSetup.MainWindow.DispatcherQueue.TryEnqueue(() =>
        {
            if (
                this.Element.MediaPlayer.PlaybackSession.PlaybackState == MediaPlaybackState.Playing
            )
            {
                this.Element.MediaPlayer.Pause();
            }
            else if (
                this.Element.MediaPlayer.PlaybackSession.PlaybackState == MediaPlaybackState.Paused
            )
            {
                this.Element.MediaPlayer.Play();
            }
        });
    }
}
