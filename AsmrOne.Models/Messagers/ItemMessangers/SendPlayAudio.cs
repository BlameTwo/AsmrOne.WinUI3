using AsmrOne.WinUI3.Models.AsmrOne;

namespace AsmrOne.WinUI3.Models.Messagers.ItemMessangers;

public class RidDetilySendPlayAudio
{
    public AudioWrapper Audio { get; init; }

    public RidDetilySendPlayAudio(AudioWrapper audio)
    {
        Audio = audio;
    }
}

public class DownloadSingleFile
{
    public DownloadSingleFile(Child downloadFile)
    {
        DownloadFile = downloadFile;
    }

    public Child DownloadFile { get; }
}