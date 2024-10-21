using System.Collections.Generic;
using System.Collections.ObjectModel;
using AsmrOne.WinUI3.Models.AsmrOne;
using AsmrOne.WinUI3.ViewModels.ItemViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace AsmrOne.WinUI3.Contracts
{
    public interface IDataFactory
    {
        public ObservableCollection<DetilyItemViewModel> CreateDetilyItemViewModels(
            List<RidDetily> ridDetilys
        );
    }
}
