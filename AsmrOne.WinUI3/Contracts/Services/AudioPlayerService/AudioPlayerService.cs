using AsmrOne.WinUI3.Models;
using AsmrOne.WinUI3.Models.AsmrOne;
using AsmrOne.WinUI3.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace AsmrOne.WinUI3.Contracts.Services;

public class AudioPlayerService : IDisposable, IAudioPlayerService
{
    #region Event

    public MediaPlayerSetDataChanged mediaPlayerSetDataChanged;

    public event MediaPlayerSetDataChanged MediaPlayerSetDataChanged
    {
        add { mediaPlayerSetDataChanged += value; }
        remove { mediaPlayerSetDataChanged -= value; }
    }

    public MediaPlayerIndexChanged mediaPlayerIndexChanged;

    public event MediaPlayerIndexChanged MediaPlayerIndexChanged
    {
        add { mediaPlayerIndexChanged += value; }
        remove { mediaPlayerIndexChanged -= value; }
    }
    #endregion



    public PlayerType PlayerType { get; set; } = PlayerType.ListLoop;

    /// <summary>
    /// 播放索引
    /// </summary>
    private int PlayerIndex
    {
        get => field;
        set
        {
            field = value;
            this.mediaPlayerIndexChanged?.Invoke(this,value);
        }
    }

    public MediaPlayer? PlayerSource { get; set; }

    public List<AudioWrapper> Audios { get; private set; }
    public List<SubtitleWrapper> Subtitle { get; private set; }

    public string CurrentFileName { get; set; }
    public RidDetily Detily { get; private set; }

    public AudioPlayerService()
    {
        PlayerSource = new MediaPlayer();
        PlayerSource.CommandManager.NextReceived += CommandManager_NextReceived;
        PlayerSource.CommandManager.PreviousReceived += CommandManager_PreviousReceived;
        PlayerSource.CommandManager.PreviousReceived += CommandManager_PreviousReceived;
        PlayerSource.PlaybackSession.PositionChanged += PlaybackSession_PositionChanged;
        PlayerSource.MediaOpened += PlayerSource_MediaOpened;
    }

    private void PlayerSource_MediaOpened(MediaPlayer sender, object args)
    {
        PlayerSource.Play();
    }

    private void PlaybackSession_PositionChanged(MediaPlaybackSession sender, object args)
    {
        if (sender.NaturalDuration == sender.Position)
        {
            switch (this.PlayerType)
            {
                case PlayerType.Single:
                    OnPause();
                    this.PlayerSource.Position = TimeSpan.FromSeconds(0);
                    OnPlay();
                    return;
                case PlayerType.ListLoop:
                    OnNext();
                    break;
                case PlayerType.Random:
                    PlayerIndex = Random.Shared.Next(0, Audios.Count - 1);
                    break;
                default:
                    break;
            }
            OnPlay();
        }
    }

    private void CommandManager_PreviousReceived(MediaPlaybackCommandManager sender, MediaPlaybackCommandManagerPreviousReceivedEventArgs args)
    {

    }

    private void CommandManager_NextReceived(MediaPlaybackCommandManager sender, MediaPlaybackCommandManagerNextReceivedEventArgs args)
    {

    }


    /// <summary>
    /// 下一个
    /// </summary>
    public void OnNext()
    {
        if (PlayerIndex == this.Audios.Count - 1)
        {
            PlayerIndex = 0;
        }
        else
        {
            PlayerIndex++;
        }
    }
    public void SetPostion()
    {
        if (PlayerSource == null)
            return;

    }

    /// <summary>
    /// 上一个
    /// </summary>
    public void OnForward()
    {
        if (PlayerIndex != 0)
        {
            PlayerIndex--;
        }
    }


    public void SetDetily(RidDetily ridDetily,List<AudioWrapper> audios)
    {
        this.Detily = ridDetily;
        this.Audios = audios;
        this.mediaPlayerSetDataChanged?.Invoke(this, Detily);
    }


    public void SetAudioSubtitles(List<SubtitleWrapper> subtitleWrappers)
    {
        this.Subtitle = subtitleWrappers;
    }

    public void OnPlayerInit(string title = null)
    {
        if (PlayerSource == null)
            return;
        PlayerSource.Pause();
        var playerTitle = "";
        if (title == null)
        {
            switch (PlayerType)
            {
                case PlayerType.Single:
                    PlayerIndex = 0;
                    break;
                case PlayerType.ListLoop:
                    PlayerIndex = 0;
                    break;
                case PlayerType.Random:
                    PlayerIndex = Random.Shared.Next(0, Audios.Count - 1);
                    break;
                default:
                    break;
            }
            playerTitle = Audios[PlayerIndex].FileName;
        }
        else
        {
            var first = Audios.Where(x => x.FileName == playerTitle).FirstOrDefault() ?? Audios[0];
        }
        OnPlay();
    }

    public void OnPlay()
    {
        var url = new Uri(Audios[index: PlayerIndex].MediaStreamUrl);
        var source = MediaSource.CreateFromUri(url);
        this.PlayerSource.Source = source;
    }

    public void SetPlay()
    {
        if (PlayerSource == null)
            return;
        PlayerSource.Play();
    }

    public void OnPause()
    {
        if (PlayerSource == null)
            return;
        if (PlayerSource.CanPause)
            PlayerSource.Pause();
    }

    public void Dispose()
    {
        PlayerSource.CommandManager.NextReceived -= CommandManager_NextReceived;
        PlayerSource.CommandManager.PreviousReceived -= CommandManager_PreviousReceived;
        PlayerSource.CommandManager.PreviousReceived -= CommandManager_PreviousReceived;
        PlayerSource.PlaybackSession.PositionChanged -= PlaybackSession_PositionChanged;
        PlayerSource.MediaOpened -= PlayerSource_MediaOpened;
        OnPause();
        PlayerSource.Dispose();
        PlayerSource = null;
    }
}
