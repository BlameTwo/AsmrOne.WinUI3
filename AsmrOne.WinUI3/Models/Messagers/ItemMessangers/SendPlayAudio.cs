namespace AsmrOne.WinUI3.Models.Messagers.ItemMessangers;

public class RidDetilySendPlayAudio
{
    public AudioWrapper Audio { get; init; }

    public RidDetilySendPlayAudio(AudioWrapper audio)
    {
        Audio = audio;
    }
}
