using AsmrOne.WinUI3.Models;
using AsmrOne.WinUI3.Models.AsmrOne;
using Microsoft.UI.Xaml.Controls;

namespace AsmrOne.WinUI3.Contracts;

public interface IAudioPlayerService
{
    public void RegisterElement(MediaPlayerPresenter element);

    public bool IsDrag { get; set; }
    public MediaPlayerPresenter Element { get; }
    #region Event
    event MediaPlayerStatusChanged MediaPlayerStatus;

    event MediaPlayerSetDataChanged SetDataChanged;

    event MediaPlayerOpenedChanged PlayerOpened;

    event MediaPlayerPostionChanged PlayerPostionChanged;
    #endregion

    public RidDetily RidDetily { get; }

    public Child ChildData { get; }
    public string NowFileName { get; }

    public void SetPostion(double postion);

    public void Player(Child url, RidDetily data);

    public void Pause();

    public void Play();

    public void Stop();

    public void SetVolumn(double value);
    void Switch();
}
