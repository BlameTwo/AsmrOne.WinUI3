using System.Collections.ObjectModel;
using AsmrOne.WinUI3.Models;
using AsmrOne.WinUI3.Models.AsmrOne;

namespace AsmrOne.WinUI3.Contracts.Services;

public class AudioManager : IAudioManager
{
    public RidDetily Detily { get; private set; }

    public ObservableCollection<IAudioDataWrapper> Tracks { get; set; }

    public void SetDetily(RidDetily ridDetily)
    {
        this.Detily = ridDetily;
    }
}
