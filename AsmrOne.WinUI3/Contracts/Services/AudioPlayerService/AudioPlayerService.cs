using System;
using AsmrOne.WinUI3.Models.AsmrOne;
using AsmrOne.WinUI3.Models.Messagers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.UI.Xaml.Controls;
using Windows.Media.Playback;

namespace AsmrOne.WinUI3.Contracts.Services;

public partial class AudioPlayerService : ObservableObject, IAudioPlayerService
{
    public bool IsDrag { get; set; }

    public RidDetily RidDetily { get; private set; }
    public Child ChildData { get; private set; }
    public string NowFileName { get; private set; }
    public MediaPlayerPresenter Element { get; private set; }

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

    public void Player(Child url, RidDetily data)
    {
        if (url.Title == NowFileName)
            return;
        this.NowFileName = url.Title;
        this.RidDetily = data;
        this.ChildData = url;
        UnRegister();
        Stop();
        this.Element.MediaPlayer.SetUriSource(new Uri(url.MediaStreamUrl));
        Register();
        Play();
        this.setDataHandler?.Invoke(this, data);
        WeakReferenceMessenger.Default.Send(new RefreshAudio(data, url));
    }

    private void Player_MediaOpened(MediaPlayer sender, object args)
    {
        this.openedHandler?.Invoke(this, sender.PlaybackSession);
    }

    private void PlaybackSession_PlaybackStateChanged(MediaPlaybackSession sender, object args)
    {
        App.MainWindow.DispatcherQueue.TryEnqueue(() =>
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
    }

    public void Switch()
    {
        App.MainWindow.DispatcherQueue.TryEnqueue(() =>
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
