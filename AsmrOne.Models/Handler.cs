using System;
using AsmrOne.WinUI3.Models.AsmrOne;
using Windows.Media.Playback;

namespace AsmrOne.WinUI3.Models;

public delegate void MediaPlayerStatusChanged(MediaPlayer player, MediaPlaybackState status);

public delegate void MediaPlayerSetDataChanged(object sender, RidDetily child);

public delegate void MediaPlayerOpenedChanged(object sender, MediaPlaybackSession data);

public delegate void MediaPlayerPostionChanged(object sender, TimeSpan time);

public delegate void PlayerProgressChangedDelegate(object source, TimeSpan time);
