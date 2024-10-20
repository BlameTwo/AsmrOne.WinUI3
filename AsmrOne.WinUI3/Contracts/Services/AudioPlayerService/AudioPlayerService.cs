using System;
using AsmrOne.WinUI3.Models.AsmrOne;
using CommunityToolkit.Mvvm.ComponentModel;
using Windows.Media.Playback;

namespace AsmrOne.WinUI3.Contracts.Services;

public partial class AudioPlayerService : ObservableObject, IAudioPlayerService
{
    MediaPlayer player;

    public void Pause()
    {
        throw new NotImplementedException();
    }

    public void Play()
    {
        if (this.player == null)
            return;
        this.player.Play();
    }

    public void Player(Child url, RidDetily data)
    {
        this.player = new MediaPlayer();
        this.player.SetUriSource(new Uri(url.MediaStreamUrl));
        this.player.PlaybackSession.PlaybackStateChanged += PlaybackSession_PlaybackStateChanged;
        Play();
        this.setDataHandler?.Invoke(this, data);
    }

    [ObservableProperty]
    string _StartGlyph = "\uE102";

    private void PlaybackSession_PlaybackStateChanged(MediaPlaybackSession sender, object args)
    {
        App.MainWindow.DispatcherQueue.TryEnqueue(() =>
        {
            this.mediaplayerStatus?.Invoke(this.player, sender.PlaybackState);
        });
    }

    public void SetPostion(double postion)
    {
        throw new NotImplementedException();
    }

    public void SetVolumn()
    {
        throw new NotImplementedException();
    }

    public void Stop()
    {
        throw new NotImplementedException();
    }
}
