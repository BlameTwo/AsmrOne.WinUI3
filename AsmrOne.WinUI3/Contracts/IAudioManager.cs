using System.Collections.ObjectModel;
using AsmrOne.WinUI3.Models;
using AsmrOne.WinUI3.Models.AsmrOne;

namespace AsmrOne.WinUI3.Contracts;

/// <summary>
/// 单个作品管理
/// </summary>
public interface IAudioManager
{
    RidDetily Detily { get; }
    ObservableCollection<IAudioDataWrapper> Tracks { get; }

    void SetDetily(RidDetily ridDetily);
}
