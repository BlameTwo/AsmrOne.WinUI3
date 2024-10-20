using AsmrOne.WinUI3.Models;
using Windows.Media.Playback;

namespace AsmrOne.WinUI3.Contracts.Services
{
    partial class AudioPlayerService
    {
        private MediaPlayerStatusChanged mediaplayerStatus;

        public event MediaPlayerStatusChanged MediaPlayerStatus
        {
            add => mediaplayerStatus += value;
            remove => mediaplayerStatus -= value;
        }
        private MediaPlayerSetDataChanged setDataHandler;

        public event MediaPlayerSetDataChanged SetDataChanged
        {
            add => setDataHandler += value;
            remove => setDataHandler -= value;
        }
        private MediaPlayerOpenedChanged openedHandler;

        public event MediaPlayerOpenedChanged PlayerOpened
        {
            add => openedHandler += value;
            remove => openedHandler -= value;
        }

        private MediaPlayerPostionChanged playerPostion;
        public event MediaPlayerPostionChanged PlayerPostionChanged
        {
            add => playerPostion += value;
            remove => playerPostion -= value;
        }

        private PlayerProgressChangedDelegate PlayerProgressHandler;
        public event PlayerProgressChangedDelegate PlayerProgressChanged
        {
            add { PlayerProgressHandler += value; }
            remove { PlayerProgressHandler -= value; }
        }
    }
}
