
using AsmrOne.WinUI3.Models.AsmrOne;
using AsmrOne.WinUI3.Models.ItemWrapper;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AsmrOne.WinUI3.Common
{
    public static class ToObservableCollection
    {
        public static ObservableCollection<T> ToObservable<T>(this IEnumerable<T> value) =>
            new ObservableCollection<T>(value);


        
        
    }
}
