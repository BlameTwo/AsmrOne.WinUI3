using AsmrOne.WinUI3.Models;
using AsmrOne.WinUI3.Models.AsmrOne;

namespace AsmrOne.WinUI3.Contracts;

public interface IAudioPlayerService
{
    #region Event
    event MediaPlayerStatusChanged MediaPlayerStatus;

    event MediaPlayerSetDataChanged SetDataChanged;
    #endregion
    string StartGlyph { get; set; }

    public void SetPostion(double postion);

    public void Player(Child url, RidDetily data);

    public void Pause();

    public void Play();

    public void Stop();

    public void SetVolumn();
}
