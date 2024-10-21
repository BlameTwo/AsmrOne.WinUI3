using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsmrOne.WinUI3.Models.AsmrOne;
using AsmrOne.WinUI3.ViewModels.ItemViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace AsmrOne.WinUI3.Contracts.Services
{
    public class DataFactory : IDataFactory
    {
        public ObservableCollection<DetilyItemViewModel> CreateDetilyItemViewModels(
            List<RidDetily> ridDetilys
        )
        {
            ObservableCollection<DetilyItemViewModel> viewModels = new();
            foreach (var item in ridDetilys)
            {
                var viewModel = ProgramLife.ServiceProvider.GetService<DetilyItemViewModel>();
                viewModel.SetData(item);
                viewModels.Add(viewModel);
            }
            return viewModels;
        }
    }
}
