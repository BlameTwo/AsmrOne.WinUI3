using AsmrOne.WinUI3.Models.AsmrOne;
using Windows.Media.Playback;

namespace AsmrOne.WinUI3.Models;

public delegate void MediaPlayerStatusChanged(MediaPlayer player, MediaPlaybackState status);

public delegate void MediaPlayerSetDataChanged(object sender, RidDetily child);
