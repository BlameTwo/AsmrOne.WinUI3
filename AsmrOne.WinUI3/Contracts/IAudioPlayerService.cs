using AsmrOne.WinUI3.Models;
using AsmrOne.WinUI3.Models.AsmrOne;
using AsmrOne.WinUI3.Models.Enums;
using System.Collections.Generic;
using Windows.Media.Playback;

namespace AsmrOne.WinUI3.Contracts;

public interface IAudioPlayerService
{
    #region Event

    event MediaPlayerIndexChanged MediaPlayerIndexChanged;
    event MediaPlayerSetDataChanged MediaPlayerSetDataChanged;
    #endregion

    List<AudioWrapper> Audios { get; }
    string CurrentFileName { get; set; }
    RidDetily Detily { get; }
    MediaPlayer PlayerSource { get; set; }
    PlayerType PlayerType { get; set; }
    List<SubtitleWrapper> Subtitle { get; }
    void Dispose();
    void OnForward();
    void OnNext();
    void OnPause();
    void OnPlay();
    void OnPlayerInit(string title = null);
    void SetAudioSubtitles(List<SubtitleWrapper> subtitleWrappers);
    void SetDetily(RidDetily ridDetily, List<AudioWrapper> audiosWrapper);
    void SetPlay();
    void SetPostion();
}