using System.Collections.Generic;
using System.Collections.ObjectModel;
using AsmrOne.WinUI3.Common;
using AsmrOne.WinUI3.Models;
using AsmrOne.WinUI3.Models.AsmrOne;
using AsmrOne.WinUI3.ViewModels;

namespace AsmrOne.WinUI3.Contracts;

public interface IDataAdaptiveService
{
    public ObservableCollection<WorkWrapper> GetWrapperData(List<RidDetily> wors);

    public ObservableCollection<IAudioDataWrapper> GetAudioData(List<Child> video, RidDetily work);

    public RidPlayerViewModel CreateRidPlayerViewModel();
}
