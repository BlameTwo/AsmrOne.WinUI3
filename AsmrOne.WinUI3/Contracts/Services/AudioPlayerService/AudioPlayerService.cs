﻿using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Timers;
using AsmrOne.WinUI3.Models;
using AsmrOne.WinUI3.Models.AsmrOne;
using AsmrOne.WinUI3.Models.Messagers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Windows.AI.MachineLearning;
using Windows.Media.Playback;

namespace AsmrOne.WinUI3.Contracts.Services;

public partial class AudioPlayerService : IAudioPlayerService
{
    public AudioPlayerService(ISubtitleService subtitleService)
    {
        SubtitleService = subtitleService;
        timer = new Timer();
        timer.Interval = 100;
        timer.Elapsed += Timer_Elapsed;
        timer.Start();
    }

    private void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
        if (App.MainWindow.DispatcherQueue == null)
            return;
        App.MainWindow.DispatcherQueue.TryEnqueue(
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

    public RidDetily RidDetily { get; private set; }
    public Child ChildData { get; private set; }
    public string NowFileName { get; private set; }
    public MediaPlayerPresenter Element { get; private set; }

    public ISubtitleService SubtitleService { get; }

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

    public async Task PlayerAsync(AudioWrapper url, RidDetily data, string subUrl = null)
    {
        if (url.Child.Title == NowFileName)
            return;
        this.NowFileName = url.Child.Title;
        this.RidDetily = data;
        this.ChildData = url.Child;
        UnRegister();
        Stop();
        this.Element.MediaPlayer.SetUriSource(new Uri(url.MediaStreamUrl));
        Register();
        this.setDataHandler?.Invoke(this, data);
        WeakReferenceMessenger.Default.Send(new RefreshAudio(data, url.Child, url));
        if (url.SubTitles != null && url.SubTitles.Count > 0)
        {
            foreach (var item in url.SubTitles)
            {
                if (item is TextWrapper text)
                {
                    var FN1 = Path.GetFileNameWithoutExtension(url.FileName);
                    var FN2 = Path.GetFileNameWithoutExtension(text.FileName);
                    if (FN1 == FN2)
                    {
                        var subTitle = await ProgramLife
                            .ServiceProvider.GetService<IAsmrClient>()
                            .Client.GetStringAsync(text.DownloadPath);
                        SubtitleService.SetSubtitle(subTitle);
                    }
                }
                else if (item is SubtitleWrapper subtitleWrapper)
                {
                    var FN1 = url.FileName.Substring(0, url.FileName.IndexOf('.'));
                    var FN2 = subtitleWrapper.FileName.Substring(
                        0,
                        subtitleWrapper.FileName.IndexOf('.')
                    );
                    if (FN1 == FN2)
                    {
                        var subTitle = await ProgramLife
                            .ServiceProvider.GetService<IAsmrClient>()
                            .Client.GetStringAsync(subtitleWrapper.DownloadPath);
                        SubtitleService.SetSubtitle(subTitle);
                    }
                }
            }
        }
        Play();
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
